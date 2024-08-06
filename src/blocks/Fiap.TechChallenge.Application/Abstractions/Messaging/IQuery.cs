using Fiap.TechChallenge.Kernel;
using MediatR;

namespace Fiap.TechChallenge.One.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}