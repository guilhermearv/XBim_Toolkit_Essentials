using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XBim_Toolkit_Essentials.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        [HttpGet]
        [Route("{filename}")]
        public async Task<IActionResult> Index(string filename)
        {
            try
            {
                var stream = System.IO.File.OpenRead(Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Local\Temp\XBim_Toolkit_Essentials\") + filename);

                FileStreamResult result = new FileStreamResult(stream, "application/octet-stream")
                {
                    FileDownloadName = filename
                };
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                var item = new { Error = e.Message, Trace = e.StackTrace };
                return StatusCode(StatusCodes.Status500InternalServerError, item);
            }
        }
    }
}
