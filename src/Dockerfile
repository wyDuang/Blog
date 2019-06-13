FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
EXPOSE 5000
=======
EXPOSE 80
>>>>>>> dd8c893... 修改结构
=======
EXPOSE 80
>>>>>>> bc6d11d... 测试
=======
EXPOSE 5000
>>>>>>> 21d7527... 修改Dockerfile

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Blog.Site/Blog.Site.csproj", "Blog.Site/"]
COPY ["Blog.Infrastructure/Blog.Infrastructure.csproj", "Blog.Infrastructure/"]
COPY ["Blog.Core/Blog.Core.csproj", "Blog.Core/"]
RUN dotnet restore "Blog.Site/Blog.Site.csproj"
COPY . .
WORKDIR "/src/Blog.Site"
RUN dotnet build "Blog.Site.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.Site.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.Site.dll"]