using FiapCloudGames.Domain.Services;
using Microsoft.Extensions.Primitives;

namespace FiapCloudGames.Api.Configurations
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _correlationIdHeader = "x-correlation-id";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ICorrelationIdService correlationIdService)
        {
            var correlationId = GetCorrelationId(httpContext, correlationIdService);
            AddCorrelationIdHeaderToResponse(httpContext, correlationId);

            using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId)) await _next(httpContext);
        }
        private static StringValues GetCorrelationId(HttpContext context, ICorrelationIdService correlationIdService)
        {
            var vlrDefinido = context.Request.Headers[_correlationIdHeader].FirstOrDefault()?.ToString();

            var correlationId = string.IsNullOrEmpty(vlrDefinido) ? Guid.NewGuid().ToString() : vlrDefinido;

            correlationIdService.Set(correlationId);

            return correlationId;
        }

        private static void AddCorrelationIdHeaderToResponse(HttpContext context, StringValues correlationId)
        => context.Response.OnStarting(() =>
        {
            context.Response.Headers[_correlationIdHeader] = new[] { correlationId.ToString() };
            return Task.CompletedTask;
        });
    }
}
