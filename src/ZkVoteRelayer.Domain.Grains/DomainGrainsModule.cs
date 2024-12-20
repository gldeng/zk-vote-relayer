using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using ZkVoteRelayer.Domain.Grains.Chain;
using ZkVoteRelayer.Domain.Grains.KeyStore;
using ZkVoteRelayer.Domain.Grains.TxRelay;

namespace ZkVoteRelayer.Domain.Grains;

public class DomainGrainsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IAElfClientFactory, AElfClientFactory>();
        context.Services.AddSingleton<IKeyStore, KeyStore.KeyStore>();
        context.Services.AddSingleton<IContractStubFactory, ContractStubFactory>();

        var configuration = context.Services.GetConfiguration();
        context.Services.Configure<SupportedCallsOptions>(
            configuration.GetSection("SupportedCallsOptions")
        );
        context.Services.Configure<KeyStoreOptions>(
            configuration.GetSection("KeyStoreOptions")
        );
        context.Services.Configure<MultiChainOptions>(
            configuration.GetSection("MultiChainOptions")
        );
    }
}