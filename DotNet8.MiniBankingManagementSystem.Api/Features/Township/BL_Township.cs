using DotNet8.MiniBankingManagementSystem.Models.Setup.Township;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Township;

public class BL_Township
{
    private readonly DA_Township _dA_Township;

    public BL_Township(DA_Township dA_Township)
    {
        _dA_Township = dA_Township;
    }

    public async Task<TownshipResponseModel> GetTownshipListAsync()
    {
        return await _dA_Township.GetTownshipListAsync();
    }

    public async Task<int> CreateTownshipListAsync()
    {
        int result = await _dA_Township.CreateTownshipListAsync();
        return result;
    }
}