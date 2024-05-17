﻿using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models;
using DotNet8.MiniBankingManagementSystem.Models.Setup.Account;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Account;

public class DA_Account
{
    #region Initializations

    private readonly AppDbContext _context;

    public DA_Account(AppDbContext context)
    {
        _context = context;
    }

    #endregion

    #region GetAccountListAsync

    public async Task<AccountListResponseModel> GetAccountListAsync()
    {
        try
        {
            var lst = await _context.Tbl_Account
                .AsNoTracking()
                .OrderByDescending(x => x.AccountId)
                .ToListAsync();

            var accountLst = lst.Select(x => x.Change()).ToList();
            return new AccountListResponseModel()
            {
                DataLst = accountLst
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region CreateAccount

    public async Task<int> CreateAccount(AccountRequestModel requestModel)
    {
        try
        {
            bool isStatePresent = await _context.Tbl_State
                .AsNoTracking()
                .AnyAsync(x => x.StateCode == requestModel.StateCode);
            if (!isStatePresent)
                throw new Exception("State does not exist.");

            bool isTownshipPresent = await _context.Tbl_Township
                .AsNoTracking()
                .AnyAsync(x => x.TownshipCode == requestModel.TownshipCode);
            if (!isTownshipPresent)
                throw new Exception("Township does not exist.");

            var townshipLst = await _context.Tbl_Township
                .AsNoTracking()
                .Where(x => x.StateCode == requestModel.StateCode)
                .ToListAsync();

            bool isTownshipValid = townshipLst.Any(township => township.TownshipCode == requestModel.TownshipCode);
            if (!isTownshipValid)
                throw new Exception("Township is invalid.");

            requestModel.CustomerCode = await GenerateCustomerCodeAsync();
            requestModel.AccountLevel = 1;
            await _context.Tbl_Account.AddAsync(requestModel.Change());
            int result = await _context.SaveChangesAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Generate Customer Code

    private async Task<string> GenerateCustomerCodeAsync()
    {
        var latestCustomerCode = await _context.Tbl_Account
            .AsNoTracking()
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