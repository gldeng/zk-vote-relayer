using AElf.Client;

namespace ZkVoteRelayer.Domain.Grains.Chain;

public interface IAElfClientFactory
{
    AElfClient GetClient(string chainName);
}