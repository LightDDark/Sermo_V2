using System.ComponentModel.DataAnnotations;

namespace Sermo_WAPI_Trial2
{
    public class Log
    {
        [Key]
        public string? stringId { get; set; }
        public HashSet<User>? Users { get; set; }

        public List<Message>? Messages { get; set; }
    }
}
