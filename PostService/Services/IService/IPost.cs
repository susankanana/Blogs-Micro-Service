using PostService.Models;
using PostService.Models.Dtos;

namespace PostService.Services.IService
{
    public interface IPost
    {
        Task<Post> GetPostById(Guid Id);

        Task<string> AddPost(Post post);

        Task<List<Post>> GetAllPosts(Guid UserId);
        Task<string> UpdatePost ();
        Task<string> DeletePost(Post post);

    }
}
