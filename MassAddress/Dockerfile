FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["MassAddress/MassAddress.csproj", "MassAddress/"]
RUN dotnet restore "MassAddress/MassAddress.csproj"
COPY . .
WORKDIR "/src/MassAddress"
RUN dotnet build "MassAddress.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MassAddress.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MassAddress.dll"]