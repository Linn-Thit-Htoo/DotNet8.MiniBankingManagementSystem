namespace DotNet8.MiniBankingManagementSystem.Models.Features.Withdraw;

public class WithdrawRequestModel
{
    public string AccountNo { get; set; } = null!;
    public decimal Amount { get; set; }
}