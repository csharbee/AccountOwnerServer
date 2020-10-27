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
            try
            {
                var accounts = _repository.Account.GetAccounts();
                return Ok(accounts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("{id}", Name = "GetAccount")]
        public IActionResult GetAccount(Guid id)
        {
            try
            {
                var account = _repository.Account.GetAccountById(id);
                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
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