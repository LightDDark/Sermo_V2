using WebApplication1.Models.In;

namespace WebApplication1.Models.Out
{
    public class OutContact : InContact
    {
        public string? Last { get; set; } = null;
        public string? Lastdate { get; set; } = null;
    }
}
