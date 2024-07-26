using APICatalog.Context;
using APICatalog.Filters;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICatolog.Repositories;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                //A utilização do AsNoTracking, se faz presente para melhorar o desempenho da sua API, pois evita o rastreamento desnecessário de uma Entidade, evitando a sobrecarga de dados na sua aplicação
                var categories = _repository.GetCategories(); 
                //A utilização do TAKE serve para limitar a quantidade de dados que vão ser mostradas, visando melhorar o desenpenho.
                //var categories = _context.Categories.Take(2).AsNoTracking().ToList();

                if (categories is null)
                {
                    return NotFound("Não existe nenhuma categoria cadastrada no momento...");
                }

                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor ao tentar retornar os dados da pesquisa...\nPor gentileza entre em contato com o nosso suporte!!!");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> Get(int id)
        {
            var category = _repository.GetCategory(id);

            if (category is null)
            {
                return NotFound("A categoria informada não existe em nosso sistema...");
            }

            return Ok(category);
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Category>> GetProductsCategory()
        {
            var productsCategory = _repository.GetProductsCategory();
            //Abaixo está sendo criado um filtro para, fazer a filtragem dos dados relacionais a categoria visando também a otimização
            //var productsCategory = _context.Categories.Include(p => p.Products).Where(c => c.CategoryId <= 5).AsNoTracking().ToList();
            if (productsCategory is null)
            {
                return NotFound();
            }
            // await _context.Categories.Include(p => p.Products).AsNoTracking().ToListAsync();
            return Ok(productsCategory);
        }

        [HttpPost]
        public IActionResult Post(Category category)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            var categoryCreated = _repository.Create(category);

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoryCreated.CategoryId }, categoryCreated);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _repository.Update(category);

            return Ok(category);
        }

        [HttpDelete]
        public ActionResult<Category> Delete(int id)
        {
            var category = _repository.GetCategory(id);

             if(category is null)
            {
                return NotFound("A categoria informada não existe em nosso sistema...");
            }

            var categoryExclude = _repository.Delete(id);

            return Ok(categoryExclude);
        }
    }
}
