namespace DotNet8.MiniBankingManagementSystem.Api.Features.Township;

public class BL_Township
{
    #region Initializations

    private readonly DA_Township _dA_Township;

    public BL_Township(DA_Township dA_Township)
    {
        _dA_Township = dA_Township;
    }

    #endregion

    #region GetTownshipListAsync

    public async Task<TownshipResponseModel> GetTownshipListAsync()
    {
        return await _dA_Township.GetTownshipListAsync();
    }

    #endregion

    #region CreateTownshipListAsync

    public async Task<int> CreateTownshipListAsync()
    {
        int result = await _dA_Township.CreateTownshipListAsync();
        return result;
    }

    #endregion
}