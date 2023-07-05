using AlatorX.Server.Gateway.Middlewares;
using AlatorX.Server.Gateway.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Yarp.ReverseProxy.Configuration;

namespace AlatorX.Server.Gateway.Configurations;

public class CustomProxyConfigProvider : IProxyConfigProvider
{
    private readonly InMemoryConfigProvider configuration = new(
        Array.Empty<Yarp.ReverseProxy.Configuration.RouteConfig>(),
        Array.Empty<Yarp.ReverseProxy.Configuration.ClusterConfig>());

    private readonly IServiceProvider serviceProvider;

    public CustomProxyConfigProvider(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;

        _ = Task.Run(UpdateConfigAsync);
    }

    public IProxyConfig GetConfig() => this.configuration.GetConfig();

    public async Task UpdateConfigAsync()
    {
        using var scope = this.serviceProvider.CreateScope();
        var websiteRepository = scope.ServiceProvider.GetRequiredService<IWebsiteRepository>();

        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(30));

        do
        {
            try
            {
                var websiteConfigurations = await websiteRepository
                    .SelectAllWebsites()
                    .Select(website => website.ConfigString)
                    .ToListAsync();

                var routes = new List<RouteConfig>();
                var clusters = new List<ClusterConfig>();

                foreach (string configuration in websiteConfigurations)
                {
                    var siteConfigDeserialization = JsonSerializer.Deserialize<Request>(configuration);

                    routes.AddRange(siteConfigDeserialization.Routes);
                    clusters.AddRange(siteConfigDeserialization.Clusters);
                }

                this.configuration.Update(routes, clusters);
            }
            catch(Exception exception)
            {

            }
        } while (await timer.WaitForNextTickAsync());
    }
}