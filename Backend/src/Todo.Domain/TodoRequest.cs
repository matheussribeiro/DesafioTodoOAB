using System.ComponentModel.DataAnnotations;

namespace Todo.Domain
{
    public class TodoRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}