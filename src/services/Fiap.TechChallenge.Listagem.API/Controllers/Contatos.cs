using Fiap.TechChallenge.API.Extensions;
using Fiap.TechChallenge.Kernel;
using Fiap.TechChallenge.Listagem.API.Application.Contatos.Listar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fiap.TechChallenge.Listagem.API.Controllers;

[ApiController]
[Route("api/contatos")]
public class Contatos(ISender sender) : ControllerBase
{
    /// <summary>
    /// Obtem lista de contatos
    /// </summary>
    /// <param name="ddd">Parametro opcional para filtro de contatos por ddd</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Será retornado uma lista com todos os contatos</returns>
    /// <response code="200">Solicitação executada com sucesso</response>
    /// <response code="400">Caso o ddd informado seja inválido</response>
    [HttpGet]
    [ProducesResponseType(typeof(ContatoResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    public async Task<IResult> ListarContatos([FromQuery] string? ddd, CancellationToken cancellationToken)
    {
        Result<IEnumerable<ContatoResponse>> result = await sender.Send(new ListarContatosQuery(ddd), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
