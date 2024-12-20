using Microsoft.Extensions.Options;
using Nethereum.KeyStore;

namespace ZkVoteRelayer.Domain.Grains.KeyStore;

public class KeyStore : KeyStoreService, IKeyStore, IDisposable
{
    private byte[] _privateKey;

    public KeyStore(IOptionsSnapshot<KeyStoreOptions> options)
    {
        _privateKey = DecryptKeyStoreFromFile(options.Value.Password, options.Value.Path);
    }

    private byte[] DecryptKeyStoreFromFile(string password, string filePath)
    {
        using var file = File.OpenText(filePath);
        var json = file.ReadToEnd();
        return DecryptKeyStoreFromJson(password, json);
    }

    public byte[] GetPrivateKey()
    {
        return _privateKey;
    }


    public void Dispose()
    {
        // Wipe out memory trace
        for (var i = _privateKey.Length - 1; i >= 0; i--)
        {
            _privateKey[i] = 0;
        }
    }
}