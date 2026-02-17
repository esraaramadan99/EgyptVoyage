using EgyptVoyage.Application.Common.Interfaces;
using EgyptVoyage.Application.DTOs.Auth;
using EgyptVoyage.Domain.Entities;
using EgyptVoyage.Domain.Enums;
using EgyptVoyage.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EgyptVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITouristRepository _touristRepository;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly PasswordHasher _passwordHasher;

    /*public AuthController(
        ITouristRepository touristRepository,
        JwtTokenGenerator jwtTokenGenerator,
        PasswordHasher passwordHasher) */

       public AuthController(
       ITouristRepository touristRepository,
      JwtTokenGenerator jwtTokenGenerator,
       PasswordHasher passwordHasher)

       {
        _touristRepository = touristRepository;
     _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
   
       }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto request)
    {
        try
        {
            var existingTourist = await _touristRepository.GetByEmailAsync(request.Email);
            if (existingTourist != null)
            {
                return BadRequest(new { message = "Email already exists" });
            }

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var tourist = new Tourist
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword
            };

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
    /// <summary>
    /// </summary>
   /* [HttpPost("register-clerk")]
    public async Task<ActionResult<AuthResponseDto>> RegisterClerk([FromBody] RegisterDto request)
    {
        try
        {
            var existingClerk = await _clerkRepository.GetByEmailAsync(request.Email);
            if (existingClerk != null)
            {
                return BadRequest(new { message = "Email already exists" });
            }

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var clerk = new Clerk
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                Role = UserRole.Clerk
            };

            await _clerkRepository.AddAsync(clerk);

            var token = _jwtTokenGenerator.GenerateTokenForClerk(clerk);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Id = clerk.Id,
                Email = clerk.Email,
                Name = clerk.Name,
                Role = clerk.Role.ToString()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", error = ex.Message });
        }
    }


*/





}