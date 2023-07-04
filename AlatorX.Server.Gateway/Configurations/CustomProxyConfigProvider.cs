using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace AlatorX.Server.Gateway.Configurations;

public class CustomProxyConfigProvider : IProxyConfigProvider
{
   
    private CustomMemoryConfig memoryConfig;

    public CustomProxyConfigProvider()
    {
        memoryConfig = new CustomMemoryConfig(
            Array.Empty<RouteConfig>(),
            Array.Empty<ClusterConfig>());
    }

    public IProxyConfig GetConfig() => this.memoryConfig;

    public void Update(
        IReadOnlyList<RouteConfig> routes,
        IReadOnlyList<ClusterConfig> clusters)
    {
        var oldConfig = this.memoryConfig;

        this.memoryConfig = new CustomMemoryConfig(routes, clusters);

        oldConfig.SignalChange();
    }

    private class CustomMemoryConfig : IProxyConfig
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public CustomMemoryConfig(
            IReadOnlyList<RouteConfig> routes,
            IReadOnlyList<ClusterConfig> clusters)
        {
            Routes = routes;
            Clusters = clusters;
            ChangeToken = new CancellationChangeToken(_cts.Token);
        }

        public IReadOnlyList<RouteConfig> Routes { get; }

        public IReadOnlyList<ClusterConfig> Clusters { get; }

        public IChangeToken ChangeToken { get; }

        internal void SignalChange()
        {
            _cts.Cancel();
        }
    }
}