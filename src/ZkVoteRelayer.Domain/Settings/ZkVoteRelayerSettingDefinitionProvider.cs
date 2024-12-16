using Volo.Abp.Settings;

namespace ZkVoteRelayer.Settings;

public class ZkVoteRelayerSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ZkVoteRelayerSettings.MySetting1));
    }
}
