using AElf.OpenTelemetry;
using ZkVoteRelayer.Domain.Grains;
using Microsoft.Extensions.DependencyInjection;
using ZkVoteRelayer.Application.Grains;
using Serilog;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
namespace ZkVoteRelayer.Silo;

[DependsOn(
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAutofacModule),
    typeof(DomainGrainsModule),
    typeof(OpenTelemetryModule)
)]
public class SiloModule : AbpModule, IDomainGrainsModule, IApplicationGrainsModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<SiloModule>(); });
        context.Services.AddHostedService<ZkVoteRelayerHostedService>();
        var configuration = context.Services.GetConfiguration();
        //add dependencies here
        context.Services.AddSerilog(loggerConfiguration => {},
            true, writeToProviders: true);

        context.Services.AddHttpClient();
    }
}