FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["WyBlog.Web/WyBlog.Web.csproj", "WyBlog.Web/"]
COPY ["WyBlog.Dtos/WyBlog.Dtos.csproj", "WyBlog.Dtos/"]
COPY ["WyBlog.Core/WyBlog.Core.csproj", "WyBlog.Core/"]
COPY ["WyBlog.Repository.MySql/WyBlog.Repository.MySql.csproj", "WyBlog.Repository.MySql/"]
COPY ["WyBlog.Entities/WyBlog.Entities.csproj", "WyBlog.Entities/"]
COPY ["WyBlog.IRepository/WyBlog.IRepository.csproj", "WyBlog.IRepository/"]
COPY ["WyBlog.IServices/WyBlog.IServices.csproj", "WyBlog.IServices/"]
COPY ["WyBlog.Services/WyBlog.Services.csproj", "WyBlog.Services/"]
RUN dotnet restore "WyBlog.Web/WyBlog.Web.csproj"
COPY . .
WORKDIR "/src/WyBlog.Web"
RUN dotnet build "WyBlog.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WyBlog.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WyBlog.Web.dll"]