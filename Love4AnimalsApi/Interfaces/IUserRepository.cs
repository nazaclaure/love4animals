using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Interfaces;
public interface IUserRepository
{
    public List<User> GetUsers();
    public User GetUser(long id);
    public User CreateUser(User user);
    public User UpdateUser(long id, User user);
    public void DeleteUser(long id);
}
