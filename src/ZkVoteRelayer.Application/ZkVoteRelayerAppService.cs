using System;
using System.Collections.Generic;
using System.Text;
using ZkVoteRelayer.Localization;
using Volo.Abp.Application.Services;

namespace ZkVoteRelayer;

/* Inherit your application services from this class.
 */
public abstract class ZkVoteRelayerAppService : ApplicationService
{
    protected ZkVoteRelayerAppService()
    {
        LocalizationResource = typeof(ZkVoteRelayerResource);
    }
}
