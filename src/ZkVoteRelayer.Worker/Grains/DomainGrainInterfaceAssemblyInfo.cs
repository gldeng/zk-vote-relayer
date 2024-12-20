using ZkVoteRelayer.Domain.Grains.Authors;
using Orleans;
using ZkVoteRelayer.Domain.Grains.TxRelay;

[assembly: GenerateCodeForDeclaringAssembly(typeof(INameValidator))]
[assembly: GenerateCodeForDeclaringAssembly(typeof(IVoteRelayJob))]
//add more grain interfaces below this line

namespace ZkVoteRelayer.Grains;