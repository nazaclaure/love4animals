using Love4AnimalsApi.Data;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public User? GetUser(long id)
        {
            return _context.Users.Find(id);
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? UpdateUser(long id, User user)
        {
            var existing = _context.Users.Find(id);
            if (existing == null) return null;
            existing.Name = user.Name;
            existing.Email = user.Email;
            existing.Password = user.Password;
            existing.ProfilePicture = user.ProfilePicture;
            _context.SaveChanges();
            return existing;
        }

        public bool DeleteUser(long id)
        {
            var existing = _context.Users.Find(id);
            if (existing == null) return false;
            _context.Users.Remove(existing);
            _context.SaveChanges();
            return true;
        }
    }
}
