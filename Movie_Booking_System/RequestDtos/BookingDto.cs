namespace Movie_Booking_System.RequestDtos
{
    public class BookingDto
    {
        public int TheaterId { get; set; }
        public int MovieId { get; set; }
        public int ShowtimeId { get; set; }

        public string SitSelection { get; set; }

    }

    public enum SeatSelection
    {
        RightSide=1,
        LeftSide=2,
        Normal=3,
        UpperSide=4
    }
}
