using System;
using System.Collections.Generic;
using BidingManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BidingManagementSystem.Domain.Entities
{
public class Bid{
    [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int BidId{ get; set; }
public int TenderId{ get; set; }

public int UserId{ get; set; }
public DateTime SubmittedAt{ get; set; }= DateTime.UtcNow;
public decimal TotalAmount{ get; set; }
 public BidStatus Status { get; set; } = BidStatus.Submitted;

    // Navigation
    public Tender Tender { get; set; } = default!;
    public User User { get; set; } = default!;
    public ICollection<BidDocument> Documents { get; set; } = new List<BidDocument>();
    public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

}}