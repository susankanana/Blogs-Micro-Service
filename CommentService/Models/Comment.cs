using System.ComponentModel.DataAnnotations.Schema;

namespace CommentService.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string CommentBody { get; set; }=string.Empty;
        [ForeignKey(nameof(PostId))]
        public Guid PostId { get; set; }
    }
}
