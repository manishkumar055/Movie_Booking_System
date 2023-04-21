using Movie_Booking_System.Models;

namespace Movie_Booking_System.AppHelper
{
    public interface IToken
    {
        public string CreateToken(User user);
        public void Createhash(string password, out byte[] passwordSalt, out byte[] passwordHash);

        public bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash);

    }
}
