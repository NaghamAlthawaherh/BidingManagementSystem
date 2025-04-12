using BidingManagementSystem.Application.DTOs.Bider;
using BidingManagementSystem.Application.Services;
using BidingManagementSystem.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BidingManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Bidder")]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        // POST: Submit a bid
        [HttpPost]
        public async Task<IActionResult> SubmitBid([FromForm] SubmitBidRequest request)
        {
            var result = await _bidService.SubmitBidAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // GET: Get bids submitted by current user
        [HttpGet("my-bids")]
        public async Task<IActionResult> GetMyBids()
        {
            var result = await _bidService.GetMyBidsAsync();
            return Ok(result);
        }

        // GET: Get open tenders
        [HttpGet("open-tenders")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOpenTenders()
        {
            var result = await _bidService.GetOpenTendersAsync();
            return Ok(result);
        }
        [Authorize(Roles = "Evaluator,ProcurementOfficer")]
        [HttpGet("tender/{tenderId}/bids")]
        public async Task<IActionResult> GetBidsForTender(int tenderId)
        {
            var result = await _bidService.GetBidsForTenderAsync(tenderId);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result);
        }
        [Authorize(Roles = "Evaluator,ProcurementOfficer")]
        [HttpGet("tender/{tenderId}/scored-bids")]
        public async Task<IActionResult> GetScoredBidsForTender(int tenderId)
        {
            var result = await _bidService.GetScoredBidsForTenderAsync(tenderId);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result);
        }
        [Authorize(Roles = "ProcurementOfficer,Evaluator")]
        [HttpPost("award/{bidId}")]
        public async Task<IActionResult> AwardBid(int bidId)
        {
            var result = await _bidService.AwardBidAsync(bidId);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result);
        }

    }
}
