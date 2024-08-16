![Código Certo Coders](https://utfs.io/f/3b2340e8-5523-4aca-a549-0688fd07450e-j4edu.jfif)
##
<h1 align="center">
API Product ordering and management
</h1>

### Projeto
Este projeto se baseia na ideia de Gerenciamento de Produtos e Usuários do sistema. Com ele é possível Adicionar um produto, atribui-lo a uma categoria e gerenciar quem pode fazer nodificações neste produto o(Gerente/Funcionário). Foram implementado no projeto funcionalidades de Autenticação e autorização, fazendo com que somente Funcionários autenticados possam cadastrar produtos e que apenas os administradores tenham acesso a todas as funcionalidades.  

<details>
    <summary><b>Categorias</b></summary>
    <ul>
        <li>
        POST /categories 
        Host: localhost:5005
        {
        "title": "categoria 1"
        }
        </li>
        <li>
        DELETE /categories/1 
        Host: localhost:5005
        </li>
        <li>
        PUT /categories/1 
        Host: localhost:5001
        {
            "Id":1,
            "Title":"nova categoria 1"
        }
        </li>
        <li>
        GET /categories/1
        Host: localhost:5005
        {
            "id":1
        }
        </li>
        <li>
        GET /categories 
        Host: localhost:5001
        </li>        
    </ul>
</details>

<details>
    <summary><b>Produtos</b></summary>
     <ul>
        <li>
        POST /products 
        Host: localhost:5001
        {            
            "title":"Produto 3",
            "description":"Fogao consul",
            "price":1900,
            "categoryId":1
        }
        </li>
        <li>
        GET /products
        Host: localhost:5005       
        </li>              
        <li>
        GET /products/1
        Host: localhost:5005
        {
            "id": 1
        }
        </li>      
     <ul>
</details>

<details>
    <summary><b>Login</b></summary>
    <ul>
        <li>
        POST /users 
        Host: localhost:5001
        {
            "username":"inacio",
            "password":"1234",
            "role":"employee"
        }
        </li>
        <li>
        POST /users/login 
        Host: localhost:5001
        {
            "username":"inacio",
            "password":"1234"
        }    
        </li>              
        <li>
        DELETE /users
        Host: localhost:5005
        {
            "id":1
        }
        </li>
        <li>
        PUT /users
        Host: localhost:5001
        {
            "id":2	 	
        }
        </li> 
        <li>
        GET /users 
        Host: localhost:5001
        </li>
        <li>
        GET /users/2
        Host: localhost:5001
        {
            "id":2
        }
        </li>      
    </ul>
</details>


## 🥷 Tecnologias

- Linguagem: C#(Sdk8.0) 
- Framework: ASP.NET CORE Spring Boot (Entity Framework Core)
- Banco de Dados: SQL Server
- Entity Framework Migrations
- Microsoft JWTBearier

## ☁️ Deploy
A API está hospedada no [Azure](https://azure.microsoft.com/pt-br/) e pode demorar um pouco para carregar.

- **Documentação da API**: https://apishopv2.azurewebsites.net/swagger/index.html
- **Coleção com as Requisições HTTPS (Postman ou Insomnia)**: [collection](/notes/img/lista_Requisicoes.png)


## ⚒️ Configuração
Pré-requisito: Dotnet sdk8.0
1. Clone o repositório
2. Instale as dependências documentadas em **/notas.md**

## 👩‍💻 Autor
Inacio Oliveira
https://www.linkedin.com/in/InacioCarvalho/

## Projeto
Api de Gerenciamento de Produtos e usuários(gerenciamento dos acessos de perfis) 

- Banco de dados : SqlServer
- Estrutura do banco montada em Data_Annotations
- Projeto possui migração direta
- validacao e autenticacao com JWT
- Ha um service que hospeda a aplicacao no AZURE Devops, juntamente com sua base de dados
