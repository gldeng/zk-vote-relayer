using ZkVoteRelayer.TxRelay;

namespace ZkVoteRelayer.Domain.Grains.TxRelay;

public class SupportedCallsOptions
{
    public List<SupportedCall> SupportedCalls { get; set; }
}