using BidingManagementSystem.Application.Bider;
using BidingManagementSystem.Application.Wrappers;
using BidingManagementSystem.Application.DTOs.Tender;
using BidingManagementSystem.Application.DTOs.Bider;
using BidingManagementSystem.Application.DTOs.Bider;
namespace BidingManagementSystem.Application.Services
{
    public interface IBidService
    {
        Task<ServiceResponse<int>> SubmitBidAsync(SubmitBidRequest request);
        Task<ServiceResponse<List<BidDto>>> GetMyBidsAsync();
        Task<ServiceResponse<List<TenderSummaryDto>>> GetOpenTendersAsync();
        Task<ServiceResponse<List<BidDto>>> GetBidsForTenderAsync(int tenderId);
       Task<ServiceResponse<List<BidDto>>> GetScoredBidsForTenderAsync(int tenderId);
       Task<ServiceResponse<string>> AwardBidAsync(int bidId);

    }
}