using Microsoft.AspNetCore.Mvc;
using src.Controllers.Common;
using src.Database;
using src.DTO;

namespace DotnetAPIRestDesign.Controllers;

public class UserController : BaseController
{
    private readonly IDatabase _db;

    public UserController(IDatabase db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync() => Ok(await _db.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(await _db.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] UserInput input)
    {
        await _db.CreateAsync(input);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UserInput input)
    {
        await _db.UpdateAsync(id, input);
        return Ok();
    }
}
