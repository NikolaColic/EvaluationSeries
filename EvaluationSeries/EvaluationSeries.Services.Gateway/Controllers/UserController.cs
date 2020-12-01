using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Models;
using EvaluationSeries.Services.Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Gateway.Controllers
{
    [Route("gateway/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserServicesGateway _user;
        private readonly AppSettings _appSettings;
        public UserController(IUserServicesGateway user, IOptions<AppSettings> appSettings)
        {
            _user = user;
            _appSettings = appSettings.Value;
        }
       
        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _user.GetUsers();
            if (users is null) return NotFound();
            return Ok(users);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<User>>> PostUser([FromBody] User user)
        {
            var response = await _user.AddUser(user);
            if (response) return RedirectToRoute("GetUsers");
            return NotFound();
        }
        [HttpPost("aut")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Actor>>> Authentication([FromBody] AuthenticateRequest request)
        {
            var response = await _user.Authenticatiion(request.Username, request.Password);
            if (response is null) return NotFound();
            var token = CreateToken(response);
            var user = CreateResponse(token, response);
            return Ok(user);
        }

        private string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        private AuthenticateResponse CreateResponse(string token, User user)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            response.Email = user.Email;
            response.Id = user.Id;
            response.Token = token;
            response.Username = user.Username;
            response.Role = user.Role;
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> PutUser(int id, [FromBody] User user)
        {
            user.Id = id;
            var response = await _user.UpdateUser(user);
            if (response) return RedirectToRoute("GetUsers");
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _user.DeleteUser(id);
            if (response) return NoContent();
            return NotFound();
        }
    }
}
