using e_catalog_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace e_catalog_backend.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly MainDbContext _context;
    public UserRepository(MainDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUser(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if(user!= null) user.IsDeleted = true;
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<List<User>> GetAllSalesmen()
    {
        var users = await _context.Users.Where(u => u.Role == Role.Salesman && u.IsDeleted != true).ToListAsync();
        return users;
    }
    
    public async Task<User> GetUserById(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user;
    }
    
    public async Task<User> ValidateUser(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        return user;
    }
}