using Microsoft.EntityFrameworkCore;
using dayPlanner.Data;
using dayPlanner.Data.Entities;

namespace dayPlanner.Services;

public class DayPlanService : IDayPlanService
{
    private readonly ApplicationDbContext _context;

    public DayPlanService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DayPlan?> GetTodayPlanAsync(int pupilId)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        return await GetPlanByDateAsync(pupilId, today);
    }

    public async Task<DayPlan?> GetPlanByDateAsync(int pupilId, DateOnly date)
    {
        return await _context.DayPlans
            .Include(d => d.Activities.OrderBy(a => a.Order))
            .FirstOrDefaultAsync(d => d.PupilId == pupilId && d.Date == date);
    }

    public async Task<DayPlan> CreateOrUpdatePlanAsync(int pupilId, DateOnly date, string? title)
    {
        var plan = await _context.DayPlans
            .FirstOrDefaultAsync(d => d.PupilId == pupilId && d.Date == date);

        if (plan == null)
        {
            plan = new DayPlan
            {
                PupilId = pupilId,
                Date = date,
                Title = title
            };
            _context.DayPlans.Add(plan);
        }
        else
        {
            plan.Title = title;
        }

        await _context.SaveChangesAsync();
        return plan;
    }

    public async Task<Activity> CreateActivityAsync(int dayPlanId, Activity activity)
    {
        activity.DayPlanId = dayPlanId;
        
        // Get the highest order number for this day plan
        var maxOrder = await _context.Activities
            .Where(a => a.DayPlanId == dayPlanId)
            .Select(a => (int?)a.Order)
            .MaxAsync() ?? 0;
        
        activity.Order = maxOrder + 1;

        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();
        return activity;
    }

    public async Task UpdateActivityAsync(Activity activity)
    {
        _context.Activities.Update(activity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteActivityAsync(int activityId)
    {
        var activity = await _context.Activities.FindAsync(activityId);
        if (activity != null)
        {
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            
            // Reorder remaining activities
            await ReorderActivitiesAfterDelete(activity.DayPlanId);
        }
    }

    public async Task ReorderActivityAsync(int activityId, bool moveUp)
    {
        var activity = await _context.Activities.FindAsync(activityId);
        if (activity == null) return;

        var activities = await _context.Activities
            .Where(a => a.DayPlanId == activity.DayPlanId)
            .OrderBy(a => a.Order)
            .ToListAsync();

        var currentIndex = activities.FindIndex(a => a.Id == activityId);
        if (currentIndex == -1) return;

        int newIndex = moveUp ? currentIndex - 1 : currentIndex + 1;
        if (newIndex < 0 || newIndex >= activities.Count) return;

        // Swap orders
        var tempOrder = activities[currentIndex].Order;
        activities[currentIndex].Order = activities[newIndex].Order;
        activities[newIndex].Order = tempOrder;

        await _context.SaveChangesAsync();
    }

    public async Task MarkActivityCompleteAsync(int activityId)
    {
        var activity = await _context.Activities.FindAsync(activityId);
        if (activity != null)
        {
            activity.CompletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Activity?> GetCurrentActivityAsync(int dayPlanId)
    {
        var activities = await _context.Activities
            .Where(a => a.DayPlanId == dayPlanId)
            .OrderBy(a => a.Order)
            .ToListAsync();

        var now = TimeOnly.FromDateTime(DateTime.Now);
        var today = DateOnly.FromDateTime(DateTime.Today);

        // First, try time-based detection
        foreach (var activity in activities)
        {
            if (activity.CompletedAt != null) continue; // Skip completed

            if (activity.StartTime.HasValue && activity.EndTime.HasValue)
            {
                if (now >= activity.StartTime.Value && now <= activity.EndTime.Value)
                {
                    return activity;
                }
            }
        }

        // Fallback to order-based: first incomplete activity
        return activities.FirstOrDefault(a => a.CompletedAt == null);
    }

    private async Task ReorderActivitiesAfterDelete(int dayPlanId)
    {
        var activities = await _context.Activities
            .Where(a => a.DayPlanId == dayPlanId)
            .OrderBy(a => a.Order)
            .ToListAsync();

        for (int i = 0; i < activities.Count; i++)
        {
            activities[i].Order = i + 1;
        }

        await _context.SaveChangesAsync();
    }
}

