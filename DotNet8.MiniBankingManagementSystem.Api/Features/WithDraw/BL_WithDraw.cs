using DotNet8.MiniBankingManagementSystem.Models.Setup.WithDraw;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.WithDraw;

public class BL_WithDraw
{
    #region MyRegion

    #endregion
    private readonly DA_WithDraw _dA_WithDraw;

    public BL_WithDraw(DA_WithDraw dA_WithDraw)
    {
        _dA_WithDraw = dA_WithDraw;
    }

    #region GetWithDrawListByAccountNoAsync

    public async Task<WithDrawListResponseModel> GetWithDrawListByAccountNoAsync(string accountNo)
    {
        if (string.IsNullOrWhiteSpace(accountNo))
            throw new Exception("Account No cannot be empty.");

        return await _dA_WithDraw.GetWithDrawListByAccountNoAsync(accountNo);
    }

    #endregion

    #region CreateWithDrawAsync

    public async Task<bool> CreateWithDrawAsync(WithDrawRequestModel requestModel)
    {
        if (string.IsNullOrWhiteSpace(requestModel.AccountNo))
            throw new Exception("Account No cannot be empty.");

        if (requestModel.Amount <= 0)
            throw new Exception("Amount cannot be empty.");

        return await _dA_WithDraw.CreateWithDrawAsync(requestModel);
    }

    #endregion
}