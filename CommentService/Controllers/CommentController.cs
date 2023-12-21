using AutoMapper;
using CommentService.Models;
using CommentService.Models.Dtos;
using CommentService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly IPost _postService;
        private readonly IComment _commentService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        public CommentController(IPost pst, IComment cmt, IMapper mapper)
        {
            _commentService = cmt;
            _postService = pst;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddComment(AddComment newComment)
        {
            var comment = _mapper.Map<Comment>(newComment);
            //if post exists
            var post = await _postService.GetPostById(comment.PostId);
            if (string.IsNullOrWhiteSpace(post.PostTitle))
            {
                _response.ErrorMessage = "Post Not Found";
                return NotFound(_response);
            }

            //post exists 

            var res = await _commentService.AddComment(comment);
            _response.Result = res;
            return Ok(_response);
        }


        [HttpGet("{PostId}")]
        public async Task<ActionResult<ResponseDto>> GetCommentsByPost(Guid PostId)
        {
            var comments = await _commentService.GetAllComments(PostId);
            var mappedComments = _mapper.Map<List<CommentResponseDto>>(comments);
            _response.Result = mappedComments;
            return Ok(_response);

        }


        [HttpGet("single/{Id}")]
        public async Task<ActionResult<ResponseDto>> GetComment(Guid Id)
        {
            var comment = await _commentService.GetComment(Id);
            if (comment == null)
            {
                _response.Result = "Comment Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = comment;
            return Ok(_response);

        }

        [HttpPut("Id")]

        public async Task<ActionResult<ResponseDto>> UpdateComment(Guid Id, AddComment updcomment)
        {
            var comment = await _commentService.GetComment(Id);
            if (comment == null)
            {
                _response.Result = "Comment Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _mapper.Map(updcomment, comment);
            var res = _commentService.UpdateComment();
            _response.Result = res;
            return Ok(_response);

        }

        [HttpDelete("Id")]
        public async Task<ActionResult<ResponseDto>> DeleteComment(Guid Id)
        {
            var comment = await _commentService.GetComment(Id);
            if (comment == null)
            {
                _response.Result = "Comment Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            var res = await _commentService.DeleteComment(comment);
            _response.Result = res;
            return Ok(_response);

        }

    }
}
