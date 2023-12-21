using Microsoft.EntityFrameworkCore;
using PostService.Models;
using System.Collections.Generic;

namespace PostService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
    }
}
   
