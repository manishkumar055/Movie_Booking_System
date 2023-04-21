namespace Movie_Booking_System.RequestDtos
{
    public class UserRequestDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
    }
}
