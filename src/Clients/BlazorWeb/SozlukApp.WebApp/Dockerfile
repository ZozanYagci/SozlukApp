FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY . ./
RUN dotnet publish "src/Client/BlazorWeb/SozlukApp.WebApp/SozlukApp.WebApp.csproj" -c Release -o output

FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=build-env /app/output/wwwroot .
COPY "src/Clients/BlazorWeb/SozlukApp.WebApp/nginx.conf" /etc/nginx/nginx.conf
