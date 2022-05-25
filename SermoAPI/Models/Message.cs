using System.ComponentModel.DataAnnotations;

namespace Sermo_WAPI_Trial2
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public User? Author { get; set; }
        public Log? Log { get; set; }
        public string? Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

    }
}
