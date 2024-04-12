using Fiap.TechChallenge.One.Domain.Kernel;
using MediatR;

namespace Fiap.TechChallenge.One.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}