using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            try
            {
                //A utilização do AsNoTracking, se faz presente para melhorar o desempenho da sua API, pois evita o rastreamento desnecessário de uma Entidade, evitando a sobrecarga de dados na sua aplicação
                var categories = await _context.Categories.AsNoTracking().ToListAsync();
                //A utilização do TAKE serve para limitar a quantidade de dados que vão ser mostradas, visando melhorar o desenpenho.
                //var categories = _context.Categories.Take(2).AsNoTracking().ToList();

                if (categories is null)
                {
                    return NotFound("Não existe nenhuma categoria cadastrada no momento...");
                }

                return categories;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro no servidor ao tentar retornar os dados da pesquisa...\nPor gentileza entre em contato com o nosso suporte!!!");
            }
            
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category is null)
            {
                return NotFound("A categoria informada não existe em nosso sistema...");
            }

            return category;
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Category>>> GetProductsCategory()
        {
            var productsCategory = await _context.Categories.Include(p => p.Products).AsNoTracking().ToListAsync();
            //Abaixo está sendo criado um filtro para, fazer a filtragem dos dados relacionais a categoria visando também a otimização
            //var productsCategory = _context.Categories.Include(p => p.Products).Where(c => c.CategoryId <= 5).AsNoTracking().ToList();
            if (productsCategory is null)
            {
                return NotFound();
            }
            return productsCategory;
        }

        [HttpPost]
        public IActionResult Post(Category category)
        {
            if (category is null)
            {
                return BadRequest();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

            if(category is null)
            {
                return NotFound("A categoria informada não existe em nosso sistema...");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok(category);
        }
    }
}
