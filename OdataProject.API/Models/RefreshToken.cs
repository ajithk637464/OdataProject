using System;
using System.Collections.Generic;

namespace OdataProject.API.Models;

public partial class RefreshToken
{
    public int RefreshTokenId { get; set; }

    public Guid RefreshTokenGuid { get; set; }

    public Guid UserGuid { get; set; }

    public string TokenHash { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? ReplacedByTokenHash { get; set; }

    public virtual User User { get; set; } = null!;
}
