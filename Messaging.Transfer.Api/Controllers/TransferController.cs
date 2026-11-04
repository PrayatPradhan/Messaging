using Messagin.Transfer.Application.Interfaces;
using Messaging.Transfer.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Transfer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransferController : Controller
{
    private readonly ITransferService _transferService;

    public TransferController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TransferLog>> Get()
    {
        return Ok(_transferService.GetTransferLogs());
    }
}
