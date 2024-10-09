using Fiap.TechChallenge.API.Extensions;
using Fiap.TechChallenge.Exclusao.API.Commands;
using Fiap.TechChallenge.Kernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fiap.TechChallenge.Exclusao.API.Endpoints;

[ApiController]
[Route("api/contatos")]
public class Contatos(ISender sender) : ControllerBase
{
    /// <summary>
    /// Endpoint utilizado para excluir um contato
    /// </summary>
    /// <param name="contatoId">Id do contato que se deseja excluir</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="204">Dados enviado para fila com sucesso para exclusão</response>
    /// <response code="404">Contato não encontrado</response>
    [HttpDelete("{contatoId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    public async Task<IResult> CriarContato([FromRoute] Guid contatoId, CancellationToken cancellationToken)
    {
        Result result = await sender.Send(new ExcluirContatoCommand(contatoId), cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
