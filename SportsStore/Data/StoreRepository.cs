using SportsStore.Interfaces;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Data
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;

        public StoreRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> Products => context.Products;
    }
}
