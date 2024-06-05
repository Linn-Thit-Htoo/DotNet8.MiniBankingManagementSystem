namespace DotNet8.MiniBankingManagementSystem.DbService.Models;

public partial class Deposit
{
    public long DepositId { get; set; }

    public string AccountNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime DepositDate { get; set; }
}
