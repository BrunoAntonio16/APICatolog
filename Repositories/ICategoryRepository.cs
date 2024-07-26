using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatalog.Models;

namespace APICatolog.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();

        Category GetCategory(int id);

        IEnumerable<Category> GetProductsCategory();

        Category Create(Category category);

        Category Update(Category category);

        Category Delete(int id);
    }
}