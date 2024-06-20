namespace DotNet8.MiniBankingManagementSystem.Models.Features.Township;

public class TownshipModel
{
    public long TownshipId { get; set; }
    public string TownshipCode { get; set; } = null!;
    public string TownshipName { get; set; } = null!;
    public string StateCode { get; set; } = null!;
}
