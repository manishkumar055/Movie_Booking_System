namespace Movie_Booking_System.RequestDtos
{
    public class MovieBookingDto
    {
        public int TheaterId { get; set; }
        public int MovieId { get; set; }
        public int ShowTimeId { get; set; }
        public string SeatChoice { get; set; } = "Normal";

    }
}
