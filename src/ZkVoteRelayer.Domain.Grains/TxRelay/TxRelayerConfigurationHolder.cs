using Microsoft.Extensions.Options;
using ZkVoteRelayer.TxRelay;

namespace ZkVoteRelayer.Domain.Grains.TxRelay;

public class TxRelayerConfigurationHolder : Grain, ITxRelayerConfigurationHolder
{
    private readonly IOptionsSnapshot<SupportedCallsOptions> _supportedCallsOptions;

    public TxRelayerConfigurationHolder(IOptionsSnapshot<SupportedCallsOptions> supportedCallsOptions)
    {
        _supportedCallsOptions = supportedCallsOptions;
    }

    public async Task<List<SupportedCall>> GetSupportedCallsAsync()
    {
        var output = new List<SupportedCall>();
        output.AddRange(_supportedCallsOptions.Value.SupportedCalls ?? new List<SupportedCall>());
        return await Task.FromResult(output);
    }
}