using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Mapper;
using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.Account;
using DotNet8.MiniBankingManagementSystem.Models.Resources;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.Account;

public class DA_Account
{
    #region Initializations

    private readonly AppDbContext _context;

    public DA_Account(AppDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Get Account List Async

    public async Task<Result<AccountListResponseModel>> GetAccountListAsync()
    {
        Result<AccountListResponseModel> responseModel;
        try
        {
            var lst = await _context
                .Accounts.AsNoTracking()
                .OrderByDescending(x => x.AccountId)
                .ToListAsync();

            var accountLst = lst.Select(x => x.Change()).ToList();
            var model = new AccountListResponseModel { DataLst = accountLst };

            responseModel = Result<AccountListResponseModel>.SuccessResult(
                model,
                MessageResource.Success
            );
        }
        catch (Exception ex)
        {
            responseModel = Result<AccountListResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    #endregion

    #region Create Account

    public async Task<Result<AccountResponseModel>> CreateAccount(AccountRequestModel requestModel)
    {
        Result<AccountResponseModel> responseModel;
        try
        {
            bool isStatePresent = await _context
                .States.AsNoTracking()
                .AnyAsync(x => x.StateCode == requestModel.StateCode);
            if (!isStatePresent)
                throw new Exception("State does not exist.");

            bool isTownshipPresent = await _context
                .Townships.AsNoTracking()
                .AnyAsync(x => x.TownshipCode == requestModel.TownshipCode);
            if (!isTownshipPresent)
                throw new Exception("Township does not exist.");

            var townshipLst = await _context
                .Townships.AsNoTracking()
                .Where(x => x.StateCode == requestModel.StateCode)
                .ToListAsync();

            bool isTownshipValid = townshipLst.Any(township =>
                township.TownshipCode == requestModel.TownshipCode
            );
            if (!isTownshipValid)
                throw new Exception("Township is invalid.");

            requestModel.CustomerCode = await GenerateCustomerCodeAsync();
            requestModel.AccountLevel = 1;
            await _context.Accounts.AddAsync(requestModel.Change());
            int result = await _context.SaveChangesAsync();

            responseModel = Result<AccountResponseModel>.ExecuteResult(result);
        }
        catch (Exception ex)
        {
            responseModel = Result<AccountResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    #endregion

    #region Generate Customer Code

    private async Task<string> GenerateCustomerCodeAsync()
    {
        var latestCustomerCode = await _context
            .Accounts.AsNoTracking()
            .OrderByDescending(a => a.CustomerCode)
            .Select(a => a.CustomerCode)
            .FirstOrDefaultAsync();

        string newCustomerCode;

        if (string.IsNullOrEmpty(latestCustomerCode))
        {
            newCustomerCode = "C00001";
        }
        else
        {
            int numericPart = int.Parse(latestCustomerCode.Substring(1)) + 1;
            newCustomerCode = $"C{numericPart:D5}";
        }

        return newCustomerCode;
    }

    #endregion
}
