# Expense Control API

Este documento apresenta a Expense Control API, uma interface de programação de aplicações (API) RESTful desenvolvida com .NET 9, destinada ao controle de despesas residenciais. O objetivo primordial deste projeto consiste na demonstração prática da implementação de uma arquitetura de software robusta e escalável.

O desenvolvimento fundamentou-se nos princípios do Domain-Driven Design (DDD) e da Clean Architecture, priorizando o desacoplamento entre componentes, a testabilidade e a aplicação de práticas modernas de DevOps.

## Tecnologias e Práticas

- **Núcleo:** C# (.NET 9) e Entity Framework Core.
- **Banco de Dados:** PostgreSQL.
- **Garantia de Qualidade:** Testes unitários implementados com xUnit e validações de dados via FluentValidation.
- **Documentação:** Swagger / OpenAPI (utilizando Swashbuckle) para a especificação detalhada dos endpoints.
- **Hospedagem:** Azure App Service (ambiente Linux).
- **CI/CD:** GitHub Actions para automação dos processos de integração e entrega.
- **Monitoramento:** Integração de Health Checks para verificação de integridade do sistema.

## Arquitetura e Organização do Projeto

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
```
git clone [https://github.com/Ghitado/expense-control-backend.git](https://github.com/Ghitado/expense-control-backend.git)
```
2. Configure a string de conexão com o PostgreSQL no arquivo `appsettings.Development.json`.
```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=ExpenseControl;Username=postgres;Password=sua_senha"
}
```
3. Execute o comando `dotnet restore` para baixar as dependências.
4. Execute o projeto API via Visual Studio ou `dotnet run`.
5. Acesse a documentação via `/swagger`.

## Melhorias Futuras

- [ ] Implementar processamento assíncrono de relatórios usando MassTransit e RabbitMQ (CloudAMQP).
- [ ] Implementação de Rate Limiting para proteção da API contra uso abusivo.