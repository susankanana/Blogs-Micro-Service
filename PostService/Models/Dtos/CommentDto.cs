using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Models.Dtos
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public string CommentBody { get; set; } = string.Empty;
    }
}
