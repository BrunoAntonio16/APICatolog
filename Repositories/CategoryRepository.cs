using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatalog.Context;
using APICatalog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace APICatolog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public IEnumerable<Category> GetProductsCategory()
        {
            return _context.Categories.Include(p => p.Products).AsNoTracking().ToList();
        }

        public Category Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }

        public Category Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return category;
        }

        public Category Delete(int id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return category;
        }
    }
}