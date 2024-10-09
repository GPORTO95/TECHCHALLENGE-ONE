using Fiap.TechChallenge.API.Extensions;
using Fiap.TechChallenge.Atualizacao.API.Commands;
using Fiap.TechChallenge.Kernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fiap.TechChallenge.Atualizacao.API.Endpoints;

[ApiController]
[Route("api/contatos")]
public class Contatos(ISender sender) : ControllerBase
{
    /// <summary>
    /// Endpoint utilizado para atualizar um contato
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="204">Dados atualizado com sucesso</response>
    /// <response code="400">Problemas de validação</response>
    /// <response code="404">Contato não encontrado</response>
    [HttpPut]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    public async Task<IResult> CriarContato([FromBody] AtualizarContatoCommand command, CancellationToken cancellationToken)
    {
        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
