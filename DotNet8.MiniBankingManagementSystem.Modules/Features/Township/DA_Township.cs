using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Mapper;
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

    public async Task<TownshipResponseModel> GetTownshipListAsync()
    {
        var townships = await _appDbContext.Townships
            .AsNoTracking()
            .OrderByDescending(x => x.TownshipId)
            .ToListAsync();

        var lst = townships.Select(x => x.Change()).ToList();

        return new TownshipResponseModel
        {
            DataLst = lst
        };
    }

    #endregion

    #region CreateTownshipListAsync

    public async Task<int> CreateTownshipListAsync()
    {
        string jsonStr = await File.ReadAllTextAsync("Data/TownshipList.json");
        List<DbService.Models.Township> lst = JsonConvert.DeserializeObject<List<DbService.Models.Township>>(jsonStr)!;
        await _appDbContext.AddRangeAsync(lst);
        int result = await _appDbContext.SaveChangesAsync();

        return result;
    }

    #endregion
}