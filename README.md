# Expense Control API

API RESTful para controle de despesas residenciais desenvolvida com .NET 9. O objetivo deste projeto é demonstrar a implementação de uma arquitetura robusta, seguindo princípios de Domain-Driven Design (DDD) e Clean Architecture, além de práticas modernas de CI/CD.

## Tecnologias e Práticas

- **Linguagem:** C# (.NET 9)
- **ORM:** Entity Framework Core
- **Banco de Dados:** PostgreSQL (Hospedado na Neon.tech)
- **Testes:** xUnit
- **Documentação:** Swagger / OpenAPI
- **Hospedagem:** Azure App Service (Linux)
- **CI/CD:** GitHub Actions

## Estrutura e Arquitetura

O projeto foi estruturado utilizando Clean Architecture para garantir o desacoplamento das camadas e facilitar a testabilidade:

1. **Domain:** Núcleo do projeto contendo as entidades (Category, Person, Transaction), interfaces de repositório, enums e regras de negócio. Não possui dependências externas.
2. **Application:** Contém os casos de uso (Use Cases), DTOs, validações e contratos de serviço.
3. **Infrastructure:** Implementação do acesso a dados (EF Core), migrações, configurações de banco e serviços externos.
4. **Api:** Camada de entrada responsável pelos Controllers, injeção de dependência (DI), middlewares de tratamento de erro global e Health Checks.

## DevOps e Deploy

O deploy é totalmente automatizado via GitHub Actions. A pipeline realiza os seguintes passos a cada push na branch principal:

1. Restauração de pacotes e build da aplicação.
2. Execução obrigatória dos testes unitários.
3. Publicação automática no Azure App Service (Plano Gratuito).

Para contornar as limitações de inatividade do plano gratuito da Azure, foi implementado um endpoint de Health Check conectado a um monitoramento externo (UptimeRobot), garantindo alta disponibilidade e evitando "Cold Starts".

## Como Executar Localmente

1. Clone o repositório.
2. Configure a string de conexão com o PostgreSQL no arquivo `appsettings.Development.json`.
3. Execute o comando `dotnet restore` para baixar as dependências.
4. Execute o projeto API via Visual Studio ou `dotnet run`.
5. Acesse a documentação via `/swagger`.

## Melhorias Futuras

- [ ] Implementar processamento assíncrono de relatórios usando **MassTransit** e **RabbitMQ** (CloudAMQP).
- [ ] Adicionar autenticação/autorização (Identity ou JWT customizado).