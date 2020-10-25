using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Data;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var owners = _repository.OwnerRepository.GetOwnersOrderByName();
                var ownersViewModel = _mapper.Map<IEnumerable<OwnerViewModel>>(owners);
                _logger.LogInfo($"Returned all owners from database.");
                return Ok(ownersViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOwnerById(string Id)
        {
            try
            {
                var owner = _repository.OwnerRepository.GetOwnerById(Id);
                if (owner == null)
                {
                    _logger.LogWarn("Owner Not Found!");
                    return NotFound();
                }
                else
                {
                    var ownerViewModel = _mapper.Map<OwnerViewModel>(owner);
                    _logger.LogInfo($"Owner with id: {owner.Id}, hasn't been found in db.");

                    return Ok(ownerViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}