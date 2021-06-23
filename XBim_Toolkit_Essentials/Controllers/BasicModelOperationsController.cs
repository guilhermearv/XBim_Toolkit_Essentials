using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using XBim_Toolkit_Essentials.Models.Resource;

namespace XBim_Toolkit_Essentials.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasicModelOperationsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string globalId)
        {
            try
            {
                BasicModelOperationsResource basicModelOperationsResource = new BasicModelOperationsResource();
                basicModelOperationsResource.GlobalId = globalId;
                basicModelOperationsResource.Retrieve();

                var item = JsonConvert.SerializeObject(basicModelOperationsResource.BasicModelOperationsViewModel);
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
