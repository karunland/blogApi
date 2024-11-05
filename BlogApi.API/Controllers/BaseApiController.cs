using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[Produces("application/json")]
[Microsoft.AspNetCore.Components.Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class BaseApiController : ControllerBase;
