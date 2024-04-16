## :fire: FIAP | Desafio - TechChallenge One
O objetivo do desafio é construir uma API em .Net 8 para que seja possível realizar a manutenção de contatos, sendo possível, cadastrar, atualizar, consultar (podendo filtrar por ddd) e até, excluir um contato. Exigir os seguintes campos para cadastro.
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
- Unit Of Work

## :building_construction: Arquitetura 
- **Application**: CQRS, interfaces para serviços externos, pipeline behaviors e validações de comandos.com fluent validator
- **Domain**: Classes compartilhadas, entidades, objetos de valor, interface de repositorios e mensagens de erros
- **Infrastructure**: Camada de acesso a dados, cache e classes concretas de acesso a serviços externos
- **Application.UnitTests**: Testes unitário para classes de comandos, queries e validações
- **Domain.UnitTests**: Testes unitário para entidades e objetos de valor

## :bookmark: Gerenciamento de Contatos
GET - Lista contatos
- `{urlbase}/api/v1/contatos`

POST - Inseri um novo contato
- `{urlbase}/api/v1/contatos`

PUT - Atualiza dados de contato
- `{urlbase}/api/v1/contatos`

DELETE - Remove o cadastro de um contato
- `{urlbase}/api/v1/contatos`

## :warning: Requisitos
- Ter o docker instalado com imagem do SQL Server ou SQL Server Management Studio
- Caso opte pelo docker, basta seguir este link para dowload da Imagem do SQL [link](https://balta.io/blog/sql-server-docker)

## :zap: Running
- Baixar o projeto do GitHub
- Excluir pasta de Migrations na camada de infrastructure somente CASO exista.
- Alterar conexão com o banco de dados no arquivo de appsettings
- Executar o comando `add-migration inicial` e em seguida `update-databse`, não se esqueça de apontar para camada de Infrastructure para que pegue o contexto correto
- E por fim, executar a aplicação
- Caso prefira, você pode optar por importar a collection no seu postman de teste na pasta de collections

