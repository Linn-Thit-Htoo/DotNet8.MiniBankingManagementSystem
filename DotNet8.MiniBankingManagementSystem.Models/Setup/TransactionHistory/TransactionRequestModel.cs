namespace DotNet8.MiniBankingManagementSystem.Models.Setup.TransactionHistory
{
    public class TransactionRequestModel
    {
        public string FromAccountNo { get; set; } = null!;
        public string ToAccountNo { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}