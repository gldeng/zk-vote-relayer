using System.ComponentModel.DataAnnotations;

namespace ZkVoteRelayer.TxRelay;

public class VoteDetailsDto
{
    [Required] public string VotingItemId { get; set; }
    [Required] public int VoteOption { get; set; }
    public long VoteAmount { get; set; }
    [Required] public string NullifierHash { get; set; }
    [Required] public ProofDto Proof { get; set; }
}

public class ProofDto
{
    public string[] PiA { get; set; }
    public string[][] PiB { get; set; }
    public string[] PiC { get; set; }
}