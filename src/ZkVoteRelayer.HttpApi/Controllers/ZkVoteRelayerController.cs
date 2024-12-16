using ZkVoteRelayer.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ZkVoteRelayer.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ZkVoteRelayerController : AbpControllerBase
{
    protected ZkVoteRelayerController()
    {
        LocalizationResource = typeof(ZkVoteRelayerResource);
    }
}
