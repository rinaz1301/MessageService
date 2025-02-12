using System.ComponentModel.DataAnnotations;

namespace MessageService.Models
{
    public class Message
    {
        public int Id { get; set; }
        [StringLength(128)]
        public string? Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int SequenceNumber { get; set; }
    }
}
