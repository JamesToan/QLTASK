#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
#WORKDIR /app
#EXPOSE 80

#FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["coreWeb.csproj", ""]
RUN dotnet restore "/coreWeb.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "coreWeb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "coreWeb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "coreWeb.dll"]
###############################################
## copy csproj and restore as distinct layers
#COPY *.sln .
#COPY *.csproj ./
#RUN dotnet restore
#
## copy everything else and build app
#COPY . ./
#WORKDIR /app
#RUN dotnet publish -c Release -o out
#
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
#WORKDIR /app
#COPY --from=build /app/out ./
#ENTRYPOINT ["dotnet", "coreWeb.dll"]
#
