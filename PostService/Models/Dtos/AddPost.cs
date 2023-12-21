namespace PostService.Models.Dtos
{
    public class AddPost
    {
        public string PostTitle { get; set; } = string.Empty;
        public string PostBody { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}

