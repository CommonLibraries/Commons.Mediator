using Commons.Mediator.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Commons.Mediator
{
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public RequestDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            var handler = this.serviceProvider.GetRequiredService<IRequestHandler<TRequest>>();
            await handler.Handle(request, cancellationToken);
        }

        public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
        {
            var handler = this.serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
            var result = await handler.Handle(request, cancellationToken);
            return result;
        }
    }
}
