using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BidingManagementSystem.Domain.Enums;

namespace BidingManagementSystem.Domain.Entities
{
    public class Tender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TenderId { get; set; }

        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        public string Category { get; set; } = default!; // اختيارية

        [Required]
        public string Industry { get; set; } = default!;

        [Required]
        public TenderType Type { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public decimal Budget { get; set; }

        [Required]
        public string Location { get; set; } = default!;
        public string EligibilityCriteria { get; set; } = string.Empty;


        public Guid CreatedByUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    
        public ICollection<TenderDocument> Documents { get; set; } = new List<TenderDocument>();

    }
}
