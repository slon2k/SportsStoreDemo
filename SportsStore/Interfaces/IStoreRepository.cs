using SportsStore.Models;
using System.Linq;


namespace SportsStore.Interfaces
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}
