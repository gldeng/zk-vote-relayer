using Volo.Abp.Application.Dtos;

namespace ZkVoteRelayer.Authors;

public class GetAuthorListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
