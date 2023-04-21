namespace Movie_Booking_System.RequestDtos
{
    public class TicketReturnDto
    {
        public string MovieTitle { get; set; }
        public string TheaterName { get; set; }
        public string ShowTiming { get; set; }

        public string TicketType { get; set; }
        public int SeatNumber { get; set; }
    }
}
