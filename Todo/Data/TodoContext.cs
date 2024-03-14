using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext (DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<Todo.Models.Task> Task { get; set; } = default!;
        public DbSet<Todo.Models.Project> Project { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable(nameof(Project))
                .HasMany(t => t.Tasks)
                .WithOne(t => t.Project);
        }
    }
}
