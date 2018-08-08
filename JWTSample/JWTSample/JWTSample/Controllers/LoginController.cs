using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using JWTSample.DomainModels;
using JWTSample.Helper;
using Microsoft.AspNetCore.Mvc;

namespace JWTSample.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]AuthenticationModel user)
        {
            try
            {
                bool Success = true;
                if (Success == true)
                {
                    var userModel = user;
                    Dictionary<string, string> claims = new Dictionary<string, string>();
                    claims.Add(JwtRegisteredClaimNames.Email, userModel.Email);
                    claims.Add(ClaimConstant.USERID, "1");
                    claims.Add(ClaimConstant.LOGIN_TYPE, "user");
                    string token = GenerateToken(claims);

                    return Ok(new { StatusCode = "200", Token = token });
                }
                else
                {
                    return Ok(new { StatusCode = "500" });

                }
            }

            catch (Exception ex)
            {
                return BadRequest("Error Occurred");
            }
        }

        public string GenerateToken(Dictionary<string, string> claims)
        {
            var token = new JwtTokenBuilder()
                                     .AddSecurityKey(JwtSecurityKey.Create("jwt-sample-key-123456"))
                                     .AddSubject("Login User")
                                     .AddIssuer("JWTSample")
                                     .AddAudience("JWTSample")
                                     .AddClaims(claims)
                                     .AddExpiry(360)
                                     .Build();
            return token.Value;
        }

        public class ClaimConstant
        {
            public const string USERID = "userid";
            public const string LOGIN_TYPE = "logintype";
        }
    }
}