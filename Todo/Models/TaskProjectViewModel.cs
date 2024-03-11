using Microsoft.AspNetCore.Mvc.Rendering;

namespace Todo.Models
{
    public class TaskProjectViewModel
    {
        public List<Task>? Tasks { get; set; }
        public SelectList? Projects { get; set; }
        public string? TaskProject {  get; set; }
        public string? SearchString { get; set; }
    }
}
