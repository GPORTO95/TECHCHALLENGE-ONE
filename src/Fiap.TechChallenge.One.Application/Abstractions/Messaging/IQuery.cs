using Fiap.TechChallenge.One.Domain.Kernel;
using MediatR;

namespace Fiap.TechChallenge.One.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}