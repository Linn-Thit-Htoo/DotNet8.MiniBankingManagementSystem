using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models;
using DotNet8.MiniBankingManagementSystem.Models.Setup.Township;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Township;

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
        var townships = await _appDbContext.Tbl_Township
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
        List<Tbl_Township> lst = JsonConvert.DeserializeObject<List<Tbl_Township>>(jsonStr)!;
        await _appDbContext.AddRangeAsync(lst);
        int result = await _appDbContext.SaveChangesAsync();

        return result;
    }

    #endregion
}