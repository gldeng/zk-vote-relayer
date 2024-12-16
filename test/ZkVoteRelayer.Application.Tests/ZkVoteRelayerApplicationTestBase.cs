using Volo.Abp.Modularity;

namespace ZkVoteRelayer;

public abstract class ZkVoteRelayerApplicationTestBase<TStartupModule> : ZkVoteRelayerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
