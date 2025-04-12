namespace BidingManagementSystem.Application.DTOs.Bider
{
    public class BidDto
    {
        public int BidId { get; set; }
        public decimal BidAmount { get; set; }
        public string TenderTitle { get; set; }
        public DateTime SubmittedAt { get; set; }
          public string Status { get; set; }
          public string BidderName { get; set; }
         public int TenderId { get; set; }
         public double Score { get; set; }
    }
}
