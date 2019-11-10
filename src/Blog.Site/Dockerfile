FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Blog.Site/Blog.Site.csproj", "Blog.Site/"]
COPY ["Blog.Core/Blog.Core.csproj", "Blog.Core/"]
COPY ["Blog.Infrastructure/Blog.Infrastructure.csproj", "Blog.Infrastructure/"]
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