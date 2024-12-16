using ZkVoteRelayer.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;

namespace ZkVoteRelayer.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(ZkVoteRelayerMongoDbModule),
    typeof(ZkVoteRelayerApplicationContractsModule)
    )]
public class ZkVoteRelayerDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "ZkVoteRelayer:"; });
    }
}
