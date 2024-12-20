using ZkVoteRelayer.TxRelay;

namespace ZkVoteRelayer.Domain.Grains.TxRelay;

public interface ITxRelayerConfigurationHolder : IGrainWithIntegerKey
{
    Task<List<SupportedCall>> GetSupportedCallsAsync();
}