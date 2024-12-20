using System.Collections.Generic;
using System.Threading.Tasks;
using AElf.Client.Dto;
using AElf.Types;
using Volo.Abp.Application.Services;

namespace ZkVoteRelayer.TxRelay;

public interface ITxRelayAppService : IApplicationService
{
    Task<List<SupportedCallDto>> GetSupportedCallsAsync();
    Task<TransactionResultDto> SubmitTransactionAsync(TxDto tx);
}