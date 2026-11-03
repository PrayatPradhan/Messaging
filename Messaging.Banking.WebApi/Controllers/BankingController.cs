using Messaging.Banking.Application.Interfaces;
using Messaging.Banking.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Banking.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BankingController : Controller
{
    private readonly IAccountService _accountService;

    public BankingController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Account>> Get()
    {
        return Ok(_accountService.GetAccounts());   
    }
}
