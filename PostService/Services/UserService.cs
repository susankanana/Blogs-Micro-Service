using Newtonsoft.Json;
using PostService.Data;
using PostService.Models;
using PostService.Models.Dtos;
using PostService.Services.IService;
using static System.Net.Mime.MediaTypeNames;

namespace PostService.Services
{
    public class UserService : IUser
    {
        private readonly IHttpClientFactory _HttpClientFactory;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }
            

        public async Task<UserDto> GetUserById(Guid Id)
        {
            var client = _HttpClientFactory.CreateClient("Users");
            var response = await client.GetAsync($"{Id}");
            var content = await response.Content.ReadAsStringAsync();//string
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserDto>(Convert.ToString(responseDto.Result));
            }
            return new UserDto();
        }
    }
}

