using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ZkVoteRelayer.TxRelay;

public interface ITxRelayAppService : IApplicationService
{
    Task<List<SupportedCallDto>> GetSupportedCallsAsync();
    Task<SubmittedTxDto> SubmitTransactionAsync(TxDto tx);
}