using Azure.Core;
using Azure;
using Fiap.TechChallenge.One.API.Extensions;
using Fiap.TechChallenge.One.API.Infrastructure;
using Fiap.TechChallenge.One.Application.Contatos;
using Fiap.TechChallenge.One.Application.Contatos.Atualizar;
using Fiap.TechChallenge.One.Application.Contatos.Criar;
using Fiap.TechChallenge.One.Application.Contatos.Excluir;
using Fiap.TechChallenge.One.Application.Contatos.Listar;
using Fiap.TechChallenge.One.Domain.Kernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.TechChallenge.One.API.Endpoints;

public class Contatos : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        /// <summary>
        /// Obtem lista de contatos
        /// </summary>
        /// <param name="ddd">Parametro opcional para filtro de contatos por ddd</param>
        /// <returns>Será retornado uma lista com todos os contatos</returns>
        /// <response code="200">Solicitação executada com sucesso</response>
        /// <response code="400">Caso o ddd informado seja inválido</response>
        app.MapGet("contatos", async (
            [FromQuery] string? ddd,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<IEnumerable<ContatoResponse>> result = await sender.Send(new ListarContatosQuery(ddd), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        });

        /// <summary>
        /// Cadastra um novo contato
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// {
        ///     "email": "joao@tes.com",
        ///     "nome": "Joao Teste",
        ///     "telefone": "956432451",
        ///     "ddd": "21"
        /// }
        /// </remarks>
        /// <returns>Será retornado um Guid com id do contato gerado</returns>
        /// <response code="200">Solicitação executada com sucesso</response>
        app.MapPost("contatos", async (
            CriarContatoCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        });

        app.MapPut("contatos", async (
            AtualizarContatoCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        });

        app.MapDelete("contatos/{contatoId}", async (
            [FromRoute]Guid contatoId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(new ExcluirContatoCommand(contatoId), cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        });
    }
}