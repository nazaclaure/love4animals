namespace Love4AnimalsApi.Dtos;
public record CreateCampaignDto (
    string Name,
    string Description,
    double FundraisingGoal,
    DateTime StartDate,
    DateTime EndDate
);
