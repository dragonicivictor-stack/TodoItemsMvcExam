using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoItems.Models
{
    [Table("ToDoItems")]
    public class ToDoListEntry
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public DateTime? DateDue { get; set; }

        [Required]
        public StatusType StatusType { get; set; }
    }
}
