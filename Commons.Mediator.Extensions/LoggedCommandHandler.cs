using Commons.Mediator.Requests;
using Microsoft.Extensions.Logging;

namespace Commons.Mediator
{
    public abstract class LoggedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly ILogger<LoggedRequestHandler<TRequest, TResponse>> logger;
        protected LoggedRequestHandler(ILogger<LoggedRequestHandler<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default)
        {
            TResponse result = default!;

            try
            {
                result = await this.Implementation(request, cancellationToken);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }

            return result;
        }

        protected abstract Task<TResponse> Implementation(TRequest request, CancellationToken cancellationToken = default);
    }
}
