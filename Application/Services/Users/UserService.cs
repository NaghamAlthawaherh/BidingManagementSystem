using BidingManagementSystem.Application.DTOs.Users;
using BidingManagementSystem.Application.Wrappers;
using BidingManagementSystem.Domain.Entities;
using BidingManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using BidingManagementSystem.Application; // لو خزنت JwtSettings هناك
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using BidingManagementSystem.Application;
namespace BidingManagementSystem.Application.Services

{
    public class UserService: IUserService{
       

        private readonly AppDbContext _context;
  private readonly JwtSettings _jwtSettings;

public UserService(AppDbContext context, IOptions<JwtSettings> jwtSettings)
{
    _context = context;
    _jwtSettings = jwtSettings.Value;
}


 public async Task<ServiceResponse<int>> RegisterAsync(RegisterRequest registerRequest)
        {

            if (await _context.Users.AnyAsync(u => u.Email == registerRequest.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

            // تشفير كلمة المرور
            var passwordHash = HashPassword(registerRequest.Password);

            // إنشاء المستخدم
            var user = new User
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                PasswordHash = passwordHash,
               Role = registerRequest.Role,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Success = true,
                Message = "User registered successfully",
                Data = user.UserId
            };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

public async Task<ServiceResponse<string>> LoginAsync(LoginRequest request)
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

    if (user == null || user.PasswordHash != HashPassword(request.Password))
    {
        return new ServiceResponse<string>
        {
            Success = false,
            Message = "Invalid email or password"
        };
    }

    var token = GenerateJwtToken(user);

    return new ServiceResponse<string>
    {
        Success = true,
        Message = "Login successful",
        Data = token
    };
}

  private string GenerateJwtToken(User user)
{
   var claims = new[]
{
    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
    new Claim(ClaimTypes.Name, user.FullName),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.Role, user.Role.ToString())
};


    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _jwtSettings.Issuer,
        audience: _jwtSettings.Audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

    

}

  
    }
