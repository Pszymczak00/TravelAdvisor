FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

# Instalacja libgdiplus
RUN apt-get update && apt-get install -y libgdiplus

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TravelAdvisor.csproj", "./"]
RUN dotnet restore "TravelAdvisor.csproj"
COPY . .
RUN dotnet build "TravelAdvisor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravelAdvisor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelAdvisor.dll"] 