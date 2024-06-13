namespace DotNet8.MiniBankingManagementSystem.Models.Features.WithDraw;

public class WithDrawModel
{
    public long WithDrawId { get; set; }
    public string AccountNo { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime WithDrawDate { get; set; }
}