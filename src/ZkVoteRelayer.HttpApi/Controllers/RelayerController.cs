using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using ZkVoteRelayer.Domain.Grains.TxRelay;
using ZkVoteRelayer.TxRelay;

namespace ZkVoteRelayer.Controllers;

[RemoteService]
[Area("app")]
[ControllerName("Relayer")]
public class RelayerController : ZkVoteRelayerController
{
    private ITxRelayAppService _txRelayAppService;

    public RelayerController(ITxRelayAppService txRelayAppService)
    {
        _txRelayAppService = txRelayAppService;
    }

    [HttpGet("supportedCalls")]
    public async Task<List<SupportedCallDto>> SupportedCalls()
    {
        return await _txRelayAppService.GetSupportedCallsAsync();
    }

    [HttpPost("relayTx")]
    public async Task<SubmittedTxDto> RelayTransaction(TxDto tx)
    {
        return await _txRelayAppService.SubmitTransactionAsync(tx);
    }
}