using System;
using System.Collections.Generic;

namespace OdataProject.API.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public Guid UserGuid { get; set; }

    public Guid RoleGuid { get; set; }

    public DateTime? Created { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
