namespace DotNet8.MiniBankingManagementSystem.Models.Features.TransactionHistory;

public class TransactionHistoryModel
{
    public string TransactionHistoryId { get; set; } = null!;
    public string FromAccountNo { get; set; } = null!;
    public string ToAccountNo { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
}
