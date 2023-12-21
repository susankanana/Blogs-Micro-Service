using CommentService.Models.Dtos;
using CommentService.Services.IServices;
using Newtonsoft.Json;

namespace CommentService.Services
{
    public class PostService : IPost
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PostService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<PostDto> GetPostById(Guid id) 
        { 
      
            var client = _httpClientFactory.CreateClient("Posts");
            var response = await client.GetAsync(id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PostDto>(responseDto.Result.ToString());
            }
            return new PostDto();
        }
    }
}
