using Newtonsoft.Json;
using PostService.Models.Dtos;
using PostService.Services.IService;

namespace PostService.Services
{

    public class CommentService : IComment
    {
        private readonly IHttpClientFactory _HttpClientFactory;
        public CommentService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }

        public async Task<List<CommentDto>> GetCommentsOfPost(Guid postId)
        {
            var client = _HttpClientFactory.CreateClient("Comments");
            var response = await client.GetAsync(postId.ToString());
            var content = await response.Content.ReadAsStringAsync();//string
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CommentDto>>(Convert.ToString(responseDto.Result));
            }
            return new List<CommentDto>();
        }
    }

}
