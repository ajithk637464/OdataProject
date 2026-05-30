---
name: project-odata-setup
description: OData DB-first ASP.NET Core 8 project structure, entity names, DbContext name, and key wiring decisions
metadata:
  type: project
---

Project lives at `D:\MIcroBackendR\OdataProject\OdataProject\OdataProject.API\`.

**DbContext**: `BackendDbContext` (in `Data/BackendDbContext.cs`) — NOT `AppDbContext`.
The database is `BackendDB` on `DESKTOP-GKE9C9P\SQLEXPRESS`.
Connection string key in appsettings.json: `DefaultConnection`.
`OnConfiguring` was removed from DbContext; connection string is injected via DI in `Program.cs`.

**Entities scaffolded** (all in `OdataProject.API.Models` namespace):
- `User` — key: `Id` (int), also has `UserGuid` (Guid, unique)
- `Role` — key: `RoleId` (int), also has `RoleGuid` (Guid, unique)
- `UserRole` — key: `UserRoleId` (int); junction between User and Role via Guid FKs
- `RefreshToken` — key: `RefreshTokenId` (int); FK to User via `UserGuid`

**OData package**: `Microsoft.AspNetCore.OData` 9.4.1 (not 8.x — 9.x is correct for .NET 8).
**EDM route prefix**: `"odata"`.
**MaxTop**: 100, **MaxExpansionDepth**: 3.

**Namespace gotcha**: `IEdmModel` is in `Microsoft.OData.Edm` — must be added explicitly even when using `ODataConventionModelBuilder` from `Microsoft.OData.ModelBuilder`.

**Controllers created**:
- `UsersController`, `RolesController`, `UserRolesController`, `RefreshTokensController`
- All derive from `ODataController`, inject `BackendDbContext`, return `IQueryable<T>` with `[EnableQuery]`.

**Why:** Build initially failed because `IEdmModel` namespace was missing. Adding `using Microsoft.OData.Edm;` resolved it.
**How to apply:** Always include `using Microsoft.OData.Edm;` in `Program.cs` when declaring `IEdmModel` return type for the EDM builder method.
