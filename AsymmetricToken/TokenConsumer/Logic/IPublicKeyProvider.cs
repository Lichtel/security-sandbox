using System.Threading.Tasks;

namespace TokenConsumer.Logic
{
    public interface IPublicKeyProvider
    {
        Task<string> GetPublicKey();
    }
}