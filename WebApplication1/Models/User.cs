using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Last { get; set; } = null;
        public string? Lastdate { get; set; } = null;
        public string? Server { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public List<User>? Contacts { get; set; } = new List<User>();
        public List<Log>? Logs { get; set; } = new List<Log>();

    }
}
