using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OdataProject.API.Data;
using OdataProject.API.Models;

namespace OdataProject.API.Controllers;

public class UserRolesController : ODataController
{
    private readonly BackendDbContext _context;

    public UserRolesController(BackendDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [EnableQuery(MaxExpansionDepth = 3, MaxTop = 100)]
    public IQueryable<UserRole> Get()
    {
        return _context.UserRoles;
    }
}
