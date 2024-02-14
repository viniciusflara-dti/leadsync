FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/LeadSync.Api/LeadSync.Api.csproj", "LeadSync.Api/"]
COPY ["src/LeadSync.Application/LeadSync.Application.csproj", "LeadSync.Application/"]
COPY ["src/LeadSync.Domain/LeadSync.Domain.csproj", "LeadSync.Domain/"]
COPY ["src/LeadSync.Contracts/LeadSync.Contracts.csproj", "LeadSync.Contracts/"]
COPY ["src/LeadSync.Infrastructure/LeadSync.Infrastructure.csproj", "LeadSync.Infrastructure/"]
COPY ["Directory.Packages.props", "./"]
COPY ["Directory.Build.props", "./"]
RUN dotnet restore "LeadSync.Api/LeadSync.Api.csproj"
COPY . ../
WORKDIR /src/LeadSync.Api
RUN dotnet build "LeadSync.Api.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LeadSync.Api.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeadSync.Api.dll"]