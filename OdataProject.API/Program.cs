using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OdataProject.API.Data;
using OdataProject.API.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BackendDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

static IEdmModel BuildEdmModel()
{
    var modelBuilder = new ODataConventionModelBuilder();

    modelBuilder.EntitySet<User>("Users")
                .EntityType.HasKey(e => e.Id);

    modelBuilder.EntitySet<Role>("Roles")
                .EntityType.HasKey(e => e.RoleId);

    modelBuilder.EntitySet<UserRole>("UserRoles")
                .EntityType.HasKey(e => e.UserRoleId);

    modelBuilder.EntitySet<RefreshToken>("RefreshTokens")
                .EntityType.HasKey(e => e.RefreshTokenId);

    return modelBuilder.GetEdmModel();
}

builder.Services.AddControllers()
    .AddOData(options =>
    {
        options
            .Select()
            .Filter()
            .OrderBy()
            .Expand()
            .Count()
            .SetMaxTop(100)
            .AddRouteComponents("odata", BuildEdmModel());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
