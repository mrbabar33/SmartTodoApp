using Microsoft.EntityFrameworkCore;
using SmartTodoApp.Models;

namespace SmartTodoApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}