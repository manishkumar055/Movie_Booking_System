using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Booking_System.Models
{
    public class Showtime
    {
        [Key]
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int TheaterId { get; set; }
        public int MovieId { get; set; }
        [ForeignKey("BookIn")]
        public int BookInId { get; set; }
        public BookIn BookIn { get; set; }
        public Theater Theater { get; set; } = null!;
        public Movie Movie { get; set; }=null!;


    }
}
