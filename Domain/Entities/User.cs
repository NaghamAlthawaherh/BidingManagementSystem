using System;
using System.Collections.Generic;
using BidingManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidingManagementSystem.Domain.Entities
{
public class User{
[Key]
 [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int UserId{ get; set; }
public string FullName{ get; set; }
public string Email{ get; set; }
public string PasswordHash{ get; set; }
public UserRole Role{ get; set; }
public DateTime CreatedAt{get; set;} = DateTime.UtcNow;

   // Navigation
    public ICollection<Bid> Bids { get; set; } = new List<Bid>();







}


}