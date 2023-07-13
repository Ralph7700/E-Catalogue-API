using e_catalog_backend.Models;

namespace e_catalog_backend.Repositories.Users;
public interface IUserRepository
{
    Task<User> GetUserById(Guid userId);
    Task<User> CreateUser(User user);
    Task<User> UpdateUser(User user);
    Task DeleteUser(Guid userId);
    Task<List<User>> GetAllSalesmen();
    Task<User> ValidateUser(string username, string password);
}