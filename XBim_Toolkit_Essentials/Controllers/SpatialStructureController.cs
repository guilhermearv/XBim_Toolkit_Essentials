﻿using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using XBim_Toolkit_Essentials.Models.Resource;

namespace XBim_Toolkit_Essentials.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpatialStructureController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                SpatialStructure spatialStructure = new SpatialStructure();
                spatialStructure.Get();

                var item = JsonConvert.SerializeObject(spatialStructure.ListSpatialStructureViewModel);
                return new OkObjectResult(item);
            }
            catch (Exception e)
            {
                var item = new { Error = e };
                return StatusCode(500, item);
            }
            
        }
    }
}