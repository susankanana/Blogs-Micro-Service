using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using PostService.Models;
using PostService.Models.Dtos;
using PostService.Services.IService;
using System.Security.Claims;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPost _postService;
        private readonly IUser _userService;
        private readonly IComment _commentService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public PostController(IPost postService, IUser userService, IMapper mapper , IComment commentService)
        {
            _mapper = mapper;
            _postService = postService;
            _userService = userService;
            _response = new ResponseDto();
            _commentService = commentService;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddPost(AddPost newPost)
        {
            var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                _response.ErrorMessage = "You are not authorized";
                return BadRequest(_response);
            }
            Console.WriteLine($"The user Id is --------->>{userId}");
            var post = _mapper.Map<Post>(newPost);
            post.UserId = new Guid(userId);
            //var user = await _userService.GetUserById(post.UserId);
            //if (string.IsNullOrWhiteSpace(user.Name))
            //{
            //    _response.ErrorMessage = "User Not Found";
            //    return NotFound(_response);
            //}

            //user exists 

            var res = await _postService.AddPost(post);
            _response.Result = res;
            return Ok(_response);
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<ResponseDto>> GetPostByUserId(Guid UserId)
        {
            var posts = await _postService.GetAllPosts(UserId);
            var mappedPosts = _mapper.Map<List<PostResponseDto>>(posts);
            _response.Result = mappedPosts;
            return Ok(_response);

        }

        [HttpGet("single/{Id}")]
        public async Task<ActionResult<ResponseDto>> GetPost(Guid Id)
        {
            var post = await _postService.GetPostById(Id);
            if (post == null)
            {
                _response.Result = "Post Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            var comments = await _commentService.GetCommentsOfPost(Id);
            var postdto = new PostResponseDto()
            {
                PostTitle = post.PostTitle,
                PostBody = post.PostBody,
                Comments = comments
            };
            _response.Result = postdto;
            return Ok(_response);

        }

        [HttpPut("Id")]

        public async Task<ActionResult<ResponseDto>> UpdatePost(Guid Id, AddPost updpost)
        {
           var post = await _postService.GetPostById(Id);
            if (post == null)
            {
                _response.Result = "Post Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
             _mapper.Map(updpost, post);
            var res = _postService.UpdatePost();
            _response.Result = res;
            return Ok(_response);

        }

        [HttpDelete("Id")]
        public async Task<ActionResult<ResponseDto>> DeletePost(Guid Id)
        {
            var post = await _postService.GetPostById(Id);
            if (post == null)
            {
                _response.Result = "Post Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            var res = await _postService.DeletePost(post);
            _response.Result = res;
            return Ok(_response);

        }


   }
}
