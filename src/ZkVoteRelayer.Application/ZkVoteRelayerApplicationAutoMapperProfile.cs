using AutoMapper;
using ZkVoteRelayer.Authors;
using ZkVoteRelayer.Domain.Grains.TxRelay;
using ZkVoteRelayer.TxRelay;
using ProofDto = ZkVoteRelayer.TxRelay.ProofDto;
using VoteDetailsDto = ZkVoteRelayer.TxRelay.VoteDetailsDto;

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
        CreateMap<SupportedCall, SupportedCallDto>();
        CreateMap<TxDto, VoteRelayDto>();
        CreateMap<VoteDetailsDto, Domain.Grains.TxRelay.VoteDetailsDto>();
        CreateMap<ProofDto, Domain.Grains.TxRelay.ProofDto>();
        CreateMap<TransactionResultDto, AElf.Client.Dto.TransactionResultDto>();
        CreateMap<LogEventDto, AElf.Client.Dto.LogEventDto>();
        CreateMap<TransactionDto, AElf.Client.Dto.TransactionDto>();
    }
}