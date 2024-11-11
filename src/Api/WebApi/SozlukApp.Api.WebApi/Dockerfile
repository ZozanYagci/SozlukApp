#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Api/WebApi/SozlukApp.Api.WebApi/SozlukApp.Api.WebApi.csproj", "src/Api/WebApi/SozlukApp.Api.WebApi/"]
COPY ["src/Api/Infrastructure/SozlukApp.Infrastructure.Persistence/SozlukApp.Infrastructure.Persistence.csproj", "src/Api/Infrastructure/SozlukApp.Infrastructure.Persistence/"]
COPY ["src/Api/Core/SozlukApp.Api.Application/SozlukApp.Api.Application.csproj", "src/Api/Core/SozlukApp.Api.Application/"]
COPY ["src/Api/Core/SozlukApp.Api.Domain/SozlukApp.Api.Domain.csproj", "src/Api/Core/SozlukApp.Api.Domain/"]
COPY ["src/Common/SozlukApp.Common/SozlukApp.Common.csproj", "src/Common/SozlukApp.Common/"]
RUN dotnet restore "./src/Api/WebApi/SozlukApp.Api.WebApi/SozlukApp.Api.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Api/WebApi/SozlukApp.Api.WebApi"
RUN dotnet build "./SozlukApp.Api.WebApi.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SozlukApp.Api.WebApi.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SozlukApp.Api.WebApi.dll"]