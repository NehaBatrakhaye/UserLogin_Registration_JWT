using UserLogin_Registration.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserLogin_Registration.Data;

public class MyDbContext: DbContext
{

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    
}