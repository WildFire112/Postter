using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class PostterContext: DbContext
    {
        public PostterContext(DbContextOptions<PostterContext> options) : base(options) {}

        public DbSet<Message> Messages { get; set; }
    }
}