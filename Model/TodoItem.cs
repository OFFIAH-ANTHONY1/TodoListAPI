using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.Model
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }  = DateTime.Now;
        public DateTime TimeCompleted { get; set; }
    }
}
