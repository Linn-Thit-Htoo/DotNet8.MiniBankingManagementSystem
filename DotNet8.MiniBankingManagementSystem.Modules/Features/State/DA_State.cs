using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Mapper;
using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.State;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.State;

public class DA_State
{
    #region Initializations

    private readonly AppDbContext _appDbContext;

    public DA_State(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #endregion

    #region GetStateListAsync

    public async Task<Result<StateListResponseModel>> GetStateListAsync()
    {
        Result<StateListResponseModel> responseModel;
        try
        {
            var states = await _appDbContext.States
            .AsNoTracking()
            .OrderByDescending(x => x.StateId)
            .ToListAsync();

            var lst = states.Select(x => x.Change()).ToList();
            var model = new StateListResponseModel
            {
                DataLst = lst
            };

            responseModel = Result<StateListResponseModel>.SuccessResult(model);
        }
        catch (Exception ex)
        {
            responseModel = Result<StateListResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    #endregion

    #region CreateStatesAsync

    public async Task<Result<StateResponseModel>> CreateStatesAsync()
    {
        Result<StateResponseModel> responseModel;
        try
        {
            string jsonStr = await File.ReadAllTextAsync("Data/StateList.json");
            List<DbService.Models.State> lst = JsonConvert.DeserializeObject<List<DbService.Models.State>>(jsonStr)!;
            await _appDbContext.States.AddRangeAsync(lst);
            int result = await _appDbContext.SaveChangesAsync();

            responseModel = Result<StateResponseModel>.ExecuteResult(result);
        }
        catch (Exception ex)
        {
            responseModel = Result<StateResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    #endregion
}