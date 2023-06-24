using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    public class JwtService
    {
        public String SecretKey {get; set;}
        public int TokenDuration { get; set; }
        private readonly IConfiguration config;

        public JwtService(IConfiguration _config)
        {
            config = _config;
            this.SecretKey = config.GetSection("AppSettings").GetSection("Secret").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("AppSettings").GetSection("Duration").Value);
        }

        public string GererateToken(string id, string tip, string nume, string prenume, string email, string telefon, string cnp)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var payload = new[] {
              new Claim("id", id),
              new Claim("tip", tip),
              new Claim("nume", nume),
              new Claim("prenume", prenume),
              new Claim("email", email),
              new Claim("telefon", telefon),
              new Claim("cnp", cnp)
            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }


    }
}
