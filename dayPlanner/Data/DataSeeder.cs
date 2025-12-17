using dayPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace dayPlanner.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Check if data already exists
            if (await context.Pupils.AnyAsync())
            {
                return; // Data already seeded
            }

            // Create a demo pupil
            var pupil = new Pupil
            {
                Name = "Test Pupil"
            };
            context.Pupils.Add(pupil);
            await context.SaveChangesAsync();

            // Create today's plan
            var today = DateOnly.FromDateTime(DateTime.Today);
            var todayPlan = new DayPlan
            {
                PupilId = pupil.Id,
                Date = today,
                Title = "School Day"
            };
            context.DayPlans.Add(todayPlan);
            await context.SaveChangesAsync();

            // Create activities for today
            var activities = new List<Activity>
            {
                new Activity
                {
                    DayPlanId = todayPlan.Id,
                    Order = 1,
                    Title = "Wake Up",
                    Location = "Home",
                    StartTime = new TimeOnly(7, 0),
                    EndTime = new TimeOnly(7, 30),
                    IconKey = "home",
                    ColorKey = "blue"
                },
                new Activity
                {
                    DayPlanId = todayPlan.Id,
                    Order = 2,
                    Title = "Breakfast",
                    Location = "Home",
                    StartTime = new TimeOnly(7, 30),
                    EndTime = new TimeOnly(8, 0),
                    IconKey = "meal",
                    ColorKey = "yellow"
                },
                new Activity
                {
                    DayPlanId = todayPlan.Id,
                    Order = 3,
                    Title = "School",
                    Location = "Classroom",
                    StartTime = new TimeOnly(8, 30),
                    EndTime = new TimeOnly(12, 0),
                    IconKey = "school",
                    ColorKey = "green"
                },
                new Activity
                {
                    DayPlanId = todayPlan.Id,
                    Order = 4,
                    Title = "Lunch Break",
                    Location = "Cafeteria",
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(12, 30),
                    IconKey = "meal",
                    ColorKey = "orange"
                },
                new Activity
                {
                    DayPlanId = todayPlan.Id,
                    Order = 5,
                    Title = "Afternoon Activities",
                    Location = "Classroom",
                    StartTime = new TimeOnly(12, 30),
                    EndTime = new TimeOnly(15, 0),
                    IconKey = "school",
                    ColorKey = "green"
                },
                new Activity
                {
                    DayPlanId = todayPlan.Id,
                    Order = 6,
                    Title = "Play Time",
                    Location = "Playground",
                    StartTime = new TimeOnly(15, 30),
                    EndTime = new TimeOnly(16, 30),
                    IconKey = "play",
                    ColorKey = "purple"
                },
                new Activity
                {
                    DayPlanId = todayPlan.Id,
                    Order = 7,
                    Title = "Dinner",
                    Location = "Home",
                    StartTime = new TimeOnly(18, 0),
                    EndTime = new TimeOnly(19, 0),
                    IconKey = "meal",
                    ColorKey = "yellow"
                },
                new Activity
                {
                    DayPlanId = todayPlan.Id,
                    Order = 8,
                    Title = "Bedtime",
                    Location = "Home",
                    StartTime = new TimeOnly(20, 0),
                    EndTime = new TimeOnly(20, 30),
                    IconKey = "sleep",
                    ColorKey = "blue"
                }
            };

            context.Activities.AddRange(activities);
            await context.SaveChangesAsync();
        }
    }
}

