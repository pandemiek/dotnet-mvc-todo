using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required, StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        [Display(Name = "Due date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set;}
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}

public enum Status
{
    Todo,
    Completed,
    Deleted
}


public enum Priority
{
    P1,
    P2,
    P3,
    P4,
}
