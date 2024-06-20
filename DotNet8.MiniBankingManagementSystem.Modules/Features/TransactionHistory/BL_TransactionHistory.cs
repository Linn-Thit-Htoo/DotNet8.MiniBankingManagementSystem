using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.TransactionHistory;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.TransactionHistory;

public class BL_TransactionHistory
{
    #region Initializations

    private readonly DA_TransactionHistory _dA_TransactionHistory;

    public BL_TransactionHistory(DA_TransactionHistory dA_TransactionHistory)
    {
        _dA_TransactionHistory = dA_TransactionHistory;
    }

    #endregion

    #region Get Transaction History List By Account No Async

    public async Task<
        Result<TransactionHistoryListResponseModel>
    > GetTransactionHistoryListByAccountNoAsync(string accountNo)
    {
        if (string.IsNullOrWhiteSpace(accountNo))
            throw new Exception("Account No cannot be empty.");

        return await _dA_TransactionHistory.GetTransactionHistoryListByAccountNoAsync(accountNo);
    }

    #endregion

    #region Create Transaction Async

    public async Task<Result<TransactionResponseModel>> CreateTransactionAsync(
        TransactionRequestModel requestModel
    )
    {
        return await _dA_TransactionHistory.CreateTransactionAsync(requestModel);
    }

    #endregion
}
