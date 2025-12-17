namespace dayPlanner.Data.Entities;

public class Activity
{
    public int Id { get; set; }
    public int DayPlanId { get; set; }
    public int Order { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Location { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
    public string IconKey { get; set; } = string.Empty;
    public string ColorKey { get; set; } = string.Empty;
    public DateTime? CompletedAt { get; set; }
    
    // Navigation property
    public DayPlan DayPlan { get; set; } = null!;
}

