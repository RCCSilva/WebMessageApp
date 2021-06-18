FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["KafkaQueueManager/KafkaQueueManager.csproj", "KafkaQueueManager/"]
RUN dotnet restore "KafkaQueueManager/KafkaQueueManager.csproj"
COPY . .
WORKDIR "/src/KafkaQueueManager"
RUN dotnet build "KafkaQueueManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KafkaQueueManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaQueueManager.dll"]
