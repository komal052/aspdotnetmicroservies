using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviours
{
    public class UnhandlerExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest,TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandlerExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();

            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request :Unhandler Exception for request{name}{@request}", requestName, request);
                throw;

            }
        }
    }
}
