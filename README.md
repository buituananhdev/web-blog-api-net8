script: 

dotnet ef migrations add InitialCreate --project WebBlog.Infrastructure --startup-project WebBlog.Api

dotnet ef database update --project WebBlog.Infrastructure --startup-project WebBlog.Api

dotnet ef migrations remove --project WebBlog.Infrastructure --startup-project WebBlog.Api

