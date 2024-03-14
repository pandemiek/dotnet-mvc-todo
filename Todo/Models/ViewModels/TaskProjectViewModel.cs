using Microsoft.AspNetCore.Mvc.Rendering;

namespace Todo.Models.ViewModels
{
    public class TaskProjectViewModel
    {
        public Project Project { get; set; }
        public Task Task { get; set; }
        public List<Project> AllProjects { get; set; }
    }
}
