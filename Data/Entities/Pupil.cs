namespace dayPlanner.Data.Entities;

public class Pupil
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Navigation property - one pupil can have many day plans
    public ICollection<DayPlan> DayPlans { get; set; } = new List<DayPlan>();
}

