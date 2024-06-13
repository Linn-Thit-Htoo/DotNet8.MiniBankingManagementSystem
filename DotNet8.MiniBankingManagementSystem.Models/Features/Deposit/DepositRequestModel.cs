namespace DotNet8.MiniBankingManagementSystem.Models.Features.Deposit;

public class DepositRequestModel
{
    public string AccountNo { get; set; } = null!;
    public decimal Amount { get; set; }
}