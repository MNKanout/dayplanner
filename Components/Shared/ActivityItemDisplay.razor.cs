using dayPlanner.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace dayPlanner.Components.Shared
{
    public partial class ActivityItemDisplay : ComponentBase
    {
        [Parameter] public Activity Activity { get; set; } = null!;
        [Parameter] public bool IsFirst { get; set; }
        [Parameter] public bool IsLast { get; set; }
        [Parameter] public EventCallback<int> OnEdit { get; set; }
        [Parameter] public EventCallback<int> OnDelete { get; set; }
        [Parameter] public EventCallback<int> OnMoveUp { get; set; }
        [Parameter] public EventCallback<int> OnMoveDown { get; set; }

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
                return $"Starts {Activity.StartTime.Value:HH:mm}";
            }
            else if (Activity.EndTime.HasValue)
            {
                return $"Ends {Activity.EndTime.Value:HH:mm}";
            }
            return string.Empty;
        }
    }
}

