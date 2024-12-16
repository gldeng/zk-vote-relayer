using System.Threading.Tasks;

namespace ZkVoteRelayer.Data;

public interface IZkVoteRelayerDbSchemaMigrator
{
    Task MigrateAsync();
}
