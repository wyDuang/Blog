FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Blog.Api/Blog.Api.csproj", "Blog.Api/"]
COPY ["Blog.Core/Blog.Core.csproj", "Blog.Core/"]
COPY ["Blog.Infrastructure/Blog.Infrastructure.csproj", "Blog.Infrastructure/"]
RUN dotnet restore "Blog.Api/Blog.Api.csproj"
COPY . .
WORKDIR "/src/Blog.Api"
RUN dotnet build "Blog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.Api.csproj" -c Release -o /app/publish

RUN apt-get update -y && apt-get install -y libgdiplus && apt-get clean && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.Api.dll"]