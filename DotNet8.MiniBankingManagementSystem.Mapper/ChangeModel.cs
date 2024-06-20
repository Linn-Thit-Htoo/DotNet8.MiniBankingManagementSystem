using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models.Features.Account;
using DotNet8.MiniBankingManagementSystem.Models.Features.Deposit;
using DotNet8.MiniBankingManagementSystem.Models.Features.State;
using DotNet8.MiniBankingManagementSystem.Models.Features.Township;
using DotNet8.MiniBankingManagementSystem.Models.Features.TransactionHistory;
using DotNet8.MiniBankingManagementSystem.Models.Features.Withdraw;

namespace DotNet8.MiniBankingManagementSystem.Mapper;

public static class ChangeModel
{
    #region Account

    public static AccountModel Change(this Account dataModel)
    {
        return new AccountModel()
        {
            AccountId = dataModel.AccountId,
            AccountLevel = dataModel.AccountLevel,
            AccountNo = dataModel.AccountNo,
            Balance = dataModel.Balance,
            CustomerCode = dataModel.CustomerCode,
            CustomerName = dataModel.CustomerName,
            IsActive = dataModel.IsActive,
            StateCode = dataModel.StateCode,
            TownshipCode = dataModel.TownshipCode,
        };
    }

    public static Account Change(this AccountRequestModel requestModel)
    {
        return new Account()
        {
            AccountNo = Ulid.NewUlid().ToString(),
            AccountLevel = requestModel.AccountLevel,
            CustomerCode = requestModel.CustomerCode!,
            CustomerName = requestModel.CustomerName,
            Balance = requestModel.Balance,
            StateCode = requestModel.StateCode,
            TownshipCode = requestModel.TownshipCode,
            IsActive = true
        };
    }

    #endregion

    #region State

    public static StateModel Change(this State dataModel)
    {
        return new StateModel()
        {
            StateId = dataModel.StateId,
            StateCode = dataModel.StateCode,
            StateName = dataModel.StateName
        };
    }

    public static TownshipModel Change(this Township dataModel)
    {
        return new TownshipModel()
        {
            TownshipId = dataModel.TownshipId,
            StateCode = dataModel.StateCode,
            TownshipCode = dataModel.TownshipCode
        };
    }

    #endregion

    #region Deposit

    public static DepositModel Change(this Deposit dataModel)
    {
        return new DepositModel()
        {
            DepositId = dataModel.DepositId,
            AccountNo = dataModel.AccountNo,
            Amount = Convert.ToInt32(dataModel.Amount),
            DepositDate = dataModel.DepositDate
        };
    }

    public static Deposit Change(this DepositRequestModel requestModel)
    {
        return new Deposit()
        {
            AccountNo = requestModel.AccountNo,
            Amount = requestModel.Amount,
            DepositDate = DateTime.Now
        };
    }

    #endregion

    #region WithDraw

    public static WithdrawModel Change(this Withdraw dataModel)
    {
        return new WithdrawModel()
        {
            AccountNo = dataModel.AccountNo,
            WithDrawId = dataModel.WithDrawId,
            Amount = Convert.ToInt32(dataModel.Amount),
            WithDrawDate = dataModel.WithDrawDate
        };
    }

    public static Withdraw Change(this WithdrawRequestModel requestModel)
    {
        return new Withdraw()
        {
            AccountNo = requestModel.AccountNo,
            Amount = requestModel.Amount,
            WithDrawDate = DateTime.Now,
        };
    }

    #endregion

    #region Transaction History

    public static TransactionHistoryModel Change(this TransactionHistory dataModel)
    {
        return new TransactionHistoryModel
        {
            Amount = Convert.ToInt32(dataModel.Amount),
            FromAccountNo = dataModel.FromAccountNo,
            ToAccountNo = dataModel.ToAccountNo,
            TransactionDate = dataModel.TransactionDate,
            TransactionHistoryId = dataModel.TransactionHistoryId
        };
    }

    public static TransactionHistory Change(this TransactionRequestModel requestModel)
    {
        return new TransactionHistory
        {
            TransactionHistoryId = Ulid.NewUlid().ToString(),
            FromAccountNo = requestModel.FromAccountNo,
            ToAccountNo = requestModel.ToAccountNo,
            Amount = requestModel.Amount,
            TransactionDate = DateTime.Now
        };
    }

    #endregion
}