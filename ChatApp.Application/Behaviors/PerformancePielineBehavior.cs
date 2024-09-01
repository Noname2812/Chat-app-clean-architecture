

using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ChatApp.Application.Behaviors
{
    public class PerformancePielineBehavior<TRequest, TRespone> : 
        IPipelineBehavior<TRequest, TRespone> where TRequest : IRequest<TRespone>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        public PerformancePielineBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }
        public async Task<TRespone> Handle(TRequest request, RequestHandlerDelegate<TRespone> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var respone = await next();
            _timer.Stop();
            var elapseMilliseconds = _timer.ElapsedMilliseconds;
            if(elapseMilliseconds < 5000)
            {
                return respone;
            }
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Long time Running - Request detail {name} ({ElapsedMilliseconds} milliseconds) {@Request}", requestName, elapseMilliseconds, request);
            return respone;
        }
    }
}
