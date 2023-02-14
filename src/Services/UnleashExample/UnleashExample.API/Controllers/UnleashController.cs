using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Unleash;

namespace UnleashExample.Controllers;

[ApiController]
[Route("[controller]")]
public class UnleashController : ControllerBase
{
    private readonly ILogger<UnleashController> _logger;

    private readonly IUnleash _unleash;

    public UnleashController(IUnleash unleash, ILogger<UnleashController>? logger = null)
    {
        _logger = logger ?? NullLogger<UnleashController>.Instance;

        _unleash = unleash ?? throw new ArgumentNullException(nameof(unleash));
    }

    [HttpGet(Name = "GetUnleash")]
    public ActionResult Get()
    {
        return Ok(_unleash.IsEnabled("ExampleFeature")
            ? "feature flag is enabled"
            : "feature flag is disabled");
    }
}
