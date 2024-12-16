using Volo.Abp.Modularity;

namespace ZkVoteRelayer;

/* Inherit from this class for your domain layer tests. */
public abstract class ZkVoteRelayerDomainTestBase<TStartupModule> : ZkVoteRelayerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
