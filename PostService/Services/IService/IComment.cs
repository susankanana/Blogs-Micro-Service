using PostService.Models.Dtos;

namespace PostService.Services.IService
{
    public interface IComment
    {
        Task<List<CommentDto>> GetCommentsOfPost(Guid postId);
    }
}
