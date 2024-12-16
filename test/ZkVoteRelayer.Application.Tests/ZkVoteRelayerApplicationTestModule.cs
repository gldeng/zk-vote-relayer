using Volo.Abp.Modularity;

namespace ZkVoteRelayer;

[DependsOn(
    typeof(ZkVoteRelayerApplicationModule),
    typeof(ZkVoteRelayerDomainTestModule)
)]
public class ZkVoteRelayerApplicationTestModule : AbpModule
{

}
