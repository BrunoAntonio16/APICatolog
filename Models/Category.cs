using APICatalog.Validations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalog.Models;
//Classe POCO

//Esse atributo Table dos Data Annotations não seria necessário, pois na classe AppDbContext as propiedades DbSet já fazem o mapeamento das classes que foram informadas para as mesmas serem uma tabela no banco de dados.
[Table("Categories")]
public class Category
{
    public Category()
    {
        Products = new Collection<Product>();
    }
    //O atributo Key dos Data Annotations define que o CategoryId vai ser uma coluna do tipo Chave primaria no banco de dados.
    //Porém ele também não seria necessário pois o EF Core entende que quando um atributo de uma classe contem o nome ID presente em sua string ele já vai ser uma chave primaria no banco.
    [Key]
    public int CategoryId { get; set; }
    //O atributo Required define que o atributo Name vai ser do tipo NOT NULL no banco de dados, algo que não pode ser vazio.
    //O atributo StringLength vai definir o tamanho da String/Texto que a coluna Name no banco dados vai receber de valor.
    [Required(ErrorMessage = "O nome da categoria é obrigatorio!!!")]
    [StringLength(12, MinimumLength = 5, ErrorMessage = "O nome da categoria só consegue armazenar textos que tenham entre 5 e 12 caracteres")]
    [FirstLetterCapitalized]
    public string? Name { get; set; }
    //O atributo Required define que o atributo UrlImage vai ser do tipo NOT NULL no banco de dados, algo que não pode ser vazio.
    //O atributo StringLength vai definir o tamanho da String/Texto que a coluna UrlImage no banco dados vai receber de valor.
    [Required]
    [StringLength(300)]
    [Url(ErrorMessage = "A url informada não está dentro dos padrões estabelecidos")]
    public string? UrlImage { get; set; }

    public ICollection<Product>? Products { get; set; }

}
