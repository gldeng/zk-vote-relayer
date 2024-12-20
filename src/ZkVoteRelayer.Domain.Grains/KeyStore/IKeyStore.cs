namespace ZkVoteRelayer.Domain.Grains.KeyStore;

public interface IKeyStore
{
    byte[] GetPrivateKey();
}