using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidingManagementSystem.Domain.Entities
{
    public class TenderDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentId { get; set; }

        [Required]
        public string FileName { get; set; } = default!;

        [Required]
        public string FilePath { get; set; } = default!;

        public string FileType { get; set; } = default!;

        [Required]
        public int TenderId { get; set; }

        // Navigation
        public Tender Tender { get; set; } = default!;
    }
}
