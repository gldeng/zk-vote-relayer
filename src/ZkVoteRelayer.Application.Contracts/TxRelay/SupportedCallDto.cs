using System.ComponentModel.DataAnnotations;

namespace ZkVoteRelayer.TxRelay;

public class SupportedCallDto
{
    [Required] public string ChainName { get; set; } = string.Empty;
    [Required] public string ContractAddress { get; set; } = string.Empty;
    [Required] public string MethodName { get; set; } = string.Empty;
}