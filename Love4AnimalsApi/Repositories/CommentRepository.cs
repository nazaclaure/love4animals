using Love4AnimalsApi.Data;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Comment> GetComments(long postId)
        {
            return _context.Comments.AsNoTracking()
                .Where(c => c.PostId == postId).ToList();
        }

        public Comment? GetComment(long postId, long id)
        {
            return _context.Comments
                .FirstOrDefault(c => c.Id == id && c.PostId == postId);
        }

        public Comment CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        public Comment? UpdateComment(long id, Comment comment)
        {
            var existing = _context.Comments.Find(id);
            if (existing == null) return null;
            existing.Content = comment.Content;
            _context.SaveChanges();
            return existing;
        }

        public bool DeleteComment(long id)
        {
            var existing = _context.Comments.Find(id);
            if (existing == null) return false;
            _context.Comments.Remove(existing);
            _context.SaveChanges();
            return true;
        }

        public void DeleteCommentsByPostId(long postId)
        {
            var comments = _context.Comments.Where(c => c.PostId == postId).ToList();
            _context.Comments.RemoveRange(comments);
            _context.SaveChanges();
        }
    }
}
