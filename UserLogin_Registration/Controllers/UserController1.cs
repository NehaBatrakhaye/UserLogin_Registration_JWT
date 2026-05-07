using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserLogin_Registration.Entities;
using UserLogin_Registration.Model;
using UserLogin_Registration.Services;

namespace UserLogin_Registration.Controllers;


[Route("api/[controller]")]
[ApiController]


public class UserController1 : ControllerBase
{
    
    
    private readonly IAuthService _authService;
    
    public UserController1(IAuthService authService)
    {
        _authService = authService;
    }
    // GET
   
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _authService.GetAllUser();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _authService.GetUserIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(UserDTO user, int id)
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdFromToken != id.ToString())
        {
            return Forbid();
        }

        var userUpdate = await _authService.UpdateUserAsync(user, id);

        if (user == null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        if (role != "Admin" && userIdFromToken != id.ToString())
            return Forbid();

        var result = await _authService.DeleteUserAsync(id);

        if (!result)
        {
            return NotFound();
        }
        return Ok("User Deleted Successfully!");
    }
}