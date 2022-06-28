using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Domain
{
    public class TodoModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DtCreated { get; set; }
        
    }
}