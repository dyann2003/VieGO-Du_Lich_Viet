using System;
using System.Collections.Generic;

namespace Model.Models;

public class DiscountCode
{
    public int DiscountCodeID { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal DiscountValue { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidUntil { get; set; }
    public int Quantity { get; set; }
    public int UsedQuantity { get; set; }
    public string Status { get; set; } // Active, Inactive, Expired

    public bool IsExpired => DateTime.Now > ValidUntil || Status == "Expired";
}
