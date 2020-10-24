using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Data;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountOwnerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private readonly ILoggerManager _logger;
        public WeatherForecastController(ILoggerManager logger, IRepositoryWrapper wrapper)
        {
            _logger = logger;
            _wrapper = wrapper;
        }

        [HttpGet]
        public IEnumerable<Owner> Get()
        {
            _logger.LogInfo($"Get Request!");
            return _wrapper.OwnerRepository.FindAll();
        }
    }
}
