# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY *.sln .

# Copy .csproj files for each project
COPY src/WebBlog.Api/*.csproj ./src/WebBlog.Api/
COPY src/WebBlog.Application/*.csproj ./src/WebBlog.Application/
COPY src/WebBlog.Domain/*.csproj ./src/WebBlog.Domain/
COPY src/WebBlog.Infrastructure/*.csproj ./src/WebBlog.Infrastructure/

# Copy .csproj files for unit tests
COPY tests/WebBlog.Application.UnitTests/*.csproj ./tests/WebBlog.Application.UnitTests/
COPY tests/WebBlog.Domain.UnitTests/*.csproj ./tests/WebBlog.Domain.UnitTests/

# Restore dependencies
RUN dotnet restore

# Copy the entire project files and build
COPY . .
RUN dotnet build ./src/WebBlog.Api/WebBlog.Api.csproj -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish ./src/WebBlog.Api/WebBlog.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose ports and set the user
EXPOSE 8080
EXPOSE 8081
USER app

ENTRYPOINT ["dotnet", "WebBlog.Api.dll"]
