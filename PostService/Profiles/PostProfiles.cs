using AutoMapper;
using PostService.Models;
using PostService.Models.Dtos;

namespace PostService.Profiles
{
    public class PostProfiles : Profile
    {
        public PostProfiles()
        {
            CreateMap<AddPost, Post>().ReverseMap();
            CreateMap<PostResponseDto, Post>().ReverseMap();
        }
    }
}
