﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Cart: BaseEntity
{
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public string ApplicationUserId { get; set; }
    [ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser ApplicationUser { get; set; }
    [Required]
    public int Count { get; set; }
}
