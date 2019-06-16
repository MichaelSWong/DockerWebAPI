using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;

        public AccountController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _repository = repository;
            _logger = logger;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllAcounts()
        {
            try
            {
                var accounts = await _repository.Account.GetAllAccountsAsync();

                _logger.LogInfo($"Returned all accounts from the database.");

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody]Account account)
        {
            try
            {
                if (account.IsObjectNull())
                {
                    _logger.LogError("Object sent from client is null.");
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client.");
                    return BadRequest("Invalid model object");
                }

               await _repository.Account.CreateAccountAsync(account);

                return CreatedAtRoute("AccountById", new { id = account.Id }, account);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAccount action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
