using dayPlanner.Data.Entities;
using dayPlanner.Services;
using Microsoft.AspNetCore.Components;

namespace dayPlanner.Components.Pages
{
    public partial class Today : ComponentBase
    {
        [Inject]
        private IDayPlanService DayPlanService { get; set; } = default!;

        private DayPlan? plan;
        private Activity? currentActivity;
        private const int DefaultPupilId = 1; // Hardcoded for now

        protected override async Task OnInitializedAsync()
        {
            await LoadTodayPlan();
        }

        private async Task LoadTodayPlan()
        {
            plan = await DayPlanService.GetTodayPlanAsync(DefaultPupilId);
            
            if (plan != null)
            {
                currentActivity = await DayPlanService.GetCurrentActivityAsync(plan.Id);
            }
        }

        private async Task HandleMarkAsDone()
        {
            if (currentActivity != null)
            {
                await DayPlanService.MarkActivityCompleteAsync(currentActivity.Id);
                await LoadTodayPlan(); // Reload to update UI
            }
        }

        private bool AllActivitiesCompleted
        {
            get
            {
                if (plan == null || !plan.Activities.Any())
                    return false;

                return plan.Activities.All(a => a.CompletedAt.HasValue);
            }
        }
    }
}

