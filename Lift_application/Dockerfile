#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lift_application/Lift_application.csproj", "Lift_application/"]
RUN dotnet restore "Lift_application/Lift_application.csproj"
COPY . .
WORKDIR "/src/Lift_application"
RUN dotnet build "Lift_application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lift_application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lift_application.dll"]