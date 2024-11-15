using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Case.Utilities; // FileLogger için namespace

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly string _logFilePath = @"C:\Users\Serhat\Desktop\New folder (4)\notlar.txt";

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterModel model)
    {
        if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        {
            return BadRequest("Invalid user data.");
        }

        // FileLogger kullanarak dosyaya yaz
        var fileLogger = new FileLogger(_logFilePath);
        var logContent = $"Timestamp: {DateTime.UtcNow}, Email: {model.Email}, Name: {model.Name}, Role: {model.Role}";
        fileLogger.WriteLog(logContent);

        return Ok(new { message = "User registered successfully!" });
    }
}

public class RegisterModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}
