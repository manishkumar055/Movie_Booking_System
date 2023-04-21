namespace Movie_Booking_System.RequestDtos
{
    public class MovieDto
    {
        public int TheaterId { get; set; }
        public string Synopsis { get; set; }
        public string Genre { get; set; }
        public string Cast { get; set; }
        public string ReleaseDate { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
    }
}
