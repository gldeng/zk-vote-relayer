namespace ZkVoteRelayer.Domain.Grains.TxRelay;

[GenerateSerializer]
public class SupportedCall
{
    [Id(0)] public string ChainName { get; set; } = string.Empty;
    [Id(1)] public string ContractAddress { get; set; } = string.Empty;
    [Id(2)] public string MethodName { get; set; } = string.Empty;
}