namespace DotNet8.MiniBankingManagementSystem.Api.Features.Reports.TransactionHistoryReport;

[Route("api/[controller]")]
[ApiController]
public class TransactionHistoryReportController : BaseController
{
    private readonly DapperService _dapperService;

    public TransactionHistoryReportController(DapperService dapperService)
    {
        _dapperService = dapperService;
    }

    [HttpGet("report")]
    public async Task<IActionResult> ExecuteAsync(string accountNo)
    {
        var parameters = new { @FromAccountNo = accountNo };
        var lst = await _dapperService.QueryAsync<TransactionHistoryReportModel>(
            "Sp_GetTransactionHistoryListByAccountNo",
            parameters,
            CommandType.StoredProcedure
        );

        var model = new ReportModel<TransactionHistoryReportModel>()
        {
            ReportFileName = "TransactionHistoryReport",
            ExportFileName = Guid.NewGuid().ToString(),
            ReportType = EnumFileType.Pdf,
            Parameters = null,
            DataLst = lst,
        };

        return ExportReport(model);
    }
}
