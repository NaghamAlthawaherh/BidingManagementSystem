namespace BidingManagementSystem.Application.DTOs.Tender
{
    public class TenderSummaryDto
    {
        public int TenderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Budget { get; set; }
    }
}
