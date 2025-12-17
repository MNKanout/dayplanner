using dayPlanner.Data.Entities;
using dayPlanner.Services;
using Microsoft.AspNetCore.Components;

namespace dayPlanner.Components.Pages
{
    public partial class EditPlan : ComponentBase
    {
        [Inject]
        private IDayPlanService DayPlanService { get; set; } = default!;

        private DayPlan? plan;
        private DateOnly selectedDate = DateOnly.FromDateTime(DateTime.Today);
        private int? editingActivityId = null;
        private bool isAddingNew = false;
        private const int DefaultPupilId = 1; // Hardcoded for now

        protected override async Task OnInitializedAsync()
        {
            await LoadPlanForDate();
        }

        private async Task LoadPlanForDate()
        {
            plan = await DayPlanService.GetPlanByDateAsync(DefaultPupilId, selectedDate);
            editingActivityId = null;
            isAddingNew = false;
        }

        private async Task CreateNewPlan()
        {
            plan = await DayPlanService.CreateOrUpdatePlanAsync(DefaultPupilId, selectedDate, null);
        }

        private async Task SavePlanTitle()
        {
            if (plan != null)
            {
                await DayPlanService.CreateOrUpdatePlanAsync(DefaultPupilId, selectedDate, plan.Title);
            }
        }

        private void StartAddingNew()
        {
            isAddingNew = true;
            editingActivityId = null;
        }

        private async Task HandleAddActivity(Activity activity)
        {
            if (plan != null)
            {
                await DayPlanService.CreateActivityAsync(plan.Id, activity);
                await LoadPlanForDate();
                isAddingNew = false;
            }
        }

        private void HandleCancelAdd()
        {
            isAddingNew = false;
        }

        private void HandleEditActivity(int activityId)
        {
            editingActivityId = activityId;
            isAddingNew = false;
        }

        private async Task HandleSaveActivity(Activity activity)
        {
            await DayPlanService.UpdateActivityAsync(activity);
            await LoadPlanForDate();
        }

        private void HandleCancelEdit()
        {
            editingActivityId = null;
        }

        private async Task HandleDeleteActivity(int activityId)
        {
            await DayPlanService.DeleteActivityAsync(activityId);
            await LoadPlanForDate();
        }

        private async Task HandleMoveUp(int activityId)
        {
            await DayPlanService.ReorderActivityAsync(activityId, moveUp: true);
            await LoadPlanForDate();
        }

        private async Task HandleMoveDown(int activityId)
        {
            await DayPlanService.ReorderActivityAsync(activityId, moveUp: false);
            await LoadPlanForDate();
        }
    }
}

