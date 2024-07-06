# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY *.sln .
COPY WebBlog.Api/*.csproj ./WebBlog.Api/
COPY WebBlog.Application/*.csproj ./WebBlog.Application/
COPY WebBlog.Domain/*.csproj ./WebBlog.Domain/
COPY WebBlog.Infrastructure/*.csproj ./WebBlog.Infrastructure/
RUN dotnet restore

# Copy the entire project and build
COPY . .
WORKDIR /src/WebBlog.Api
RUN dotnet build -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose ports and set the user
EXPOSE 8080
EXPOSE 8081
USER app

ENTRYPOINT ["dotnet", "WebBlog.Api.dll"]
