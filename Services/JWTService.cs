using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using DiscordRipoff.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DiscordRipoff.Services {
    public class JWTService {
        private readonly string secret;
        private readonly SecurityKey securityKey;

        public JWTService(string secret) {
            this.secret = secret;
            securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        public string CreateToken(int id, string username) {
            var handler = new JwtSecurityTokenHandler();
            
            var algorithm = SecurityAlgorithms.HmacSha256;

            var discriptor = new SecurityTokenDescriptor {
                SigningCredentials = new SigningCredentials(securityKey, algorithm),
                TokenType = "JWT",
                Claims = new Dictionary<string, object>{
                    {"id", id},
                    {"username", username},
                },
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(1),
                IssuedAt = DateTime.UtcNow,
            };
            // Console.WriteLine(DateTime.UtcNow.AddSeconds(10));
            
            var token = handler.CreateToken(discriptor);

            return handler.WriteToken(token);
        }

        public bool ValidateTokent(
            [FromServices] AppDbContext dbContext,
            string token) 
        {
            var handler = new JwtSecurityTokenHandler();
            var loggedOut = dbContext.Blacklist.FirstOrDefault(list => list.JWT == token) != null;
            if(loggedOut) return false;
            try {
                handler.ValidateToken(token, GetDefaultParam(), out var _);
            } catch (SecurityTokenException e) {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        private TokenValidationParameters GetDefaultParam() {
            return new TokenValidationParameters() {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = new TimeSpan(0,0,1), 
                // RM: so dump
                IssuerSigningKey = securityKey,
            };
        }

        public DateTime GetTimeExpired(string token) {
            return new JwtSecurityToken(token).ValidTo;
        } 
    }
}