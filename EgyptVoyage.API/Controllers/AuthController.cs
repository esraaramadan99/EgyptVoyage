using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Auth;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // بنعمل متغيرات خاصة بالـ Controller عشان نستخدمهم في كل الـ Methods
    private readonly ITouristRepository _touristRepository; // بيتعامل مع قاعدة البيانات
    private readonly JwtTokenGenerator _jwtTokenGenerator;  // بيعمل JWT Token
    private readonly PasswordHasher _passwordHasher;        // بيعمل Hash للباسورد

    // الـ Constructor — بيتنفذ أول ما الـ Controller يتعمل
    // الـ .NET بيجيب الـ Dependencies دي تلقائياً (Dependency Injection)
    public AuthController(
        ITouristRepository touristRepository,
        JwtTokenGenerator jwtTokenGenerator,
        PasswordHasher passwordHasher)
    {
        // بنحفظ اللي جالنا في المتغيرات اللي فوق
        _touristRepository = touristRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;

    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto request)
    {
        try
        {
            // 1. Check if email exists

            var existingTourist = await _touristRepository.GetByEmailAsync(request.Email);
            if (existingTourist != null)
            {
                return BadRequest(new { message = "Email already exists" });
            }
            // hashed password
            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var tourist = new Tourist
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword
            };
            //save to mongo db
            await _touristRepository.AddAsync(tourist);

            var token = _jwtTokenGenerator.GenerateTokenForTourist(tourist);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Id = tourist.Id,
                Email = tourist.Email,
                Name = tourist.Name,
                Role = "Tourist"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during registration", error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto request)
    {
        try
        {
            var tourist = await _touristRepository.GetByEmailAsync(request.Email);
            if (tourist != null && _passwordHasher.VerifyPassword(request.Password, tourist.Password))
            {
                var token = _jwtTokenGenerator.GenerateTokenForTourist(tourist);
                return Ok(new AuthResponseDto
                {
                    Token = token,
                    Id = tourist.Id,
                    Email = tourist.Email,
                    Name = tourist.Name,
                    Role = "Tourist"
                });
            }

           

            return Unauthorized(new { message = "Invalid email or password" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during login", error = ex.Message });
        }
    }


    // PUT: api/auth/update-password
    [HttpPut("update-password")]
    [Authorize(Roles = "Tourist")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto request)
    {
        try
        {
            // تأكد إن الباسورد الجديد والتأكيد متطابقين
            if (request.NewPassword != request.ConfirmNewPassword)
                return BadRequest(new { message = "New passwords do not match" });

            // جيب الـ Tourist من الـ Token
            var touristId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tourist = await _touristRepository.GetByIdAsync(touristId!);

            if (tourist == null)
                return NotFound(new { message = "Tourist not found" });

            // تحقق من الباسورد القديم
            if (!_passwordHasher.VerifyPassword(request.CurrentPassword, tourist.Password))
                return BadRequest(new { message = "Current password is incorrect" });

            // حدّث الباسورد
            tourist.Password = _passwordHasher.HashPassword(request.NewPassword);
            await _touristRepository.UpdateAsync(tourist);

            return Ok(new { message = "Password updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", error = ex.Message });
        }
    }

    // POST: api/auth/forgot-password
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
    {
        try
        {
            var tourist = await _touristRepository.GetByEmailAsync(request.Email);

            // حتى لو الإيميل مش موجود، مش بنقول للـ Client عشان الأمان
            if (tourist == null)
                return Ok(new { message = "If this email exists, a reset token has been generated" });

            // عمل Reset Token
            var resetToken = Guid.NewGuid().ToString("N"); // token بدون dashes
            tourist.PasswordResetToken = resetToken;
            tourist.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1); // صالح ساعة

            await _touristRepository.UpdateAsync(tourist);

            // في Production هتبعت الـ Token في إيميل
            // دلوقتي بنرجعه في الـ Response للـ Testing
            return Ok(new
            {
                message = "Password reset token generated",
                resetToken = resetToken,
                expiresAt = tourist.PasswordResetTokenExpiry
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", error = ex.Message });
        }
    }

    // POST: api/auth/reset-password
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
    {
        try
        {
            if (request.NewPassword != request.ConfirmNewPassword)
                return BadRequest(new { message = "Passwords do not match" });

            // جيب الـ Tourist بالـ Token
            var tourist = await _touristRepository.GetByResetTokenAsync(request.Token);

            if (tourist == null)
                return BadRequest(new { message = "Invalid reset token" });

            // تحقق إن الـ Token لسه صالح
            if (tourist.PasswordResetTokenExpiry < DateTime.UtcNow)
                return BadRequest(new { message = "Reset token has expired" });

            // حدّث الباسورد وامسح الـ Token
            tourist.Password = _passwordHasher.HashPassword(request.NewPassword);
            tourist.PasswordResetToken = null;
            tourist.PasswordResetTokenExpiry = null;

            await _touristRepository.UpdateAsync(tourist);

            return Ok(new { message = "Password reset successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", error = ex.Message });
        }
    }






    // مؤقت عشان نعمل Hash للباسورد -  ما اخد الـ Hash
    [HttpGet("hash")]
    public IActionResult HashPassword([FromQuery] string password)
    {
        var hashed = _passwordHasher.HashPassword(password);
        return Ok(new { hashed });
    }


}