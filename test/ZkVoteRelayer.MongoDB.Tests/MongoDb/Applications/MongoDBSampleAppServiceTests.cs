using ZkVoteRelayer.MongoDB;
using ZkVoteRelayer.Samples;
using Xunit;

namespace ZkVoteRelayer.MongoDb.Applications;

[Collection(ZkVoteRelayerTestConsts.CollectionDefinitionName)]
public class MongoDBSampleAppServiceTests : SampleAppServiceTests<ZkVoteRelayerMongoDbTestModule>
{

}
