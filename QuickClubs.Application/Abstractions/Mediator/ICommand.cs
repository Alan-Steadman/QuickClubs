using MediatR;
using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Application.Abstractions.Mediator;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand // IBaseCommand is used as a marker to identify commands (vs queries) eg when setting up a logging pipeline behaviour
{
}