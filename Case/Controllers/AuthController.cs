using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // Örnek kullanıcı doğrulama (gerçek bir projede bu bir veri tabanı ile yapılmalıdır)
        if (model.Email == "admin" && model.Password == "admin")
        {
            // Kullanıcı rolleri
            var roles = new List<string> { "Admin", "User" };

            // Claim'ler oluşturuluyor
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Rolleri claim'lere ekle
            roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdasdasdasdaera123123asdasero2323123"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token oluştur
            var token = new JwtSecurityToken(
                issuer: "CaseAPI",
                audience: "CaseAPIUsers",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds);

            // Token döndür
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        return Unauthorized();
    }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
