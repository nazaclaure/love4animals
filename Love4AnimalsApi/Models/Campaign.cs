namespace Love4AnimalsApi.Models;
public class Campaign
{
    public Campaign(long Id, string Name, string Description, double FundraisingGoal, double TotalRaised, DateTime StartDate, DateTime EndDate)
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.FundraisingGoal = FundraisingGoal;
        this.TotalRaised = TotalRaised;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
    }
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double FundraisingGoal { get; set; }
    public double TotalRaised { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
