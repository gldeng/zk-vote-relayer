namespace ZkVoteRelayer.Domain.Grains.TxRelay;

[GenerateSerializer]
public class LogEventDto
{
    [Id(0)] public string Address { get; set; }

    [Id(1)] public string Name { get; set; }

    [Id(2)] public string[] Indexed { get; set; }

    [Id(3)] public string NonIndexed { get; set; }

    public static LogEventDto FromClientDto(AElf.Client.Dto.LogEventDto dto)
    {
        return new LogEventDto
        {
            Address = dto.Address,
            Name = dto.Name,
            Indexed = dto.Indexed,
            NonIndexed = dto.NonIndexed
        };
    }
}

[GenerateSerializer]
public class TransactionDto
{
    [Id(0)] public string From { get; set; }

    [Id(1)] public string To { get; set; }

    [Id(2)] public long RefBlockNumber { get; set; }

    [Id(3)] public string RefBlockPrefix { get; set; }

    [Id(4)] public string MethodName { get; set; }

    [Id(5)] public string Params { get; set; }

    [Id(6)] public string Signature { get; set; }

    public static TransactionDto FromClientDto(AElf.Client.Dto.TransactionDto dto)
    {
        return new TransactionDto
        {
            From = dto.From,
            To = dto.To,
            RefBlockNumber = dto.RefBlockNumber,
            RefBlockPrefix = dto.RefBlockPrefix,
            MethodName = dto.MethodName,
            Params = dto.Params,
            Signature = dto.Signature
        };
    }
}

[GenerateSerializer]
public class TransactionResultDto
{
    [Id(0)] public string TransactionId { get; set; }

    [Id(1)] public string Status { get; set; }

    [Id(2)] public LogEventDto[] Logs { get; set; }

    [Id(3)] public string Bloom { get; set; }

    [Id(4)] public long BlockNumber { get; set; }

    [Id(5)] public string BlockHash { get; set; }

    [Id(6)] public TransactionDto Transaction { get; set; }

    [Id(7)] public string ReturnValue { get; set; }

    [Id(8)] public string Error { get; set; }

    public static TransactionResultDto FromClientDto(AElf.Client.Dto.TransactionResultDto dto)
    {
        return new TransactionResultDto
        {
            TransactionId = dto.TransactionId,
            Status = dto.Status,
            Logs = dto.Logs.Select(LogEventDto.FromClientDto).ToArray(),
            Bloom = dto.Bloom,
            BlockNumber = dto.BlockNumber,
            BlockHash = dto.BlockHash,
            Transaction = dto.Transaction != null ? TransactionDto.FromClientDto(dto.Transaction) : null,
            ReturnValue = dto.ReturnValue,
            Error = dto.Error
        };
    }
}