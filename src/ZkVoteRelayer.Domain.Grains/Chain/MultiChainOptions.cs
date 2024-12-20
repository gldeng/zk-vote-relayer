namespace ZkVoteRelayer.Domain.Grains.Chain;

public class MultiChainOptions
{
    public Dictionary<string, ChainOptions> ChainOptions { get; set; }
}