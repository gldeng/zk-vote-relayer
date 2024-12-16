using AutoMapper;
using ZkVoteRelayer.Authors;

namespace ZkVoteRelayer;

public class ZkVoteRelayerApplicationAutoMapperProfile : Profile
{
    public ZkVoteRelayerApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        
        //Example related, can be removed
        CreateMap<Author, AuthorDto>();

    }
}
