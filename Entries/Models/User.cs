using System.ComponentModel.DataAnnotations;

namespace Sermo_WAPI_Trial2
{
    public class User
    {
        [Key]
        public string? Name { get; set; }
        [Required]
        public string? Nickname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public List<User>? Contacts { get; set; }
        public List<Log>? Logs { get; set; } 
    }
}
