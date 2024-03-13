using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Data;

namespace Todo.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using TodoContext context = new TodoContext(serviceProvider.GetRequiredService<DbContextOptions<TodoContext>>());
        if (!context.Task.Any())
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
                Colour = "#885645"
            };
            var petsProject = new Project
            {
                Name = "Pets",
                Colour = "#457829"
            };

            // Create the tasks
            var aubergineTask = new Task { Title = "Aubergine", Status = Status.Todo };
            var tomatoesTask = new Task { Title = "Tomatoes", Status = Status.Todo };
            var feedCatTask = new Task { Title = "Feed the cat", Status = Status.Todo };
            var walkDogTask = new Task { Title = "Walk the dog", Status = Status.Todo };

            // Associate tasks with their respective projects
            groceriesProject.Tasks.Add(aubergineTask);
            groceriesProject.Tasks.Add(tomatoesTask);
            petsProject.Tasks.Add(feedCatTask);
            petsProject.Tasks.Add(walkDogTask);

            // Set the project property for each task
            aubergineTask.Project = groceriesProject;
            tomatoesTask.Project = groceriesProject;
            feedCatTask.Project = petsProject;
            walkDogTask.Project = petsProject;

            // Add the projects to the context and save changes
            context.Project.AddRange(inbox, groceriesProject, petsProject);
            context.SaveChanges();

            // Print a success message
            Console.WriteLine("Data seeded successfully with projects and tasks.");
        }
    }



}
