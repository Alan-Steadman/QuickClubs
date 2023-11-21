using MediatR;
using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Application.Abstractions.Mediator;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
