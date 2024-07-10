using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _context.Products.AsNoTracking().ToList();
            if (products is null)
            {
                return NotFound("Não existe nenhum produto cadastrado!!!");
            }
            return products;
        }

        //Uma rota nomeada
        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.AsNoTracking().FirstOrDefault(product => product.ProductId == id);
            if (product == null)
            {
                return NotFound("O produto informado não existe em nosso catalogo!!!");
            }
            return product;
        }

        //O metodo post recebe as informações no corpo da requisição
        [HttpPost]
        public IActionResult Post(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            //Cria o produto na memoria de maneira local, somente existente na aplicação
            _context.Products.Add(product);
            //Cria o produto no banco de dados, pegando as informações que foram salvas na memoria de maneira local
            _context.SaveChanges();

            //Retornando a Classe CreateAtRouteResult que vai retornar um status HTTP 201 que significa que o objeto foi criado e esse objeto que foi criado
            //Vai ser utilizado para criar uma variavel do tipo ID que vai pegar a informação do ID do objeto que foi criado e jogar para a rota que foi especificada
            //O valor que vai ser passado vai depender do valor que vai está sendo pedido pela rota
            return new CreatedAtRouteResult("ObterProduto", new { id = product.ProductId }, product);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if(product == null)
            {
                return NotFound("O produto informado não existe em nosso catalogo!!!");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
