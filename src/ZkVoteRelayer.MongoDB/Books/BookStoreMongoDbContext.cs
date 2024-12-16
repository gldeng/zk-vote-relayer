using MongoDB.Driver;
using ZkVoteRelayer.Authors;
using Volo.Abp.MongoDB;

namespace ZkVoteRelayer.Books;

public class BookStoreMongoDbContext : AbpMongoDbContext
{
    public IMongoCollection<Book> Books => Collection<Book>();
    public IMongoCollection<Author> Authors => Collection<Author>();

}
