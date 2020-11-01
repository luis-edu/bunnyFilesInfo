FROM microsoft/dotnet:3.1-aspnetcore-runtime AS base
WORKDIR /app
COPY . .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet BunnyAPI.dll