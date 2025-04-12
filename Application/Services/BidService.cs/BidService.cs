using BidingManagementSystem.Application.Bider;
using BidingManagementSystem.Application.Wrappers;
using BidingManagementSystem.Domain.Entities;
using BidingManagementSystem.Domain.Enums;
using BidingManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using BidingManagementSystem.Application.DTOs.Bider;
using BidingManagementSystem.Application.DTOs.Tender;
namespace BidingManagementSystem.Application.Services
{
    public class BidService : IBidService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BidService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<int>> SubmitBidAsync(SubmitBidRequest request)
        {
            var userId = GetCurrentUserId();

            var tender = await _context.Tenders.FindAsync(request.TenderId);
            if (tender == null || tender.Deadline < DateTime.UtcNow)
            {
                return new ServiceResponse<int> { Success = false, Message = "Tender not found or already closed" };
            }

            var bid = new Bid
            {
                TenderId = request.TenderId,
                SubmittedAt = DateTime.UtcNow,
                TotalAmount = request.BidAmount,
                UserId = userId,
            };

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Success = true, Message = "Bid submitted successfully", Data = bid.BidId };
        }

        public async Task<ServiceResponse<List<BidDto>>> GetMyBidsAsync()
        {
            var userId = GetCurrentUserId();

            var bids = await _context.Bids
                .Include(b => b.Tender)
                .Where(b => b.UserId == userId)
                .Select(b => new BidDto
                {
                    BidId = b.BidId,
                    BidAmount = b.TotalAmount,
                    TenderTitle = b.Tender.Title,
                    SubmittedAt = b.SubmittedAt,
                    TenderId = b.TenderId
                })
                .ToListAsync();

            return new ServiceResponse<List<BidDto>>
            {
                Success = true,
                Data = bids
            };
        }

        public async Task<ServiceResponse<List<TenderSummaryDto>>> GetOpenTendersAsync()
        {
            var openTenders = await _context.Tenders
                .Where(t => t.Deadline > DateTime.UtcNow)
                .Select(t => new TenderSummaryDto
                {
                    TenderId = t.TenderId,
                    Title = t.Title,
                    Description = t.Description,
                    Deadline = t.Deadline,
                    Budget = t.Budget
                })
                .ToListAsync();

            return new ServiceResponse<List<TenderSummaryDto>>
            {
                Success = true,
                Data = openTenders
            };
        }

       private int GetCurrentUserId()
{
    var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        return userId;

    return 0; 
}
public async Task<ServiceResponse<List<BidDto>>> GetBidsForTenderAsync(int tenderId)
{
    var bids = await _context.Bids
        .Include(b => b.User)
        .Where(b => b.TenderId == tenderId)
        .Select(b => new BidDto
        {
            BidId = b.BidId,
            TenderId = b.TenderId,
            BidAmount = b.TotalAmount,
            SubmittedAt = b.SubmittedAt,
            BidderName = b.User.FullName,
            Status = b.Status.ToString()
        })
        .ToListAsync();

    return new ServiceResponse<List<BidDto>>
    {
        Success = true,
        Data = bids
    };
}   
      public async Task<ServiceResponse<List<BidDto>>> GetScoredBidsForTenderAsync(int tenderId)
{
    var bids = await _context.Bids
        .Include(b => b.User)
        .Where(b => b.TenderId == tenderId)
        .ToListAsync();

    if (!bids.Any())
        return new ServiceResponse<List<BidDto>> { Success = false, Message = "No bids found" };

    var minPrice = bids.Min(b => b.TotalAmount);

    var scoredBids = bids.Select(b => new BidDto
    {
        BidId = b.BidId,
        TenderId = b.TenderId,
        BidAmount = b.TotalAmount,
        SubmittedAt = b.SubmittedAt,
        BidderName = b.User.FullName,
        Status = b.Status.ToString(),
        Score = Math.Round((double)(minPrice / b.TotalAmount) * 100, 2)
    }).ToList();

    return new ServiceResponse<List<BidDto>>
    {
        Success = true,
        Data = scoredBids
    };
}  
    public async Task<ServiceResponse<string>> AwardBidAsync(int bidId)
{
    var winningBid = await _context.Bids
        .Include(b => b.Tender)
        .FirstOrDefaultAsync(b => b.BidId == bidId);

    if (winningBid == null)
        return new ServiceResponse<string> { Success = false, Message = "Bid not found" };

    // ترسية العرض
    winningBid.Status = BidStatus.Awarded;

    // نرفض العروض الأخرى لنفس العطاء (اختياريًا)
    var otherBids = await _context.Bids
        .Where(b => b.TenderId == winningBid.TenderId && b.BidId != bidId)
        .ToListAsync();

    foreach (var b in otherBids)
    {
        b.Status = BidStatus.Rejected;
    }

    await _context.SaveChangesAsync();

    return new ServiceResponse<string>
    {
        Success = true,
        Message = "Bid has been awarded successfully"
    };
}




    }
}
