using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Infrastructure.Persistence;
using QuickClubs.Presentation.Controllers;
using QuickClubs.WebApi.Middleware;
using System.Reflection;

namespace QuickClubs.ArchitectureTests;

public class BaseTest
{
    protected static Assembly DomainAssembly => typeof(IEntity).Assembly;
    protected static Assembly ApplicationAssembly => typeof(ICommand).Assembly;
    protected static Assembly InfrastructureAssembly => typeof(ApplicationDbContext).Assembly;
    protected static Assembly PresentationAssembly => typeof(ApiController).Assembly;
    protected static Assembly ContractsAssembly => typeof(Contracts.AssemblyMarker).Assembly;
    protected static Assembly WebAssembly => typeof(ExceptionHandlingMiddleware).Assembly;
}