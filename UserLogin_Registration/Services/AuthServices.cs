using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserLogin_Registration.Data;
using UserLogin_Registration.Entities;
using UserLogin_Registration.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace UserLogin_Registration.Services;

public class AuthServices: IAuthService
{
    private readonly IConfiguration _configuration;
    
    private readonly MyDbContext _context;

    public AuthServices(IConfiguration configuration, MyDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public  async Task<User?> RegisterAsync(UserDTO userdto)
    {
        if (await _context.Users.AnyAsync(x => x.Username == userdto.Username))
        {
            return null;
        }

        var user = new User();
        user.Username = userdto.Username;
        user.Roles = "User";
        
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, userdto.Password);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return (user);

        
    }

    public async Task<string> LoginAsync(UserDTO userdto)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userdto.Username);

        if (user == null)
        {
            return null;
        }

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userdto.Password) ==
            PasswordVerificationResult.Failed)
        {
            return null;
        }

        string token = CreateToken(user);
        return (token);
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>

        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Roles)
        };
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!)
            
        );


        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
            audience: _configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds

        );

        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

    }


    public async Task<List<User>> GetAllUser()

    {
        return await _context.Users.ToListAsync();
    }
    
    public async Task<User?> GetUserIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        
    }

    public async Task<User?> UpdateUserAsync(UserDTO userdto, int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return null;
        }
        
        user.Username = userdto.Username;
        
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
}

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDTO userdto);
    
    Task<string> LoginAsync(UserDTO userdto);
    
    
    Task<List<User>> GetAllUser();
    
    Task<User?> GetUserIdAsync(int id);
    
    Task<User?> UpdateUserAsync(UserDTO userdto, int id);
    
    Task<bool> DeleteUserAsync(int id);
    
    
}