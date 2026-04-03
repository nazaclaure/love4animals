using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Interfaces;
public interface IUserRepository
{
    List<User> GetUsers();
    User? GetUser(long id);
    User CreateUser(User user);
    User? UpdateUser(long id, User user);
    bool DeleteUser(long id);
}
