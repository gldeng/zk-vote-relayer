using ZkVoteRelayer.Samples;
using Xunit;

namespace ZkVoteRelayer.MongoDB.Domains;

[Collection(ZkVoteRelayerTestConsts.CollectionDefinitionName)]
public class MongoDBSampleDomainTests : SampleDomainTests<ZkVoteRelayerMongoDbTestModule>
{

}
