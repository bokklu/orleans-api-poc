FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/Orleans.Api/Orleans.Api.csproj", "src/Orleans.Api/"]
COPY ["src/Orleans.Api.Application/Orleans.Api.Application.csproj", "src/Orleans.Api.Application/"]
COPY ["src/Orleans.Api.Application.Interfaces/Orleans.Api.Application.Interfaces.csproj", "src/Orleans.Api.Application.Interfaces/"]
COPY ["src/Orleans.Api.Domain/Orleans.Api.Domain.csproj", "src/Orleans.Api.Domain/"]
COPY ["src/Orleans.Api.Grains/Orleans.Api.Grains.csproj", "src/Orleans.Api.Grains/"]
COPY ["src/Orleans.Api.Grains.Interfaces/Orleans.Api.Grains.Interfaces.csproj", "src/Orleans.Api.Grains.Interfaces/"]
COPY ["src/Orleans.Api.Infra.Controllers/Orleans.Api.Infra.Controllers.csproj", "src/Orleans.Api.Infra.Controllers/"]
COPY ["src/Orleans.Api.Models/Orleans.Api.Models.csproj", "src/Orleans.Api.Models/"]
RUN dotnet restore "src/Orleans.Api/Orleans.Api.csproj"
COPY . .
WORKDIR "/src/src/Orleans.Api"
RUN dotnet build "Orleans.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orleans.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orleans.Api.dll"]