using AutoMapper;
using Contracts;
using Data;
using Data.DataTransferObjects;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var owners = _repository.Owner.GetOwnersOrderByName();
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

        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnerById(Guid Id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(Id);
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
        [HttpGet("{id}/account")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerWithDetails(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with details for id: {id}");

                    var ownerResult = _mapper.Map<OwnerViewModel>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateOwner([FromBody]OwnerCreateDto owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = _mapper.Map<Owner>(owner);
                _repository.Owner.CreateOwner(ownerEntity);
                _repository.Commit();
                var createdOwner = _mapper.Map<OwnerViewModel>(ownerEntity);
                // CreatedAtRoute return 201 for creation so we can see this is a create operation.
                return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody]OwnerUpdateDto owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var ownerEntity = _repository.Owner.GetOwnerById(id);
                if (ownerEntity == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(owner, ownerEntity);
                _repository.Owner.UpdateOwner(ownerEntity);
                _repository.Commit();
                return NoContent(); // return 204 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            try
            {
                var entity = _repository.Owner.GetOwnerById(id);
                if (entity == null)
                {
                    _logger.LogInfo($"This entity not found with id: {id}");
                    return NotFound();
                }
                var accountsByOwner = _repository.Account.AccountsByOwner(id);
                if (accountsByOwner.Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }
                _repository.Owner.Delete(entity);
                _repository.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }
    }
}