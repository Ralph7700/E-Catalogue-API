FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["e-catalog-backend.csproj", "./"]
RUN dotnet restore "e-catalog-backend.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "e-catalog-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "e-catalog-backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "e-catalog-backend.dll"]
