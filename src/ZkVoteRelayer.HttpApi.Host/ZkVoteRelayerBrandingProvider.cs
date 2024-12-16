using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ZkVoteRelayer;

[Dependency(ReplaceServices = true)]
public class ZkVoteRelayerBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ZkVoteRelayer";
}
