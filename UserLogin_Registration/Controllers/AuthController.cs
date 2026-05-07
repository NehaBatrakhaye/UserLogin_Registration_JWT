using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using UserLogin_Registration.Entities;
using UserLogin_Registration.Model;
using UserLogin_Registration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace UserLogin_Registration.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private static User user = new User();

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<User?>>  Register(UserDTO userDto)
    {
        var user = await _authService.RegisterAsync(userDto);

        if (user == null)
        {
            return BadRequest("User not found");
        }
        else
        {
            return Ok(user);
            
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(UserDTO userDto)
    {
        var token = await _authService.LoginAsync(userDto);
        if (token == null)
        {
            return BadRequest("Invalid Credentials");
        }
        else
        {
            return Ok(token);
        }
        
    }

    [Authorize]
    [HttpGet("Auth-endpoint")]
    public IActionResult AuthCheck()
    {
        return Ok("You are Authorized now");
    }
    
    
    [Authorize(Roles = "Admin, Manager, Employee")]
    [HttpGet("Admin-endpoint")]
    public IActionResult AdminCheck()
    {
        return Ok("You are Authorized now");
    }


   
    
}
