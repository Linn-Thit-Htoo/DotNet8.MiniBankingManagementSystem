namespace DotNet8.MiniBankingManagementSystem.Models.Setup.TransactionHistory;

public class TransactionHistoryModel
{
    public long TransactionHistoryId { get; set; }
    public string FromAccountNo { get; set; } = null!;
    public string ToAccountNo { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
}