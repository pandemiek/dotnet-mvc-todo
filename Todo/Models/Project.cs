using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Display(Name = "Project")]
        public string Name { get; set; }
        public string Colour { get; set; }
        public List<Task> Tasks { get; } = [];
    }
}
