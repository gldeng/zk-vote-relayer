using Localization.Resources.AbpUi;
using ZkVoteRelayer.Localization;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;

namespace ZkVoteRelayer;

[DependsOn(
    typeof(ZkVoteRelayerApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule)
    )]
public class ZkVoteRelayerHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ZkVoteRelayerResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
