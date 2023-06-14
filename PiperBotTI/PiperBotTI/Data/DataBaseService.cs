using Microsoft.EntityFrameworkCore;
using PiperBotTI.Contracts;
using System.Threading.Tasks;

namespace PiperBotTI.Data
{
    public class DataBaseService : DbContext , IDataBaseService
    {
        public DataBaseService(DbContextOptions options): base(options)
        {
            Database.EnsureCreatedAsync();  
        }
        public DataBaseService()
        {
            Database.EnsureCreatedAsync();
        }

        public async Task<bool>SaveAsync()
        {
            return (await SaveChangesAsync() > 0);
        }

    }
}
