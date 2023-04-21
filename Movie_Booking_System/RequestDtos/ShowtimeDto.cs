namespace Movie_Booking_System.RequestDtos
{
    public class ShowtimeDto
    {
        public int MovieId { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public int TheaterId { get; set; }
        public int RightMax { get; set; }

        public int LeftMax { get; set; }

        public int NormalMax { get; set; }

        public int UpperMax { get; set; }

    }
}
