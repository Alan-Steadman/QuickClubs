using MediatR;
using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Application.Abstractions.Mediator;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}