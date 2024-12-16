using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace ZkVoteRelayer.MongoDB;

[DependsOn(
    typeof(ZkVoteRelayerApplicationTestModule),
    typeof(ZkVoteRelayerMongoDbModule)
)]
public class ZkVoteRelayerMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = ZkVoteRelayerMongoDbFixture.GetRandomConnectionString();
        });
    }
}
