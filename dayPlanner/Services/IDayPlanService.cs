using dayPlanner.Data.Entities;

namespace dayPlanner.Services;

public interface IDayPlanService
{
    Task<DayPlan?> GetTodayPlanAsync(int pupilId);
    Task<DayPlan?> GetPlanByDateAsync(int pupilId, DateOnly date);
    Task<DayPlan> CreateOrUpdatePlanAsync(int pupilId, DateOnly date, string? title);
    Task<Activity> CreateActivityAsync(int dayPlanId, Activity activity);
    Task UpdateActivityAsync(Activity activity);
    Task DeleteActivityAsync(int activityId);
    Task ReorderActivityAsync(int activityId, bool moveUp);
    Task MarkActivityCompleteAsync(int activityId);
    Task<Activity?> GetCurrentActivityAsync(int dayPlanId);
}

