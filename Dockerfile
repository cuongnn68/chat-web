FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
# ? why it should on the same line. an article said it can cach the apt update so it can be the old version ?
RUN apt update && apt install npm -y
# ? apt install == apt upgrade
RUN npm install npm@latest -g
WORKDIR /app
COPY DiscordRipoff.csproj .
RUN dotnet restore
# RM copy many file
COPY ["client-app/package.json", "client-app/package-lock.json", "./"]
RUN npm install --production
# RUN npm run build # done in dotnet with MSBuild
COPY . .
RUN dotnet publish -c Release
# RUN ["dotnet", "dev-certs", "https", "-ep", "/app/cert/https.pfx", "-p", "password"]
# RUN ["dotnet", "dev-certs", "https","--trust"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS runtime-env
WORKDIR /app
COPY --from=build-env /app/bin/Release/net5.0/publish .
# COPY --from=build-env /app/cert .
COPY ripoff.db .

# ENV ASPNETCORE_URLS=http://+:5000;https://+:5001
ENV ASPNETCORE_URLS=http://+:5000
#EXPOSE port ???
EXPOSE 5000

# RM DONT FKING NEED HTTPS ON CLOUD # ?
# ENV Kestrel__Certificates__Default__Path=/app/cert/https.pfx
# ENV Kestrel__Certificates__Default__Password=password

ENTRYPOINT [ "dotnet", "DiscordRipoff.dll" ]


# build: docker build -t discord-ripoff:first .
# run:  docker run -p 80:5000 --name first-container discord-ripoff:first
# docker run -p 80:5000 -p 443:5001 -e Kestrel__Certificates__Default__Path=/app/cert/httpsCert.pfx -e Kestrel__Certificates__Default__Password=password --rm -v D:/code/sharp/_ltnc/DiscordRipoff/cert:/app/cert discord-ripoff:first 
# RM -p port:port map from host to container
# RM -e env
# RM --rm remove after stop
# ?  -e "ASPNETCORE_URLS=https://+;http://+"
