using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidingManagementSystem.Domain.Entities
{


public class BidDocument{
    [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int BidDocumentId{ get; set; }
public int BidId{ get; set; }
public string FileName{ get; set; }
public string FilePath{ get; set; }
public string FileType{ get; set; }

// Navigation
    public Bid Bid { get; set; } = default!;

}}


