using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Movie_Booking_System.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Movie_Booking_System.AppHelper
{
    public class Token : IToken
    {
        private readonly IConfiguration configuration;
        public Token(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Createhash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            passwordSalt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            Console.WriteLine($"Salt: {Convert.ToBase64String(passwordSalt)}");
            passwordHash = (KeyDerivation.Pbkdf2(
                                 password: password!,
                                 salt: passwordSalt,
                                 prf: KeyDerivationPrf.HMACSHA512,
                                 iterationCount: 100000,
                                 numBytesRequested: 256 / 8));
        }

        public string CreateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);// before HmacSha256
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
            };
            var token = new JwtSecurityToken(
               
                claims:claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string CreateToken2(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token=new JwtSecurityToken(claims: claims, signingCredentials:creds,expires:DateTime.Now.AddDays(1));
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
            
        }



        public bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
           var newpasswordHash = (KeyDerivation.Pbkdf2(
                                 password: password!,
                                 salt: passwordSalt,
                                 prf: KeyDerivationPrf.HMACSHA512,
                                 iterationCount: 100000,
                                 numBytesRequested: 256 / 8));
            return newpasswordHash.SequenceEqual(newpasswordHash);

           /* var hm = new HMACSHA512(passwordSalt);
            var newHash=hm.ComputeHash(Encoding.UTF8.GetBytes(password));
            return newHash.SequenceEqual(passwordHash);*/
        }
    }
}
