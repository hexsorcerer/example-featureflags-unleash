using Microsoft.AspNetCore.Mvc;

namespace UnleashExample.Controllers;

[ApiController]
[Route("[controller]")]
public class UnleashController : ControllerBase
{
    private readonly ILogger<UnleashController> _logger;

    public UnleashController(ILogger<UnleashController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUnleash")]
    public ActionResult Get()
    {
        return Ok("unleash");
    }
}
