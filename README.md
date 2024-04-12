## :fire: FIAP | Desafio TechChallenge One | O Desafio
Contruir uma API em .Net 8 para que seja possível realizar o cadastro, atualização, consulta e exclusão de contatos que contenha os seguintes campos.
- (Obrigátorio) Nome 
- (Obrigátorio) Email
- (Obrigátorio) Telefone
- (Obrigátorio) Associação de DDD

## :building_construction: Arquitetura 3 camadas
- **Application**: Camada onde fica os comandos e queries, interfaces de serviços externos, pipelines e validações de comandos.
- **Domain**: Toda parte de entidade, objeto de valor, interface de repositorios e mensagens de erros
- **Infrastructure**: Camada de acesso a dados, cache e serviços externos
- **Application.UnitTests**: Testes unitário para classes de comandos, queries e validações
- **Domain.UnitTests**: Testes unitário para entidades e objetos de valor
