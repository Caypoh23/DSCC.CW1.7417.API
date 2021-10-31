sudo dotnet restore /var/www/DSCC_CW1_7417_API/DSCC.7417.API/DSCC.7417.API.csproj
sudo dotnet publish -c release /var/www/DSCC_CW1_7417_API/DSCC.7417.API/DSCC.7417.API.csproj -o /var/www/DSCC_7417_API_APP/
systemctl enable 7417_API.service
systemctl start 7417_API.service