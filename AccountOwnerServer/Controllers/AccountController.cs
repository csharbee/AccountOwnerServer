using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Data;
using Data.DataTransferObjects;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILoggerManager _logger;
        public AccountController(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAccounts()
        {
            var model = new BaseModel() { Success = false, Message = "Get All Accounts Information!", InternalMessage = "This message for front-end developers" };
            try
            {
                var accounts = _repository.Account.GetAccounts();
                var accountViewModels = _mapper.Map<IEnumerable<AccountViewModel>>(accounts);
                model.Data = accountViewModels;
                model.Success = true;
                return Ok(model);
            }
            catch (Exception ex)
            {
                model.InternalMessage = ex.Message;
                model.Message = "There is an error occured";
                _logger.LogError($"Error:{ex.Message}");

                return StatusCode(500, model);
            }
        }
        [HttpGet("{id}", Name = "GetAccount")]
        public IActionResult GetAccount(Guid id)
        {
            var model = new BaseModel() { Success = false, Message = "Get All Accounts Information!", InternalMessage = "This message for front-end developers" };
            try
            {
                var account = _repository.Account.GetAccountById(id);
                if (account == null)
                {
                    model.Success = false;
                    return NotFound(model);
                }

                model.Data = _mapper.Map<AccountViewModel>(account);
                model.Success = true;
                return Ok(model);
            }
            catch (Exception ex)
            {
                model.InternalMessage = ex.Message;
                model.Message = $"An Error Occured When Get Repuest With Id:{id}";
                return StatusCode(500, model);
            }
        }
        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountCreateDto accountCreateDto)
        {
            try
            {
                if (accountCreateDto == null)
                {
                    return BadRequest("Account is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid account");
                }
                var account = _mapper.Map<Account>(accountCreateDto);
                _repository.Account.CreateAccount(account);
                _repository.Commit();
                var accountViewModel = _mapper.Map<AccountViewModel>(account);

                return CreatedAtRoute("GetAccount", new { id = account.Id }, accountViewModel);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAccount(Guid id, [FromBody] AccountUpdateDto accountUpdateDto)
        {
            try
            {
                if (accountUpdateDto == null)
                {
                    return BadRequest("accountUpdateDto is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("accountUpdateDto is not valid");
                }
                var account = _repository.Account.GetAccountById(id);
                if (account == null)
                {
                    return NotFound($"Entity is not found by id: {id}");
                }
                _mapper.Map(accountUpdateDto, account);
                _repository.Account.UpdateAccount(account);
                _repository.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}