using DotNet8.MiniBankingManagementSystem.Models.Setup.State;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.State;

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

    public async Task<StateListResponseModel> GetStateListAsync()
    {
        var states = await _appDbContext.States
            .AsNoTracking()
            .OrderByDescending(x => x.StateId)
            .ToListAsync();

        var lst = states.Select(x => x.Change()).ToList();

        return new StateListResponseModel
        {
            DataLst = lst
        };
    }

    #endregion

    #region CreateStatesAsync

    public async Task<int> CreateStatesAsync()
    {
        string jsonStr = await File.ReadAllTextAsync("Data/StateList.json");
        List<DbService.Models.State> lst = JsonConvert.DeserializeObject<List<DbService.Models.State>>(jsonStr)!;
        await _appDbContext.States.AddRangeAsync(lst);
        int result = await _appDbContext.SaveChangesAsync();

        return result;
    }

    #endregion
}