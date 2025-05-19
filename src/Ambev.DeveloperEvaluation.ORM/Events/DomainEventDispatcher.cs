using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.ORM.Events;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(DomainEvent domainEvent)
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());

        var handlers = _serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            await ((Task)handlerType.GetMethod("Handle").Invoke(handler, new[] { domainEvent }));
        }
    }
}

