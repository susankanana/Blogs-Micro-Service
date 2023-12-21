namespace CommentService.Models.Dtos
{
    public class PostDto
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string PostTitle { get; set; } = string.Empty;
        public string PostBody { get; set; } = string.Empty;
    }

}
