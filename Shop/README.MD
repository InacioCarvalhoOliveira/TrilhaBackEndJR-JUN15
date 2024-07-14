## API ASPNET_CORE e EF_FRAMEWORK_CORE 8
Banco de dados : sqlServer(IDE SqlExpress)
Estrutura do banco montada em Data_Annotations
    - Projeto possui migração direta
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
