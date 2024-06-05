namespace DotNet8.MiniBankingManagementSystem.DbService.Models;

public partial class Withdraw
{
    public long WithDrawId { get; set; }

    public string AccountNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime WithDrawDate { get; set; }
}