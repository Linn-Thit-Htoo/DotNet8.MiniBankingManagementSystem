using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Mapper;
using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.Township;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.Township;

public class DA_Township
{
    #region Initializations

    private readonly AppDbContext _appDbContext;

    public DA_Township(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #endregion

    #region GetTownshipListAsync

    public async Task<Result<TownshipListResponseModel>> GetTownshipListAsync()
    {
        Result<TownshipListResponseModel> responseModel;
        try
        {
            var townships = await _appDbContext
                .Townships.AsNoTracking()
                .OrderByDescending(x => x.TownshipId)
                .ToListAsync();

            var lst = townships.Select(x => x.Change()).ToList();
            var model = new TownshipListResponseModel { DataLst = lst };

            responseModel = Result<TownshipListResponseModel>.SuccessResult(model);
        }
        catch (Exception ex)
        {
            responseModel = Result<TownshipListResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    #endregion

    #region CreateTownshipListAsync

    public async Task<Result<TownshipResponseModel>> CreateTownshipListAsync()
    {
        Result<TownshipResponseModel> responseModel;
        try
        {
            string jsonStr = await File.ReadAllTextAsync("Data/TownshipList.json");
            List<DbService.Models.Township> lst = JsonConvert.DeserializeObject<
                List<DbService.Models.Township>
            >(jsonStr)!;
            await _appDbContext.AddRangeAsync(lst);
            int result = await _appDbContext.SaveChangesAsync();

            responseModel = Result<TownshipResponseModel>.ExecuteResult(result);
        }
        catch (Exception ex)
        {
            responseModel = Result<TownshipResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    #endregion
}
