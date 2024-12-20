using AElf;
using AElf.Client.Dto;
using AElf.Types;
using Google.Protobuf;
using Orleans.Runtime;
using TomorrowDAO.Contracts.Vote;
using ZkVoteRelayer.Domain.Grains.Chain;

namespace ZkVoteRelayer.Domain.Grains.TxRelay;

[GenerateSerializer]
public class TxRelayJobState
{
    public ByteString TransactionBytes { get; set; }
    public string TransactionId { get; set; }
    public TransactionResultDto TransactionResult { get; set; }
}

public class VoteRelayJobGrain : Grain<TxRelayJobState>, IVoteRelayJob
{
    private readonly IPersistentState<TxRelayJobState> _state;
    private readonly IAElfClientFactory _clientFactory;
    private readonly IContractStubFactory _contractStubFactory;

    public VoteRelayJobGrain(
        [PersistentState("TxRelayJobState", "Default")]
        IPersistentState<TxRelayJobState> state,
        IAElfClientFactory clientFactory,
        IContractStubFactory contractStubFactory
    )
    {
        _state = state;
        _clientFactory = clientFactory;
        _contractStubFactory = contractStubFactory;
    }


    public async Task<TransactionResultDto> SendTxAsync(VoteRelayDto request)
    {
        try
        {
            var contractInstance = _contractStubFactory.GetInstance<VoteContractContainer.VoteContractStub>(
                request.ChainName, request.ContractAddress
            );
            var input = new VoteInput
            {
                VotingItemId = Hash.LoadFromHex(request.VoteDetails.VotingItemId),
                VoteOption = request.VoteDetails.VoteOption,
                VoteAmount = request.VoteDetails.VoteAmount,
                Memo = "",
                AnonymousVoteExtraInfo = new VoteInput.Types.AnonymousVoteExtraInfo
                {
                    Nullifier = Hash.LoadFromHex(request.VoteDetails.NullifierHash),
                    Proof = new VoteInput.Types.Proof()
                    {
                        A = new VoteInput.Types.G1Point()
                        {
                            X = request.VoteDetails.Proof.PiA[0],
                            Y = request.VoteDetails.Proof.PiA[1]
                        },
                        B = new VoteInput.Types.G2Point()
                        {
                            X = new VoteInput.Types.Fp2()
                            {
                                First = request.VoteDetails.Proof.PiB[0][1],
                                Second = request.VoteDetails.Proof.PiB[0][0]
                            },
                            Y = new VoteInput.Types.Fp2()
                            {
                                First = request.VoteDetails.Proof.PiB[1][1],
                                Second = request.VoteDetails.Proof.PiB[1][0]
                            },
                        },
                        C = new VoteInput.Types.G1Point()
                        {
                            X = request.VoteDetails.Proof.PiC[0],
                            Y = request.VoteDetails.Proof.PiC[1]
                        },
                    }
                }
            };

            var tx = contractInstance.Vote.GetTransaction(input);
            _state.State.TransactionBytes = tx.ToByteString();

            var client = _clientFactory.GetClient(request.ChainName);
            var sendResult = await client.SendTransactionAsync(new SendTransactionInput()
            {
                RawTransaction = tx.ToByteArray().ToHex()
            });
            _state.State.TransactionId = sendResult.TransactionId;

            var transactionResult = TransactionResultDto.FromClientDto(
                await client.WaitForTransactionCompletionAsync(Hash.LoadFromHex(sendResult.TransactionId))
            );

            _state.State.TransactionResult = transactionResult;
            return transactionResult;
        }
        finally
        {
            await _state.WriteStateAsync();
        }
    }

    public async Task<TransactionResultDto> GetTransactionResultAsync()
    {
        return await Task.FromResult(_state.State.TransactionResult);
    }
}