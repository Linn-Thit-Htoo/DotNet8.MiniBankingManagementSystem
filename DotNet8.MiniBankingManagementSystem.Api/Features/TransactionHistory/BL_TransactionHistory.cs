using DotNet8.MiniBankingManagementSystem.Models.Setup.TransactionHistory;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.TransactionHistory;

public class BL_TransactionHistory
{
    private readonly DA_TransactionHistory _dA_TransactionHistory;

    public BL_TransactionHistory(DA_TransactionHistory dA_TransactionHistory)
    {
        _dA_TransactionHistory = dA_TransactionHistory;
    }

    #region GetTransactionHistoryListByAccountNoAsync

    #endregion
    public async Task<TransactionHistoryListResponseModel> GetTransactionHistoryListByAccountNoAsync(string accountNo)
    {
        if (string.IsNullOrWhiteSpace(accountNo))
            throw new Exception("Account No cannot be empty.");

        return await _dA_TransactionHistory.GetTransactionHistoryListByAccountNoAsync(accountNo);
    }

    public async Task<bool> CreateTransactionAsync(TransactionRequestModel requestModel)
    {
        if (string.IsNullOrEmpty(requestModel.FromAccountNo))
            throw new Exception("From Account No cannot be empty.");

        if (string.IsNullOrEmpty(requestModel.ToAccountNo))
            throw new Exception("From Account No cannot be empty.");

        if (requestModel.Amount <= 0)
            throw new Exception("Amount cannot be empty.");

        return await _dA_TransactionHistory.CreateTransactionAsync(requestModel);
    }
}