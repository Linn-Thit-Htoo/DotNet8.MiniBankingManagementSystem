namespace DotNet8.MiniBankingManagementSystem.Models.Features.Withdraw;

public class WithdrawModel
{
    public long WithDrawId { get; set; }
    public string AccountNo { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime WithDrawDate { get; set; }
}
