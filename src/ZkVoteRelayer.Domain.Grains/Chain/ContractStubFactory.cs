using AElf.Cryptography;
using AElf.CSharp.Core;
using AElf.Types;
using ZkVoteRelayer.Domain.Grains.KeyStore;

namespace ZkVoteRelayer.Domain.Grains.Chain;



public class ContractStubFactory : IContractStubFactory
{
    private readonly IAElfClientFactory _clientFactory;
    private readonly IKeyStore _keyStore;

    public ContractStubFactory(IAElfClientFactory clientFactory, IKeyStore keyStore)
    {
        _clientFactory = clientFactory;
        _keyStore = keyStore;
    }


    public T GetInstance<T>(string chainName, string contractAddress)
        where T : ContractStubBase, new()
    {
        return GetInstance<T>(chainName, Address.FromBase58(contractAddress));
    }

    public T GetInstance<T>(string chainName, Address contractAddress)
        where T : ContractStubBase, new()
    {
        var privateKey = _keyStore.GetPrivateKey();
        var senderKey = CryptoHelper.FromPrivateKey(privateKey);
        return new T
        {
            __factory = new MethodStubFactory(_clientFactory.GetClient(chainName), senderKey, contractAddress)
        };
    }
}