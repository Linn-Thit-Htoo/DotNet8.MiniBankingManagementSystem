using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models;
using DotNet8.MiniBankingManagementSystem.Models.Setup.State;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.State
{
    public class DA_State
    {
        private readonly AppDbContext _appDbContext;

        public DA_State(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<StateListResponseModel> GetStateListAsync()
        {
            var states = await _appDbContext.Tbl_State
                .AsNoTracking()
                .OrderByDescending(x => x.StateId)
                .ToListAsync();

            var lst = states.Select(x => x.Change()).ToList();

            return new StateListResponseModel()
            {
                DataLst = lst
            };
        }

        public async Task<int> CreateStatesAsync()
        {
            string jsonStr = await File.ReadAllTextAsync("Data/StateList.json");
            List<Tbl_State> lst = JsonConvert.DeserializeObject<List<Tbl_State>>(jsonStr)!;
            await _appDbContext.Tbl_State.AddRangeAsync(lst);
            int result = await _appDbContext.SaveChangesAsync();

            return result;
        }
    }
}
