namespace TokenProvider.Logic
{
    public interface ICryptoLogic
    {
        string GetPrivateKey();

        string GetPublicKey();
    }
}