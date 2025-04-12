using BidingManagementSystem.Domain.Enums;
namespace BidingManagementSystem.Application.DTOs.Tender
{
    public class TenderDto
    {
        public int TenderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Industry { get; set; }
        public TenderType Type { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Budget { get; set; }
        public string Location { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
