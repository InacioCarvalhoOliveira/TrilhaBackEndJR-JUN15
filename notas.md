pacotes necessários:
    $ dotnet add package Microsoft.entityFrameworkCore.InMemory
    $ dotnet add package Microsoft.entityFrameworkCore.SqlServer


    ef tool update
       $ dotnet tool update --global dotnet-ef --version 8.0.7

    certs trust
        $ dotnet dev-certs https --clean
        $ dotnet dev-certs https --trust
    
    params on connection string : Trusted_Connection=False;TrustServerCertificate=True;

    migrations
        $ dotnet tool install --global dotnet-ef
        $ dotnet add package Microsoft.EntityFrameworkCore.Design
        
        $ dotnet ef migrations add initialCreate
        To undo this action, use 'ef migrations remove'
        
        $ dotnet ef database update

    autenticacao e permissao usuarios
        $ dotnet add package Microsoft.AspNetCore.Authentication 
        $ dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer


        $ identity server