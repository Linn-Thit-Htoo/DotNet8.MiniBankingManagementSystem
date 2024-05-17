using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models.Setup.Account;
using DotNet8.MiniBankingManagementSystem.Models.Setup.Deposit;
using DotNet8.MiniBankingManagementSystem.Models.Setup.State;
using DotNet8.MiniBankingManagementSystem.Models.Setup.Township;
using DotNet8.MiniBankingManagementSystem.Models.Setup.TransactionHistory;
using DotNet8.MiniBankingManagementSystem.Models.Setup.WithDraw;

namespace DotNet8.MiniBankingManagementSystem.Models;

public static class ChangeModel
{
    #region Account

    public static AccountModel Change(this Tbl_Account dataModel)
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

    public static Tbl_Account Change(this AccountRequestModel requestModel)
    {
        return new Tbl_Account()
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

    public static StateModel Change(this Tbl_State dataModel)
    {
        return new StateModel()
        {
            StateId = dataModel.StateId,
            StateCode = dataModel.StateCode,
            StateName = dataModel.StateName
        };
    }

    public static TownshipModel Change(this Tbl_Township dataModel)
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

    public static DepositModel Change(this Tbl_Deposit dataModel)
    {
        return new DepositModel()
        {
            DepositId = dataModel.DepositId,
            AccountNo = dataModel.AccountNo,
            Amount = Convert.ToInt32(dataModel.Amount),
            DepositDate = dataModel.DepositDate
        };
    }

    public static Tbl_Deposit Change(this DepositRequestModel requestModel)
    {
        return new Tbl_Deposit()
        {
            AccountNo = requestModel.AccountNo,
            Amount = requestModel.Amount,
            DepositDate = DateTime.Now
        };
    }

    #endregion

    #region WithDraw

    public static WithDrawModel Change(this Tbl_WithDraw dataModel)
    {
        return new WithDrawModel()
        {
            AccountNo = dataModel.AccountNo,
            WithDrawId = dataModel.WithDrawId,
            Amount = Convert.ToInt32(dataModel.Amount),
            WithDrawDate = dataModel.WithDrawDate
        };
    }

    public static Tbl_WithDraw Change(this WithDrawRequestModel requestModel)
    {
        return new Tbl_WithDraw()
        {
            AccountNo = requestModel.AccountNo,
            Amount = requestModel.Amount,
            WithDrawDate = DateTime.Now,
        };
    }

    #endregion

    #region Transaction History

    public static TransactionHistoryModel Change(this Tbl_TransactionHistory dataModel)
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

    public static Tbl_TransactionHistory Change(this TransactionRequestModel requestModel)
    {
        return new Tbl_TransactionHistory
        {
            FromAccountNo = requestModel.FromAccountNo,
            ToAccountNo = requestModel.ToAccountNo,
            Amount = requestModel.Amount,
            TransactionDate = DateTime.Now
        };
    }
    #endregion
}