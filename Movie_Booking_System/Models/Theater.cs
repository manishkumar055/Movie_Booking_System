using System.ComponentModel.DataAnnotations;

namespace Movie_Booking_System.Models
{
    public class Theater
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Screen { get; set; } = null!;
        public int SeatingCapacity { get; set; }
      

        public ICollection<MovieTheaterHandler> Movies { get; set; }
        public ICollection<Showtime> Showtimes { get; set; }

    }
}
