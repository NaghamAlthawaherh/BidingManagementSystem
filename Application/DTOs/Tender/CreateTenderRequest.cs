using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using BidingManagementSystem.Domain.Enums;

namespace BidingManagementSystem.Application.DTOs.Tender
{
public class CreateTenderRequest{
 [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "Deadline is required")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Budget is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Eligibility criteria is required")]
        public string EligibilityCriteria { get; set; } = default!;

        [Required(ErrorMessage = "Industry is required")]
        public string Industry { get; set; } = default!;

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = default!;

        [Required(ErrorMessage = "Tender type is required")]
        public TenderType TenderType { get; set; }

        // مرفقات مثل PDF، صور، ملفات Word
        public List<IFormFile>? Attachments { get; set; }


}





}