using System;
using System.IO;
using System.Security.Cryptography;

namespace TokenProvider.Logic
{
    public class CryptoLogic : ICryptoLogic
    {
        private const string PublicKeyFile = "Keys/rsa-key";
        
        private string PrivateKey { get; }
        private string PublicKey { get; }
        
        public CryptoLogic()
        {
            var privateKeyBytes = ReadPrivateKeyFromFile();
            PrivateKey = Convert.ToBase64String(privateKeyBytes);
            
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

            var publicKeyBytes = rsa.ExportRSAPublicKey();
            PublicKey = Convert.ToBase64String(publicKeyBytes);
        }
        
        public string GetPrivateKey()
        {
            return PrivateKey;
        }

        public string GetPublicKey()
        {
            return PublicKey;
        }

        private static byte[] ReadPrivateKeyFromFile()
        {
            var privateKeyContent = File.ReadAllText(PublicKeyFile);
            
            var privateKeyBytes = Convert.FromBase64String(privateKeyContent);

            return privateKeyBytes;
        }
    }
}