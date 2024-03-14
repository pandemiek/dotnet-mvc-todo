using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using Todo.Models;
using Task = Todo.Models.Task;

namespace Todo.Data;

public class SeedData
{
    public static void Initialize(TodoContext context)
    {
        if (!context.Task.Any() && !context.Project.Any())
        {

            var inbox = new Project
            {
                Name = "Inbox",
                Colour = "#382832"
            };
            // Create the projects
            var groceriesProject = new Project
            {
                Name = "Groceries",
                Colour = "#885645",
            };
            var petsProject = new Project
            {
                Name = "Pets",
                Colour = "#457829"
            };

            context.AddRange(inbox, groceriesProject, petsProject);

            // Create the tasks
            var feedCatTask = new Task { Title = "Feed the cat", Status = Status.Todo, Project = petsProject };
            var walkDogTask = new Task { Title = "Walk the dog", Status = Status.Todo, Project = petsProject };

            
            context.AddRange(feedCatTask, walkDogTask);
            context.SaveChanges();
          
        }
    }



}
