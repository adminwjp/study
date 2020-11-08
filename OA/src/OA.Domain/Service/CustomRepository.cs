using Utility.Domain.Repositories;

namespace Utility.Database.Service
{
    public class CustomRepository<T> : BaseRepository<T>, IRepository<T> where T : class
    {
      
    }
}
