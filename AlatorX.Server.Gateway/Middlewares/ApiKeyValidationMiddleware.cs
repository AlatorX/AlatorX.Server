using AlatorX.Server.Gateway.Configurations;
using AlatorX.Server.Gateway.Repositories;

namespace AlatorX.Server.Gateway.Middlewares;

public class ApiKeyValidationMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyQueryParameter = "api_key";

    public ApiKeyValidationMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IUserRepository userRepository,
        CustomProxyConfigProvider proxyConfigProvider,
        ILogger<ApiKeyValidationMiddleware> logger)
    {
        try
        {
            string apiKey = context.Request.Query[ApiKeyQueryParameter];

            if (!IsValidApiKey(apiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (!await userRepository.IsApiKeyExistsAsync(apiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
        catch(Exception exception)
        {
            logger.LogError(exception.Message);
        }
    }

    private void UpdateProxyConfig(
        CustomProxyConfigProvider proxyConfigProvider)
    {

    }

    private bool IsValidApiKey(string apiKey) =>
        !string.IsNullOrEmpty(apiKey);
}
