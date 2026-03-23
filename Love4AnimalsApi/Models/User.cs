/*using System;
namespace Love4AnimalsApi.Models;
public class User
{
    public User (int Id, string Name, string Email)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
*/
namespace Love4AnimalsApi.Models;
public class User
{
    public User (long Id, string Name, string Email, string Password, string ProfilePicture)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.Password = Password;
        this.ProfilePicture = ProfilePicture;
    }
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
}