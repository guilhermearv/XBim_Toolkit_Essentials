using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using XBim_Toolkit_Essentials.Models.Resource;

namespace XBim_Toolkit_Essentials.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcelSpaceReportFromIfcController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            try
            {
                ExcelSpaceReportFromIfcResource excelSpaceReportFromIfcResource = new ExcelSpaceReportFromIfcResource();
                excelSpaceReportFromIfcResource.GenerateExcel();

                //var item = JsonConvert.SerializeObject(basicModelOperationsResource.BasicModelOperationsViewModel);
                var item = new { Result = "OK" };
                return new OkObjectResult(item);
            }
            catch (Exception e)
            {
                var item = new { Error = e.Message, Trace = e.StackTrace };
                return StatusCode(StatusCodes.Status500InternalServerError, item);
            }

        }
    }
}
