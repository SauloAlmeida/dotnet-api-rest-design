using Microsoft.AspNetCore.Mvc;

namespace src.Controllers.Common;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : Controller
{

}
