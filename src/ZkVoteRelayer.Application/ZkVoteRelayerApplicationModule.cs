using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using ZkVoteRelayer.Domain.Grains;

namespace ZkVoteRelayer;

[DependsOn(
    typeof(ZkVoteRelayerDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(ZkVoteRelayerApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule)
)]
public class ZkVoteRelayerApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<ZkVoteRelayerApplicationModule>(); });
    }
}