﻿using FreightManagement.Api.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FreightManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FreightRegionController : ControllerBase
    {
        public IActionResult Index()
        {
            var freightRegions = Enum.GetValues(typeof(EFreightRegion)).Cast<int>().Select(it => new
            {
                Id = it,
                Name = Enum.GetName(typeof(EFreightRegion), it)
            });

            return Ok(freightRegions);
        }
    }
}