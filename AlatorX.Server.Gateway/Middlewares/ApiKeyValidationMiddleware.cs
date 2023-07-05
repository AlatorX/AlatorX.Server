using AlatorX.Server.Gateway.Repositories;
using Yarp.ReverseProxy.Configuration;

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
        ILogger<ApiKeyValidationMiddleware> logger)
    {
        try
        {
            string apiKey = context.Request.Query[ApiKeyQueryParameter];

            if (!IsValidString(apiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (!(await userRepository.IsApiKeyExistsAsync(apiKey)))
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

    private bool IsValidString(string apiKey) =>
        !string.IsNullOrEmpty(apiKey);
}


public class Request
{
    public List<RouteConfig> Routes { get; set; }
    public List<ClusterConfig> Clusters { get; set; }
}