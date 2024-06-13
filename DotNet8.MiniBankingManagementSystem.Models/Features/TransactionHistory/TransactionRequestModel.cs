
namespace DotNet8.MiniBankingManagementSystem.Models.Features.TransactionHistory;

public class TransactionRequestModel
{
    public string FromAccountNo { get; set; } = null!;
    public string ToAccountNo { get; set; } = null!;
    public decimal Amount { get; set; }

    public DbService.Models.TransactionHistory Change()
    {
        throw new NotImplementedException();
    }
}