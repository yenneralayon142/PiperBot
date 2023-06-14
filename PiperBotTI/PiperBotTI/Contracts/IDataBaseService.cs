using System.Threading.Tasks;

namespace PiperBotTI.Contracts
{
    public interface IDataBaseService
    {
        Task<bool> SaveAsync();

    }
}
