FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
EXPOSE 8080

#copy scproj and restore as dinstinct layers
COPY *.csproj ./
RUN dotnet website_gym

#copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

#build runtime image


FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "API.dll" ]