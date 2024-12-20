using Orleans;
namespace ZkVoteRelayer.Domain.Grains.TxRelay;

[GenerateSerializer]
public class VoteRelayDto
{
    [Id(0)] public string ChainName { get; set; }
    [Id(1)] public string ContractAddress { get; set; }
    [Id(2)] public VoteDetailsDto VoteDetails { get; set; }
}