FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

WORKDIR /app
COPY ./src/WyBlog.Web /app

EXPOSE 80
ENTRYPOINT ["dotnet", "WyBlog.Web.dll"]