#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["aspnet-core/Klir.TechChallenge.Web.Api/Klir.TechChallenge.Web.Api.csproj", "aspnet-core/Klir.TechChallenge.Web.Api/"]
COPY ["aspnet-core/Klir.TechChallenge.Application/Klir.TechChallenge.Application.csproj", "aspnet-core/Klir.TechChallenge.Application/"]
COPY ["aspnet-core/Klir.TechChallenge.Domain/Klir.TechChallenge.Domain.csproj", "aspnet-core/Klir.TechChallenge.Domain/"]
COPY ["aspnet-core/Klir.TechChallenge.Infrastructure/Klir.TechChallenge.Infrastructure.csproj", "aspnet-core/Klir.TechChallenge.Infrastructure/"]
COPY ["aspnet-core/Klir.TechChallenge.CrossCutting/Klir.TechChallenge.CrossCutting.csproj", "aspnet-core/Klir.TechChallenge.CrossCutting/"]
RUN dotnet restore "aspnet-core/Klir.TechChallenge.Web.Api/Klir.TechChallenge.Web.Api.csproj"
COPY . .
WORKDIR "/src/aspnet-core/Klir.TechChallenge.Web.Api"
RUN dotnet build "Klir.TechChallenge.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Klir.TechChallenge.Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Klir.TechChallenge.Web.Api.dll"]