using ZkVoteRelayer.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ZkVoteRelayer.Permissions;

public class ZkVoteRelayerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ZkVoteRelayerPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ZkVoteRelayerPermissions.MyPermission1, L("Permission:MyPermission1"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ZkVoteRelayerResource>(name);
    }
}
