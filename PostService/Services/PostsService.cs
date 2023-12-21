using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PostService.Data;
using PostService.Models;
using PostService.Models.Dtos;
using PostService.Services.IService;
using static Azure.Core.HttpHeader;

namespace PostService.Services
{
    public class PostsService : IPost
    {
        private readonly ApplicationDbContext _context;
        public PostsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return "Post Added";
        }

        public async Task<string> DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return "Post deleted successfully";
        }

        public async Task<List<Post>> GetAllPosts(Guid UserId)
        {
            return await _context.Posts.Where(x => x.UserId == UserId).ToListAsync();

        }

        public async Task<Post> GetPostById(Guid Id)
        {
            return await _context.Posts.Where(x => x.PostId == Id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdatePost()
        {
            await _context.SaveChangesAsync();
            return "Post updated successfully";
        }
    }
}
