using DotNet8.MiniBankingManagementSystem.Models.Setup.Account;

namespace DotNet8.MiniBankingManagementSystem.Models.Features.Account;

public class AccountListResponseModel
{
    public List<AccountModel> DataLst { get; set; }
}