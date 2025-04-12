namespace BidingManagementSystem.Application.DTOs.Bider
{
    public class SubmitBidRequest
    {
        public int TenderId { get; set; }
        public decimal BidAmount { get; set; }
        public string Description { get; set; }
    }
}
