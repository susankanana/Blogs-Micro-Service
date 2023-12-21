using CommentService.Models;

namespace CommentService.Services.IServices
{
    public interface IComment
    {
        Task<Comment> GetComment(Guid Id);
        Task<List<Comment>> GetAllComments(Guid PostId);
        Task<string> DeleteComment(Comment comment);
        Task<string> AddComment(Comment comment);
        Task<string> UpdateComment();   
    }
    
}
