using System.ComponentModel.DataAnnotations;

namespace PostService.Models
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public string PostTitle { get; set; }=string.Empty;
        [Required]
        public string PostBody { get; set; } = string.Empty;
        //public List<Comment> comments { get; set; }= new List<Comment>();
    }
}
