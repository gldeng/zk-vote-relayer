using AElf;

namespace ZkVoteRelayer.Domain.Grains.Chain;

using AElf.Client;
using AElf.Client.Dto;
using AElf.Types;
using Google.Protobuf;

public static class Extensions
{
    public static async Task<TransactionResultDto> WaitForTransactionCompletionAsync(
        this AElfClient client,
        Hash transactionId)
    {
        const int maxRetries = 6; // TODO: Make this configurable
        const int initialDelayMs = 1000;

        var result = await RetryWithExponentialBackoff(maxRetries, initialDelayMs, async () =>
        {
            var result = await client.GetTransactionResultAsync(transactionId.ToHex());
            var status = ParseStatus(result.Status);

            switch (status)
            {
                case TransactionResultStatus.Pending:
                case TransactionResultStatus.PendingValidation:
                case TransactionResultStatus.NotExisted:
                    return (false, result); // Continue retrying
                case TransactionResultStatus.Failed:
                case TransactionResultStatus.Conflict:
                case TransactionResultStatus.NodeValidationFailed:
                case TransactionResultStatus.Mined:
                    return (true, result); // Transaction is mined, exit the retry loop
                default:
                    throw new Exception($"Unexpected transaction status: {status}");
            }
        });

        // ReSharper disable once ComplexConditionExpression
        if (ParseStatus(result.Status) == TransactionResultStatus.NotExisted)
        {
            throw new Exception($"Transaction does not exist after {maxRetries} retries");
        }

        return result;
    }

    public static (TransactionResult, ByteString) IntoProtobuf(this TransactionResultDto dto)
    {
        var status = dto.Status.ParseStatus();

        return (new TransactionResult()
        {
            BlockHash = dto.BlockHash != null
                ? Hash.LoadFromHex(dto.BlockHash)
                : null,
            BlockNumber = dto.BlockNumber,
            Bloom = dto.Bloom != null ? ByteString.FromBase64(dto.Bloom) : ByteString.Empty,
            ReturnValue = dto.ReturnValue != null
                ? ByteString.CopyFrom(ByteArrayHelper.HexStringToByteArray(dto.ReturnValue))
                : ByteString.Empty,
            Error = string.IsNullOrEmpty(dto.Error) ? "" : dto.Error,
            Logs =
            {
                dto.Logs.Select(x => new LogEvent()
                {
                    Address = Address.FromBase58(x.Address),
                    Indexed = { x.Indexed.Select(ByteString.FromBase64) },
                    Name = x.Name,
                    NonIndexed = ByteString.FromBase64(x.NonIndexed)
                })
            },
            Status = status,
            TransactionId = Hash.LoadFromHex(dto.TransactionId),
        }, ByteString.CopyFrom(ByteArrayHelper.HexStringToByteArray(dto.ReturnValue)));
    }


    public static TransactionResultStatus ParseStatus(this string value)
    {
        var mapping = new Dictionary<string, string>()
        {
            { "NOTEXISTED", "NOT_EXISTED" },
            { "PENDINGVALIDATION", "PENDING_VALIDATION" },
            { "NODEVALIDATIONFAILED", "NODE_VALIDATION_FAILED" }
        };

        // ReSharper disable once ComplexConditionExpression
        if (!Enum.TryParse<TransactionResultStatus>(value, true, out var status) &&
            mapping.TryGetValue(value, out var newValue) &&
            !Enum.TryParse<TransactionResultStatus>(newValue, true, out status))
        {
            throw new Exception($"Invalid transaction status: {value}");
        }

        return status;
    }

    private static async Task<TResult?> RetryWithExponentialBackoff<TResult>(int maxRetries, int initialDelayMs,
        Func<Task<(bool completed, TResult? result)>> operation)
    {
        var completed = false;
        TResult? result = default(TResult);
        for (var retry = 0; retry < maxRetries; retry++)
        {
            (completed, result) = await operation();
            if (completed)
            {
                return result; // Operation succeeded, return the result
            }

            if (retry < maxRetries - 1)
            {
                int delay = initialDelayMs * (int)Math.Pow(2, retry);
                await Task.Delay(delay);
            }
        }

        return result; // Return the final result if it's not NotExisted
    }

    public static async Task<RefBlockInfo> GetRefBlockInfoAsync(this AElfClient client)
    {
        var chain = await client.GetChainStatusAsync();
        var height = chain.LastIrreversibleBlockHeight;
        var prefix = BlockHelper.GetRefBlockPrefix(Hash.LoadFromHex(chain.LastIrreversibleBlockHash));
        return new RefBlockInfo(height, prefix);
    }
}