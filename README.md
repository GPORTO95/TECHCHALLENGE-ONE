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

## :bookmark: Métodos

#### :bangbang: Atenção :bangbang:
- Propriedades marcado com o ícone :diamonds: são de preenchimento obrigatório

<details>
    <summary>[GET - Lista contatos]</summary>

`{urlbase}/api/v1/contatos`

- #### Caso de sucesso
    - Retornado lista de Contatos podendo filtrar por Ddd

- #### Query Params
    - **ddd** | string: Deve ser informado o ddd que deseja obter os contatos

- #### Exemplo Request
    - ##### Response - Será retornado uma lista
    ```
    [
        {
            "contatoId": "1e6fd294-5ad2-4d4c-8c2b-2bc23a5f45bc",
            "nome": "Gabriel Teste",
            "email": "teste@tes.com.br",
            "telefone": "956432451",
            "ddd": 21
        },
        {
            "contatoId": "7119a005-575f-4316-bcf4-c0b435b711f6",
            "nome": "Andre Teste",
            "email": "andre@tes.com.br",
            "telefone": "956432453",
            "ddd": 11
        }
    ]
    ```
</details>
<details>
    <summary>[POST - Inserir contato]</summary>
    
`{urlbase}/api/v1/contatos`

- #### Caso de sucesso
    - Inseri dados de contato na tabela com associação para ddd

- #### Use Case
    - Caso o `ddd` informado não exista, será retornado um 404 NotFound informando que não existe

- ### Validators
    - Caso o `email` informado não esteja em um formato válido, será retornado um 400 BadRequest 
    - Caso o `nome` informado não esteja em um formato válido, será retornado um 400 BadRequest  
    - Caso o `telefone` informado não esteja em um formato válido, será retornado um 400 BadRequest  
    - Caso o `ddd` informado não seja informado no padrão válido, será retornado um 400 BadRequest

- #### Atributos
    - <sup>(Obrigatório)</sup> **nome** | String: Deve ser informado o nome do contato
    - (Obrigatório) **email** | String: Deve ser informado o e-mail do contato
    - (Obrigatório) **telefone** | String: Deve ser informado o telefone do contato
    - (Obrigatório) **ddd** | String: Deve ser informado o ddd

- #### Exemplo Request
    - ##### Válido
    ```
    {
        "email": "teste@tes.com",
        "nome": "Gabriel Teste",
        "telefone": "956432451",
        "ddd": "11"
    }
    ```
    - ##### Response - Será retornado um Guid com o Id do contato
    ```
    "28eb0baa-e67a-4f64-86e1-cfa1326301c6"
    ```
    - ##### Validator - Email inválido
    ```
    {
        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        "title": "Email.FormatoInvalido",
        "status": 400,
        "detail": "Email está inválido"
    }
    ```
    - ##### Validator - Nome inválido
    ```
    {
        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        "title": "Nome.NomeIncompleto",
        "status": 400,
        "detail": "Informe o nome completo"
    }
    ```
    - ##### Validator - Telefone inválido
    ```
    {
        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        "title": "Telefone.FormatoInvalido",
        "status": 400,
        "detail": "Formato inválido, deve ser fornecido 9########"
    }
    ```
     - ##### Validator - Ddd inválido
    ```
    {
        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        "title": "CodigoRegiao.ValorInvalido",
        "status": 400,
        "detail": "O valor informado para DDD não é valido"
    }
    ```
     - ##### Use Case - ddd não encontrado
    ```
    {
        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        "title": "Ddd.NaoEncontrado",
        "status": 400,
        "detail": "Ddd não encontrado para o Valor = '19' informado"
    }
    ```
</details>

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

