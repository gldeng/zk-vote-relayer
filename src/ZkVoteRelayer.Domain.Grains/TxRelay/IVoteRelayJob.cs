using AElf.Client.Dto;
using ZkVoteRelayer.TxRelay;

namespace ZkVoteRelayer.Domain.Grains.TxRelay;

public interface IVoteRelayJob : IGrainWithStringKey
{
    Task<TransactionResultDto> SendTxAsync(VoteRelayDto request);
    Task<TransactionResultDto> GetTransactionResultAsync();
}