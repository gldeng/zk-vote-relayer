using ZkVoteRelayer.TxRelay;

namespace ZkVoteRelayer.Domain.Grains.TxRelay;

public interface IVoteRelayJob : IGrainWithStringKey
{
    Task<string> SendTxAsync(VoteRelayDto request);
}