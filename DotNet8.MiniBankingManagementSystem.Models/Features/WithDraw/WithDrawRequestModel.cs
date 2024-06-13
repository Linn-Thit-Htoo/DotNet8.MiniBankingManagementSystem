namespace DotNet8.MiniBankingManagementSystem.Models.Features.WithDraw;

public class WithDrawRequestModel
{
    public string AccountNo { get; set; } = null!;
    public decimal Amount { get; set; }
}