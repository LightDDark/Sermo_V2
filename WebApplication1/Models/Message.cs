using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public User? Author { get; set; }
        public Log? Log { get; set; }
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Created { get; set; }

        public bool? Sent { get; set; }

    }
}
