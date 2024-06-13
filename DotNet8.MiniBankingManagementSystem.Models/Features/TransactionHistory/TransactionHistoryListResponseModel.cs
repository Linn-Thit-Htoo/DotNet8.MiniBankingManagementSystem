using DotNet8.MiniBankingManagementSystem.Models.Setup.TransactionHistory;

namespace DotNet8.MiniBankingManagementSystem.Models.Features.TransactionHistory;

public class TransactionHistoryListResponseModel
{
    public List<TransactionResponseModel> DataLst { get; set; }
}