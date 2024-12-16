using Microsoft.Extensions.DependencyInjection;
using ZkVoteRelayer.Application.Grains;
using ZkVoteRelayer.Domain.Grains;
using ZkVoteRelayer.MongoDB;
using ZkVoteRelayer.Worker.Author;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Modularity;

namespace ZkVoteRelayer.Worker;

[DependsOn(
    typeof(AbpBackgroundWorkersModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(ZkVoteRelayerApplicationModule),
    typeof(ZkVoteRelayerMongoDbModule),
    typeof(AbpAutofacModule)
)]
public class ZkVoteRelayerWorkerModule : AbpModule, IDomainGrainsModule, IApplicationGrainsModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.AddHttpClient();
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        // add your background workers here
        await context.AddBackgroundWorkerAsync<AuthorSummaryWorker>();
    }
}