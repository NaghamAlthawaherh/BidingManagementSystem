using BidingManagementSystem.Application.DTOs.Users;
using BidingManagementSystem.Application.Wrappers;

namespace BidingManagementSystem.Application.Services
{
public interface IUserService{

public  Task<ServiceResponse<int>> RegisterAsync(RegisterRequest registerRequest);

public  Task<ServiceResponse<string>> LoginAsync(LoginRequest request);
}











}