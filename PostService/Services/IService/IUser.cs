using PostService.Models;
using PostService.Models.Dtos;

namespace PostService.Services.IService
{
    public interface IUser
    {
        Task<UserDto> GetUserById(Guid Id);
    }
}
