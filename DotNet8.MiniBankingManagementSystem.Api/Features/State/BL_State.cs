using DotNet8.MiniBankingManagementSystem.Models.Setup.State;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.State
{
    public class BL_State
    {
        private readonly DA_State _dA_State;

        public BL_State(DA_State dA_State)
        {
            _dA_State = dA_State;
        }

        #region GetStateListAsync

        public async Task<StateListResponseModel> GetStateListAsync()
        {
            return await _dA_State.GetStateListAsync();
        }

        #endregion

        public async Task<int> CreateStatesAsync()
        {
            int result = await _dA_State.CreateStatesAsync();
            return result;
        }
    }
}