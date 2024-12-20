using System.ComponentModel.DataAnnotations;
using AElf;

namespace ZkVoteRelayer.TxRelay;

public class TxDto
{
    [Required] public string ChainName { get; set; } = string.Empty;
    [Required] public string ContractAddress { get; set; } = string.Empty;
    [Required] public VoteDetailsDto VoteDetails { get; set; }

    public override string ToString()
    {
        return $"{ChainName}/{ContractAddress}";
    }

    public string ToJobId()
    {
        return HashHelper.ComputeFrom(ToString()).ToHex();
    }
}