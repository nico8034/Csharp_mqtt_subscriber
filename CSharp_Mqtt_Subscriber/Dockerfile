FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CSharp_Mqtt_Subscriber/CSharp_Mqtt_Subscriber.csproj", "CSharp_Mqtt_Subscriber/"]
RUN dotnet restore "CSharp_Mqtt_Subscriber/CSharp_Mqtt_Subscriber.csproj"
COPY . .
WORKDIR "/src/CSharp_Mqtt_Subscriber"
RUN dotnet build "CSharp_Mqtt_Subscriber.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharp_Mqtt_Subscriber.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharp_Mqtt_Subscriber.dll"]
