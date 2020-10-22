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
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IRepositoryBase<Owner> _repositoryBase;
        private Context Context;
        private readonly ILoggerManager _logger;
        public WeatherForecastController(ILoggerManager logger, Context context)
        {
            Context = context;
            _logger = logger;
            _repositoryBase = new RepositoryBase<Owner>(Context);
        }

        [HttpGet]
        public IEnumerable<Owner> Get()
        {
            _logger.LogInfo($"Get Request!");
            return _repositoryBase.FindAll();
        }
    }
}
