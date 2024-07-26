using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatalog.Models;
using APICatalog.Context;

namespace APICatolog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetProducts()
        {
        }

        public Product GetProduct(int id)
        {
        }

        public Product Create(Product product)
        {
        }

        public bool Update(Product product)
        {
        }

        public bool Delete(int id)
        {
        }

    }
}