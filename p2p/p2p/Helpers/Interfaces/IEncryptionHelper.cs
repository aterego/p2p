namespace p2p.Helpers
{
    public interface IEncryptionHelper
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
    }
}
