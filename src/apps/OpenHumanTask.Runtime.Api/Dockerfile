#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
RUN apt-get update
RUN apt-get install jq -y

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/apps/OpenHumanTask.Runtime.Api/OpenHumanTask.Runtime.Api.csproj", "src/apps/OpenHumanTask.Runtime.Api/"]
COPY ["src/core/OpenHumanTask.Runtime.Application/OpenHumanTask.Runtime.Application.csproj", "src/core/OpenHumanTask.Runtime.Application/"]
COPY ["src/core/OpenHumanTask.Runtime.Infrastructure/OpenHumanTask.Runtime.Infrastructure.csproj", "src/core/OpenHumanTask.Runtime.Infrastructure/"]
COPY ["src/core/OpenHumanTask.Runtime.Domain/OpenHumanTask.Runtime.Domain.csproj", "src/core/OpenHumanTask.Runtime.Domain/"]
COPY ["src/core/OpenHumanTask.Runtime.Integration/OpenHumanTask.Runtime.Integration.csproj", "src/core/OpenHumanTask.Runtime.Integration/"]
COPY ["src/apps/OpenHumanTask.Runtime.Dashboard/OpenHumanTask.Runtime.Dashboard.csproj", "src/apps/OpenHumanTask.Runtime.Dashboard/"]
RUN dotnet restore "src/apps/OpenHumanTask.Runtime.Api/OpenHumanTask.Runtime.Api.csproj"
COPY . .
WORKDIR "/src/src/apps/OpenHumanTask.Runtime.Api"
RUN dotnet build "OpenHumanTask.Runtime.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenHumanTask.Runtime.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenHumanTask.Runtime.Api.dll"]