using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Todo.Data;

namespace Todo.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TodoContext(
                serviceProvider.GetRequiredService<DbContextOptions<TodoContext>>()))
            {
                if (context.Task.Any())
                {
                    return;
                }
                context.Task.AddRange(
                    new Task
                    {
                        Title = "Feed the cat",
                        Status = Status.Todo,
                        Project = "Pets"
                    },
                    new Task
                    {
                        Title = "Walk the dog",
                        DueDate = DateTime.Now,
                        Status = Status.Todo,
                        Project = "Pets"
                    },
                    new Task
                    {
                        Title = "Cook dinner",
                        DueDate = DateTime.Now,
                        Status = Status.Todo,
                        Project = "Personal"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
