using CommentService.Models.Dtos;

namespace CommentService.Services.IServices
{
    public interface IPost
    {
        Task<PostDto> GetPostById(Guid id);
    }
}
