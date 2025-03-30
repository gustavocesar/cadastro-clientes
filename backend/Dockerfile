# Imagem base para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo do projeto e restaura as dependências
COPY ["src/CadastroCliente.csproj", "./"]
RUN dotnet restore

# Copia todo o código fonte
COPY src/. .

# Publica a aplicação
RUN dotnet publish -c Release -o /app/publish

# Imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expõe a porta da API
EXPOSE 80
EXPOSE 443

# Define a variável de ambiente para o ASP.NET Core
ENV ASPNETCORE_URLS=http://+:80

# Inicia a aplicação
ENTRYPOINT ["dotnet", "CadastroCliente.dll"]