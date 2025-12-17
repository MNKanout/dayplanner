using dayPlanner.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace dayPlanner.Components.Shared
{
    public partial class ActivityCard : ComponentBase
    {
        [Parameter] public Activity Activity { get; set; } = null!;
        [Parameter] public bool IsCurrent { get; set; }
        [Parameter] public bool IsCompleted { get; set; }
        [Parameter] public bool ShowActions { get; set; }
        [Parameter] public EventCallback OnMarkAsDone { get; set; }

        private string GetCardClasses()
        {
            var classes = "activity-card-base";
            if (IsCompleted) classes += " completed";
            if (IsCurrent) classes += " current";
            return classes;
        }

        private string GetAriaLabel()
        {
            var label = $"Activity: {Activity.Title}";
            if (IsCurrent) label += ", currently active";
            if (IsCompleted) label += ", completed";
            return label;
        }

        private string GetColorValue()
        {
            return Activity.ColorKey switch
            {
                "blue" => "#3b82f6",
                "green" => "#10b981",
                "yellow" => "#f59e0b",
                "orange" => "#f97316",
                "red" => "#ef4444",
                "purple" => "#a855f7",
                _ => "#6b7280"
            };
        }

        private string GetIconEmoji()
        {
            return Activity.IconKey switch
            {
                "home" => "🏠",
                "school" => "🏫",
                "meal" => "🍽️",
                "break" => "☕",
                "play" => "🎮",
                "sleep" => "😴",
                _ => "📋"
            };
        }

        private string GetTimeRange()
        {
            if (Activity.StartTime.HasValue && Activity.EndTime.HasValue)
            {
                return $"{Activity.StartTime.Value:HH:mm} - {Activity.EndTime.Value:HH:mm}";
            }
            else if (Activity.StartTime.HasValue)
            {
                return $"Starts at {Activity.StartTime.Value:HH:mm}";
            }
            else if (Activity.EndTime.HasValue)
            {
                return $"Ends at {Activity.EndTime.Value:HH:mm}";
            }
            return string.Empty;
        }

        private async Task HandleMarkAsDone()
        {
            if (OnMarkAsDone.HasDelegate)
            {
                await OnMarkAsDone.InvokeAsync();
            }
        }
    }
}

