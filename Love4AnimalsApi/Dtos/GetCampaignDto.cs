namespace Love4AnimalsApi.Dtos;
public record GetCampaignDto (
    long Id,
    string Name,
    string Description,
    double FundraisingGoal,
    double TotalRaised,
    DateTime StartDate,
    DateTime EndDate
);
