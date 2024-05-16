using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models.Setup.Account;

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
}