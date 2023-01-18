FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CheeseBot/CheeseBot.csproj", "CheeseBot/"]
RUN dotnet restore "CheeseBot/CheeseBot.csproj"
COPY . .
WORKDIR "/src/CheeseBot"
RUN dotnet build "CheeseBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CheeseBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CheeseBot.dll"]
