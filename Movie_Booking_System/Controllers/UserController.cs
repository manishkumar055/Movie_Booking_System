using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Booking_System.AppHelper;
using Movie_Booking_System.Context;
using Movie_Booking_System.Models;
using Movie_Booking_System.RequestDtos;

namespace Movie_Booking_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MBSContext _context;
        private readonly IToken token;
        private string role = "User";
        private string admin = "Admin";
        private byte[] passwordSalt;
        private byte[] passwordHash;
        public UserController(MBSContext mBSContext, IToken token)
        {
            _context = mBSContext;
            this.token = token;
        }

        [HttpPost]

        public async Task<IActionResult> Register(UserRequestDto request)
        {
            var user=await _context.Users.FirstOrDefaultAsync(x=>x.Email== request.Email);
            if(user!=null) { throw new Exception("Already Registred Please Login"); }
            token.Createhash(request.Password, out passwordSalt, out passwordHash);
            await _context.Users.AddAsync(new User()
            {
                Name = request.Name,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = role,

            });
            await _context.SaveChangesAsync();
            return Ok("User Added");

        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var userObj = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (userObj == null)
            {
                throw new Exception("Please Register First");

            }
            if (!token.VerifyPassword(password, userObj.PasswordSalt, userObj.PasswordHash))
            {
                throw new Exception("Password Not Matched");
            }
            return Ok(token.CreateToken(userObj));
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddAdmin(UserRequestDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user != null) { throw new Exception("Already Registred Please Login"); }
            token.Createhash(request.Password, out passwordSalt, out passwordHash);
            await _context.Users.AddAsync(new User()
            {
                Name = request.Name,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = admin,

            });
            await _context.SaveChangesAsync();
            return Ok("User Added");
        }
    }
}
