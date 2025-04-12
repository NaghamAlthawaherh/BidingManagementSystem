using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BidingManagementSystem.Domain.Entities
{



public class Evaluation{
    [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int EvaluationId{get; set;}
public int BidId{get; set;}
public float Score{get; set;}
public string Comment{get; set;}
public int EvaluatedByUserId{get; set;}
public DateTime CreatedAt{get; set;}= DateTime.UtcNow;
 // Navigation
    public Bid Bid { get; set; } = default!;
}
}
