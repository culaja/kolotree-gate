FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS builder

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build -c Release .

WORKDIR /app/
RUN dotnet publish -c Release --no-build -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

WORKDIR /app

COPY --from=builder /app/out .

ENTRYPOINT ["dotnet", "kolotree-gate.dll"]

