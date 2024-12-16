using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ZkVoteRelayer.Data;

/* This is used if database provider does't define
 * IZkVoteRelayerDbSchemaMigrator implementation.
 */
public class NullZkVoteRelayerDbSchemaMigrator : IZkVoteRelayerDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
