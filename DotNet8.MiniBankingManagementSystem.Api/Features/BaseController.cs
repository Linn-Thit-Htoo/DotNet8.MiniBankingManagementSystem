using AspNetCore.Reporting;
using DotNet8.MiniBankingManagementSystem.Models.Enums;
using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Resources;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult Content(object? obj)
    {
        return Ok(JsonConvert.SerializeObject(obj));
    }

    protected IActionResult Accepted(EnumRespType enumRespType)
    {
        switch (enumRespType)
        {
            case EnumRespType.Created:
                return StatusCode(201, MessageResource.SaveSuccess);
            case EnumRespType.Accepted:
                return StatusCode(202, MessageResource.SaveSuccess);
            case EnumRespType.Deleted:
                return StatusCode(202, MessageResource.DeleteSuccess);
            case EnumRespType.None:
            default:
                return BadRequest("Invalid Response Type.");
        }
    }

    protected IActionResult HandleFailure(Exception ex)
    {
        return StatusCode(500, ex.Message);
    }

    public IActionResult ExportReport<T>(ReportModel<T> requestModel)
    {
        try
        {
            string mimetype = string.Empty;
            int extension = 100;
            var path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ReportFiles",
                requestModel.ReportFileName + ".rdlc"
            );
            LocalReport lr = new LocalReport(path);
            lr.AddDataSource(requestModel.DataSetName, requestModel.DataLst);

            if (requestModel.ReportType == EnumFileType.Pdf)
            {
                ReportResult result = lr.Execute(
                    RenderType.Pdf,
                    extension,
                    requestModel.Parameters,
                    mimetype
                );
                return File(
                    result.MainStream,
                    "application/pdf",
                    $"{requestModel.ExportFileName}.pdf"
                );
            }
            else if (requestModel.ReportType == EnumFileType.Excel)
            {
                ReportResult result = lr.Execute(
                    RenderType.Excel,
                    extension,
                    requestModel.Parameters,
                    mimetype
                );
                return File(
                    result.MainStream,
                    "application/msexcel",
                    $"{requestModel.ExportFileName}.xls"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return Ok();
    }
}
