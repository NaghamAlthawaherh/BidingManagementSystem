using System;
using BidingManagementSystem.Domain.Enums;
namespace BidingManagementSystem.Application.DTOs.Tender
{
    public class UpdateTenderDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string EligibilityCriteria { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public decimal Budget { get; set; }
        public TenderType TenderType { get; set; }
    }
}
