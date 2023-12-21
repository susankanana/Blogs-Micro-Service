namespace CommentService.Models.Dtos
{
    public class AddComment
    {
        public string CommentBody { get; set; } = string.Empty;
        public Guid PostId { get; set; }
    }
}
