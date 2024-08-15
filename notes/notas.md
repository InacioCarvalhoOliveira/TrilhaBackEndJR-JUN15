## Pacotes Necessários
Pacotes necessários para funcionar o EF core, JWT, certificados

SE for utilizar o projeto somente para testes, sem a necessidade do banco de dados instale no termninal(Nuget ou console)

    $- dotnet add package Microsoft.EntityFrameworkCore.InMemory

**descomentar** linha **25** do arquivo **Startup.cs** e
**comentar** linha **27** do arquivo **Startup.cs**

CASO contrário instale:

    $- dotnet add package Microsoft.entityFrameworkCore.SqlServer

### EF tool update

    $- dotnet tool update --global dotnet-ef --version 8.0.7

### Dar permissão aos certificados(HTTPS) 

    $- dotnet dev-certs https --clean
    $- dotnet dev-certs https --trust

### Pacotes para migrações (SOMENTE se estiver utilizando banco de dados)

    $- dotnet tool install --global dotnet-ef
    $- dotnet add package Microsoft.EntityFrameworkCore.Design
    $- dotnet ef migrations add initialCreate

para desfazer a ação, use 'ef migrations remove'

    $- dotnet ef database update

### autenticacao e permissao usuarios
        $ dotnet add package Microsoft.AspNetCore.Authentication
        $ dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
