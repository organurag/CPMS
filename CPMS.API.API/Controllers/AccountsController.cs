using CPMS.API.Models;
using CPMS.API.Models.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CPMS.API.API.Controllers
{
    public class AccountController : ApiController
    {
        ProjectContext _db = new ProjectContext();
        public IHttpActionResult Authenticate(User user)
        {
            if (ModelState.IsValid)
            {
                bool IsAutenticated = _db.Users.Any(u => u.Email == user.Email && u.Password == user.Password);
                if (IsAutenticated)
                {
                    string token = createToken(user.Email);
                    return Ok(token);
                }

            }
            return BadRequest(ModelState);
        }
        private string createToken(string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddMinutes(10);
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Name, username),
                   new Claim(ClaimTypes.Email, username)
            });

            const string sec =
                "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            var token = (JwtSecurityToken)
        tokenHandler.CreateJwtSecurityToken(issuer: "https://localhost:44382",
        audience: "https://localhost:44382",
            subject: claimsIdentity,
            notBefore: issuedAt, expires: expires,
            signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}