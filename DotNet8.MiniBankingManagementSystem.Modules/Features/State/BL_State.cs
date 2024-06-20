using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.State;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.State;

public class BL_State
{
    #region Initializations

    private readonly DA_State _dA_State;

    public BL_State(DA_State dA_State)
    {
        _dA_State = dA_State;
    }

    #endregion

    #region GetStateListAsync

    public async Task<Result<StateListResponseModel>> GetStateListAsync()
    {
        return await _dA_State.GetStateListAsync();
    }

    #endregion

    #region CreateStatesAsync

    public async Task<Result<StateResponseModel>> CreateStatesAsync()
    {
        return await _dA_State.CreateStatesAsync();
    }

    #endregion
}
