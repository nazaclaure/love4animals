using Love4AnimalsApi.Data;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Post> GetPosts()
        {
            return _context.Posts.AsNoTracking().ToList();
        }

        public Post? GetPost(long id)
        {
            return _context.Posts.Find(id);
        }

        public Post CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

        public Post? UpdatePost(long id, Post post)
        {
            var existing = _context.Posts.Find(id);
            if (existing == null) return null;
            existing.Description = post.Description;
            existing.ImageURL = post.ImageURL;
            existing.CampaignId = post.CampaignId;
            _context.SaveChanges();
            return existing;
        }

        public bool DeletePost(long id)
        {
            var existing = _context.Posts.Find(id);
            if (existing == null) return false;
            _context.Posts.Remove(existing);
            _context.SaveChanges();
            return true;
        }
    }
}
