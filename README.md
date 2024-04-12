## :fire: FIAP | Desafio TechChallenge One | O Desafio
Contruir uma API em .Net 8 para que seja possível realizar o cadastro, atualização, consulta e exclusão de contatos que contenha os seguintes campos.
- (Obrigátorio) Nome 
- (Obrigátorio) Email
- (Obrigátorio) Telefone
- (Obrigátorio) Associação de DDD

## :woman_technologist: Tecnologias
- .NET 8
- Entity Framework Core 8
- FluentValidator
- MediatR
- Teste de unidade

## :building_construction: Arquitetura 
- **Application**: CQRS, interfaces para serviços externos, pipeline behaviors e validações de comandos.com fluent validator
- **Domain**: Classes compartilhadas, entidades, objetos de valor, interface de repositorios e mensagens de erros
- **Infrastructure**: Camada de acesso a dados, cache e classes concretas de acesso a serviços externos
- **Application.UnitTests**: Testes unitário para classes de comandos, queries e validações
- **Domain.UnitTests**: Testes unitário para entidades e objetos de valor
