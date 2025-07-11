using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using tripath.Models;
using tripath.Utils;

namespace tripath.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserManagement user)
        {
            var claims = new[]
            {
            new Claim($"{AppStrings.Constants.UserId}", user.UserId ?? ""),
            new Claim($"{AppStrings.Constants.UserName}", user.UserName ?? ""),
            new Claim($"{AppStrings.Constants.UserEmail}", user.UserEmail ?? "")
        };
        Console.WriteLine("user.userId" + user.UserId);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["JwtSettings:ExpiryInDays"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}