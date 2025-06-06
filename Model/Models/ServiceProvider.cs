using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class ServiceProvider
{
    public int ProviderId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string Status { get; set; } = null!;

    public string ProviderType { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();

    public virtual User? User { get; set; }
}
