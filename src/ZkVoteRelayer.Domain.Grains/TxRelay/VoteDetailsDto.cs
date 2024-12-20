namespace ZkVoteRelayer.Domain.Grains.TxRelay;

[GenerateSerializer]
public class VoteDetailsDto
{
    [Id(0)] public string VotingItemId { get; set; }
    [Id(1)] public int VoteOption { get; set; }
    [Id(2)] public long VoteAmount { get; set; }
    [Id(3)] public string NullifierHash { get; set; }
    [Id(4)] public ProofDto Proof { get; set; }
}

[GenerateSerializer]
public class ProofDto
{
    [Id(0)] public string[] PiA { get; set; }
    [Id(1)] public string[][] PiB { get; set; }
    [Id(2)] public string[] PiC { get; set; }
}