namespace Love4AnimalsApi.Dtos;
public record UpdateCampaignDto (
    string Name,
    string Description,
    double FundraisingGoal,
    DateTime StartDate,
    DateTime EndDate
);
