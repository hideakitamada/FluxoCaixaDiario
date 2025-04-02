Controle de Fluxo de Caixa

Descrição

Este projeto implementa um sistema de Controle de Lançamentos e Consolidado Diário baseado em microsserviços utilizando C# e .NET. A arquitetura foi projetada para ser escalável, resiliente e segura, utilizando mensageria com RabbitMQ, autenticação JWT, e persistência com Entity Framework e banco de dados relacional.

Tecnologias Utilizadas:

 - .NET Core
 - ASP.NET Core Web API
 - Entity Framework Core
 - RabbitMQ (Mensageria)
 - JWT (Autenticação)
 - xUnit e Moq (Testes Unitários)

Arquitetura

O sistema segue o padrão Microsserviços, separando as responsabilidades em diferentes serviços:
 - Serviço de Lançamentos: Responsável por criar e listar lançamentos financeiros.
 - Serviço de Consolidação: Calcula o saldo diário baseado nos lançamentos.

Funcionalidades:

 - Criar lançamentos de crédito e débito.
 - Recuperar a lista de lançamentos.
 - Obter o saldo diário consolidado.
 - Autenticação segura com JWT.
 - Persistência de dados usando Entity Framework.


Configuração e Execução

1. Clonar o repositório
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio

2. Configurar e rodar o Banco de Dados
Caso esteja utilizando o SQL Server, edite a string de conexão no appsettings.json:

"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FluxoCaixa;User Id=sa;Password=SuaSenha;"
}

Para PostgreSQL, altere conforme necessário.

3. Executar a aplicação

dotnet build
dotnet run

A API estará disponível em: http://localhost:7952

4. Testes

Para rodar os testes unitários, utilize:

dotnet test

Endpoints da API

Autenticação (JWT)

POST /api/auth/token → Gera um token JWT para autenticação.

Lançamentos

POST /api/lancamentos → Criar um novo lançamento.
GET /api/lancamentos → Listar todos os lançamentos.

Consolidação

GET /api/consolidado → Retorna o saldo diário consolidado.

Melhorias Futuras
 - Implementar um sistema de retries no RabbitMQ.
 - Melhorar a escalabilidade com event sourcing e CQRS.
 - Criar uma interface web para visualização dos lançamentos e saldos.

Contribuição

Pull requests são bem-vindos! Sinta-se à vontade para contribuir com melhorias e novas funcionalidades.

Desenvolvido por Hugo Tamada 🚀
