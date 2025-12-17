namespace dayPlanner.Data.Entities;

public class DayPlan
{
    public int Id { get; set; }
    public int PupilId { get; set; }
    public DateOnly Date { get; set; }
    public string? Title { get; set; }
    
    // Navigation properties
    public Pupil Pupil { get; set; } = null!;
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}

