FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

WORKDIR /app
COPY ./src.WyBlog.Api /app

EXPOSE 5000
ENTRYPOINT ["dotnet", "WyBlog.Api.dll"]