using Volo.Abp.Modularity;

namespace ZkVoteRelayer;

[DependsOn(
    typeof(ZkVoteRelayerDomainModule),
    typeof(ZkVoteRelayerTestBaseModule)
)]
public class ZkVoteRelayerDomainTestModule : AbpModule
{

}
