using System.ComponentModel.DataAnnotations;

namespace Movie_Booking_System.Models
{
    public class MovieTheaterHandler
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int TheaterId { get; set; }

        public Movie Movie { get; set; }
        public Theater Theater { get; set; }

    }
}
