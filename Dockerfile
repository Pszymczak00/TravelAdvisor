FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TravelAdvisor/TravelAdvisor.csproj", "./"]
RUN dotnet restore "TravelAdvisor.csproj"
COPY . .
WORKDIR "/src/TravelAdvisor"
RUN dotnet build "TravelAdvisor.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/TravelAdvisor"
RUN dotnet publish "TravelAdvisor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelAdvisor.dll"] 