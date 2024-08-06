using Fiap.TechChallenge.API.Extensions;
using Fiap.TechChallenge.Cadastro.Commands;
using Fiap.TechChallenge.Kernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fiap.TechChallenge.Cadastro.Controllers;

[ApiController]
[Route("api/contatos")]
public class Contatos(ISender sender) : ControllerBase
{
    /// <summary>
    /// Endpoint utilizado para criar um contato
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retornado um Guid que representa o Id gerado</returns>
    /// <response code="200">Dados inserido com sucesso</response>
    /// <response code="400">Problemas de validação</response>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IResult> CriarContato([FromBody] CriarContatoCommand command, CancellationToken cancellationToken)
    {
        Result<Guid> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
