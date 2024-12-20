using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AElf.OpenTelemetry;
using AElf.OpenTelemetry.ExecutionTime;
using AElf.Types;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Orleans;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;
using ZkVoteRelayer.Domain.Grains.TxRelay;

namespace ZkVoteRelayer.TxRelay;

[AggregateExecutionTime]
public class TxRelayAppService : ApplicationService, ITxRelayAppService
{
    private readonly IClusterClient _clusterClient;
    private readonly IObjectMapper _objectMapper;
    private readonly ActivitySource _activitySource;
    private readonly ILogger<TxRelayAppService> _logger;
    private List<SupportedCallDto> _supportedCalls;

    public TxRelayAppService(
        IClusterClient clusterClient,
        IInstrumentationProvider instrumentationProvider,
        ILogger<TxRelayAppService> logger, IObjectMapper objectMapper)
    {
        _clusterClient = clusterClient;
        _activitySource = instrumentationProvider.ActivitySource;
        _logger = logger;
        _objectMapper = objectMapper;
    }

    public async Task<List<SupportedCallDto>> GetSupportedCallsAsync()
    {
        if (_supportedCalls != null)
        {
            return _supportedCalls;
        }

        var grain = _clusterClient.GetGrain<ITxRelayerConfigurationHolder>(0);
        var result = await grain.GetSupportedCallsAsync();
        _supportedCalls = result.Select(x => new SupportedCallDto
        {
            ChainName = x.ChainName,
            ContractAddress = x.ContractAddress,
            MethodName = x.MethodName
        }).ToList();


        return _supportedCalls;
    }

    public async Task<SubmittedTxDto> SubmitTransactionAsync(TxDto tx)
    {
        var calls = await GetSupportedCallsAsync();
        if (!calls.Any(
                call => tx.ChainName == call.ChainName &&
                        tx.ContractAddress == call.ContractAddress
            )) throw new Exception("the target call is not supported");

        var jobId = tx.ToJobId();

        var txRelayJob = _clusterClient.GetGrain<IVoteRelayJob>(jobId);
        var grainDto = _objectMapper.Map<TxDto, VoteRelayDto>(tx);
        var result = await txRelayJob.SendTxAsync(grainDto);
        return new SubmittedTxDto
        {
            JobId = jobId,
            TxId = result
        };
    }
}