namespace PostService.Models.Dtos
{
    public class PostResponseDto
    {
        public string PostTitle { get; set; } = string.Empty;
        public string PostBody { get; set; } = string.Empty;
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
