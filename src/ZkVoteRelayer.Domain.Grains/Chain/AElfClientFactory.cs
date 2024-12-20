using System.Collections.Concurrent;
using AElf.Client;
using Microsoft.Extensions.Options;

namespace ZkVoteRelayer.Domain.Grains.Chain;

public class AElfClientFactory : IAElfClientFactory
{
    private readonly MultiChainOptions _options;

    private readonly ConcurrentDictionary<string, AElfClient> _clientDic =
        new ConcurrentDictionary<string, AElfClient>();

    public AElfClientFactory(IOptionsSnapshot<MultiChainOptions> options)
    {
        _options = options.Value;
    }

    public AElfClient GetClient(string chainName)
    {
        var chainInfo = _options.ChainOptions[chainName];
        if (chainInfo == null)
        {
            throw new Exception($"Chain {chainName} is not supported.");
        }

        if (_clientDic.TryGetValue(chainName, out var client))
        {
            return client;
        }

        client = new AElfClient(chainInfo.BaseUrl);
        _clientDic[chainName] = client;
        return client;
    }
}