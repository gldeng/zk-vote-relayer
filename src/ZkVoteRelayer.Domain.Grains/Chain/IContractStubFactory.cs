using AElf.CSharp.Core;
using AElf.Types;

namespace ZkVoteRelayer.Domain.Grains.Chain;

public interface IContractStubFactory
{
    T GetInstance<T>(string chainName, string contractAddress)
        where T : ContractStubBase, new();

    T GetInstance<T>(string chainName, Address contractAddress)
        where T : ContractStubBase, new();
}