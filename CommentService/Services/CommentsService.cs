using CommentService.Data;
using CommentService.Models;
using CommentService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Services
{
    public class CommentsService : IComment
    {
        private readonly ApplicationDbContext _context;
        public CommentsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return "Comment Added!";
        }

        public async Task<string> DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return "Comment Removed!";
        }

        public async Task<List<Comment>> GetAllComments(Guid PostId)
        {
            return await _context.Comments.Where(x => x.PostId==PostId).ToListAsync();
        }

        public async Task<Comment> GetComment(Guid Id)
        {
            return await _context.Comments.Where(x => x.CommentId == Id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateComment()
        {
            await _context.SaveChangesAsync();
            return "Comment Updated!";
        }
    }
}
