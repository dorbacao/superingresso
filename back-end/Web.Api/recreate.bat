c:
cd C:\Projetos\superingresso\back-end\Web.Api
dotnet ef database drop --force
dotnet ef migrations remove --force
dotnet ef migrations add initial
dotnet ef database update