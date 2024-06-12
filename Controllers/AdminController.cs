using Microsoft.AspNetCore.Mvc;
using server.Data;
using AutoMapper;
using server.DTOs.Admin;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using server.Models;


namespace server.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AdminController: ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AdminController(LibraryContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var user = _context.Admins.FirstOrDefault(x => x.Username == loginDTO.Username && x.Password == loginDTO.Password);
            if (user == null)
            {
                return NotFound();
            }
            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signin
            );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            var admin = _mapper.Map<Admin, GetAdminDTO>(user);
            return Ok(new {Token = tokenValue, Admin = admin});
        }
    }
}