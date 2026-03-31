using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
namespace Love4AnimalsApi.Repositories;
public class UserRepository : IUserRepository
{
    private List<User> Users { get; set; }
    public UserRepository()
    {
        this.Users = [];
        User newUser = new(1, "Nazaret", "nazaret@gmail.com", "1234", "https://picsum.photos/200");
        this.Users.Add(newUser);
    }
    public List<User> GetUsers()
    {
        return this.Users;
    }
    public User GetUser(long id)
    {
        return this.Users.First(u => u.Id == id);
    }
    public User CreateUser(User user)
    {
        user.Id = this.Users.Any() ? this.Users.Max(u => u.Id) + 1 : 1;
        this.Users.Add(user);
        return user;
    }
    public User UpdateUser(long id, User user)
    {
        User existingUser = this.Users.First(u => u.Id == id);
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
        existingUser.ProfilePicture = user.ProfilePicture;
        return existingUser;
    }
    public void DeleteUser(long id)
    {
        User existingUser = this.Users.First(u => u.Id == id);
        this.Users.Remove(existingUser);
    }
}
