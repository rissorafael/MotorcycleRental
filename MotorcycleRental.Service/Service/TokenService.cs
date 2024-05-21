using Microsoft.IdentityModel.Tokens;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Infra.CrossCutting.ExtensionMethods;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MotorcycleRental.Service.Service
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(List<Claim> claims)
        {
            var Token = string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ExatractConfiguration.GetChaveToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}
