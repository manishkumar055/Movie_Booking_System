using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Booking_System.Models
{
    public class BookIn
    {
        [Key]
        public int Id { get; set; }
        public int RightSide { get; set; }
        public int RightMax { get; set; }
        public int LeftSide { get; set; }
        public int LeftMax { get; set; }
        public int Normal { get; set; }
        public int NormalMax { get; set; }
        public int UpperSide { get; set; }
        public int UpperMax { get; set; }
        public int FilledSeats { get; set; }
        
        public Showtime Showtime { get; set; }

    }
}
