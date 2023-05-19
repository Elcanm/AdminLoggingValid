using AdminLoggingValid.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdminLoggingValid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;
        //private readonly string logDosya = "log.txt";

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAdminService adminService, IConfiguration configuration)
        {
            _logger = logger;
            _adminService = adminService;
            _configuration = configuration;
        }

        [HttpGet("LogIn")]
        public async Task<string> Login(string email, string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, name)
                }),

                Expires = DateTime.UtcNow.AddMinutes(18),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                
            };
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }
       

        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await _adminService.Get();
        }

        [Authorize]
        [HttpPost]
        public async Task<User> Add([FromQuery] UserDTO userDTO)
        {
            User user = await _adminService.Add(userDTO);
            _logger.LogInformation($"{user.Email}");
            return user;
        }

        [Authorize]
        [HttpPut]
        public async Task<User> Update([FromQuery] UserDTO userDTO, int id)
        {
            return await _adminService.Update(userDTO, id);
        }

        [Authorize]
        [HttpDelete]
        public async Task<User> Delete(int id)
        {
            return await _adminService.Delete(id);
        }
    }
}