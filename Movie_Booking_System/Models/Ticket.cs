using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Booking_System.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string TheaterName { get; set; }
        public string ShowTiming { get; set; }

        public string TicketType { get; set; }
        public int SeatNumber { get; set; }
        public string BookedBy { get; set; } = string.Empty;


        [ForeignKey(nameof(BookIn))]
        public int BookInId { get; set; }

        public BookIn BookIn { get; set; }
    }
}
