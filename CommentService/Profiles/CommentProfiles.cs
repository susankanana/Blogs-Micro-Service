using AutoMapper;
using CommentService.Models;
using CommentService.Models.Dtos;

namespace CommentService.Profiles
{
    public class CommentProfiles : Profile
    {
        public CommentProfiles()
        {
            CreateMap<AddComment, Comment>().ReverseMap();
            CreateMap<CommentResponseDto, Comment>().ReverseMap();
            
        }
    }
}
