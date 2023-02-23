using Microsoft.AspNetCore.Mvc;
using src.Controllers.Common;

namespace DotnetAPIRestDesign.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{
    public UserController()
    {
    }
}
