using System;
using System.Collections.Generic;
using System.Text.Json;
using AngularClient.Models;
using AngularClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AngularClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodedValuesController : ControllerBase
    {    
        readonly CodedValuesService codedValuesService;
        private readonly ILogger _logger;

        public CodedValuesController(IConfiguration configuration, ILogger<CodedValuesController> logger)
        {
            codedValuesService = new CodedValuesService(configuration.GetConnectionString("OctobridgeDatabase"));
            _logger = logger;
        }

        // GET: api/codedvalues?groupName=OrderStatus
        [HttpGet]
        public ActionResult<IEnumerable<CodedValue>> GetCodedValue([FromQuery] string groupName)
        {
            List<CodedValue> codedValues;

            try
            {
                codedValues = codedValuesService.GetCodedValuesByName(groupName);

                // Log 
                string cvJson = JsonSerializer.Serialize(codedValues);
                _logger.LogInformation($"CodedValues (Get): {cvJson}");

                return Ok(codedValues);
            }
            catch (Exception exc)
            {
                return BadRequest($"Error in retrieving customers. Source: {exc.Source}. Message: {exc.Message}.");
            }
        }

    }
}
