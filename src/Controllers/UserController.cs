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

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateAgeAsync(Guid id, [FromBody] int age)
    {
        await _db.UpdateAgeAsync(id, age);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _db.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("{id:guid}/infos")]
    public async Task<IActionResult> GetUsersInfoByIdAsync(Guid id) => Ok(await _db.GetUsersInfoByIdAsync(id));
}
