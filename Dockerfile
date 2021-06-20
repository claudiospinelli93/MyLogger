FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /app

COPY MyLogger.sln ./
COPY . .
RUN dotnet restore MyLogger.sln

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Configure timezone
RUN unlink /etc/localtime
RUN ln -s /usr/share/zoneinfo/Brazil/East /etc/localtime

ENTRYPOINT ["dotnet", "MyLogger.dll"]