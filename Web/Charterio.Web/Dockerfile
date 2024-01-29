#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web/Charterio.Web/Charterio.Web.csproj", "Web/Charterio.Web/"]
COPY ["Data/Charterio.Data.Models/Charterio.Data.Models.csproj", "Data/Charterio.Data.Models/"]
COPY ["Data/Charterio.Data.Common/Charterio.Data.Common.csproj", "Data/Charterio.Data.Common/"]
COPY ["Data/Charterio.Data/Charterio.Data.csproj", "Data/Charterio.Data/"]
COPY ["Global/Charterio.Global.csproj", "Global/"]
COPY ["Services/Charterio.Services.Data/Charterio.Services.Data.csproj", "Services/Charterio.Services.Data/"]
COPY ["Web/Charterio.Web.ViewModels/Charterio.Web.ViewModels.csproj", "Web/Charterio.Web.ViewModels/"]
COPY ["Services/Charterio.Services.Mapping/Charterio.Services.Mapping.csproj", "Services/Charterio.Services.Mapping/"]
COPY ["Services/Charterio.Services.Hosted/Charterio.Services.Hosted.csproj", "Services/Charterio.Services.Hosted/"]
COPY ["Services/Charterio.Services.Payment/Charterio.Services.Payment.csproj", "Services/Charterio.Services.Payment/"]
COPY ["Services/Charterio.Services/Charterio.Services.csproj", "Services/Charterio.Services/"]
COPY ["Web/Charterio.Web.Infrastructure/Charterio.Web.Infrastructure.csproj", "Web/Charterio.Web.Infrastructure/"]
RUN dotnet restore "./Web/Charterio.Web/./Charterio.Web.csproj"
COPY . .
WORKDIR "/src/Web/Charterio.Web"
RUN dotnet build "./Charterio.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Charterio.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Charterio.Web.dll"]