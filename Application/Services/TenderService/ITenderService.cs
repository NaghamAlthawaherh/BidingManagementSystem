using BidingManagementSystem.Application.DTOs.Tender;
using BidingManagementSystem.Application.Wrappers;


namespace BidingManagementSystem.Application.Services
{
    public interface ITenderService
    {
        Task<ServiceResponse<int>> CreateTenderAsync(CreateTenderRequest request);
         Task<ServiceResponse<List<TenderDto>>> GetAllTendersAsync();
         Task<ServiceResponse<string>>UpdateTenderAsync(int tenderId,UpdateTenderDto request);
         Task<ServiceResponse<string>> DeleteTenderAsync(int tenderId);
        Task<ServiceResponse<List<TenderDto>>> GetOpenTendersAsync();

    }
}
