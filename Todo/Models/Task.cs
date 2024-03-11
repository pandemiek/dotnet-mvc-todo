using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Models
{
    public class Task
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Due date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set;}
        public string Project { get; set; }
        public required Status Status { get; set; }
    }
}

public enum Status
{
    Todo,
    Completed,
    Deleted
}
