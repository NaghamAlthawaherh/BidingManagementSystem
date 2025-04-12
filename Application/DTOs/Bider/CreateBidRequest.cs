using Microsoft.AspNetCore.Http;

namespace BidingManagementSystem.Application.Bider{
public class CreateBidRequest
{
    public int TenderId { get; set; }
    public decimal TotalAmount { get; set; }
    public string ProposalSummary { get; set; } = string.Empty;
    public List<IFormFile>? Attachments { get; set; }
}



















}