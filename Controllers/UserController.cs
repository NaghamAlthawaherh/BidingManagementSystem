using Microsoft.AspNetCore.Mvc;
// using BidingManagementSystem.Application.Services.UserService;
// using BidingManagementSystem.Application.Services.IUserService;
using BidingManagementSystem.Application.DTOs.Users;
using BidingManagementSystem.Application.Services;
using BidingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;


namespace BidingManagementSystem.API.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase{

private readonly IUserService _userService;
public UserController(IUserService userService){
_userService=userService;
}
[HttpPost("register")]
public async Task<IActionResult> Register([FromBody]RegisterRequest registerRequest)
{
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // استدعاء منطق الخدمة لتسجيل المستخدم
            var result = await _userService.RegisterAsync(registerRequest);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);

}

[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequest request)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var result = await _userService.LoginAsync(request);

    if (!result.Success)
        return BadRequest(result.Message);

    return Ok(new
    {
        message = result.Message,
         token = result.Data
    });}

}}