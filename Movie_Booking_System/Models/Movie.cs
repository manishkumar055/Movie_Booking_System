using System.ComponentModel.DataAnnotations;

namespace Movie_Booking_System.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public string Genre { get; set; }
        public string Cast { get; set; }
        public string ReleaseDate { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }

        public ICollection<MovieTheaterHandler> Theaters { get; set; }
        public ICollection<Showtime> Showtimes { get; set; }


    }
}
