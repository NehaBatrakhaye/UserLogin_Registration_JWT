namespace UserLogin_Registration.Entities;

public class User
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string PasswordHash { get; set; }
    
    public string? Roles{get; set;}
}