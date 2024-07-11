using APICatalog.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalog.Models;
//Classe POCO

//Esse atributo Table dos Data Annotations não seria necessário, pois na classe AppDbContext as propiedades DbSet já fazem o mapeamento das classes que foram informadas para as mesmas serem uma tabela no banco de dados.
[Table("Products")]
public class Product : IValidatableObject
{
    //O atributo Key dos Data Annotations define que o ProductId vai ser uma coluna do tipo Chave primaria no banco de dados.
    //Porém ele também não seria necessário pois o EF Core entende que quando um atributo de uma classe contem o nome ID presente em sua string ele já vai ser uma chave primaria no banco.
    [Key]
    public int ProductId { get; set; }
    //O atributo Required define que o atributo Name vai ser do tipo NOT NULL no banco de dados, algo que não pode ser vazio.
    //O atributo StringLength vai definir o tamanho da String/Texto que a coluna Name no banco dados vai receber de valor.
    [Required]
    [StringLength(80)]
    [FirstLetterCapitalized]
    public string? Name { get; set; }
    //O atributo Required define que o atributo Description vai ser do tipo NOT NULL no banco de dados, algo que não pode ser vazio.
    //O atributo StringLength vai definir o tamanho da String/Texto que a coluna Description no banco dados vai receber de valor.
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
    //O atributo Required define que o atributo Price vai ser do tipo NOT NULL no banco de dados, algo que não pode ser vazio.
    //O atributo Column define que esse atributo da classe vai ser uma coluna contendo as informações passadas via parametro do Column
    [Required]
    [Column(TypeName ="decimal(10,2)")]
    public decimal Price { get; set; }
    //O atributo Required define que o atributo UrlImage vai ser do tipo NOT NULL no banco de dados, algo que não pode ser vazio.
    //O atributo StringLength vai definir o tamanho da String/Texto que a coluna Description no banco dados vai receber de valor.
    [Required]
    [StringLength(300)]
    public string? UrlImage { get; set; }
    public float Stock { get; set; }
    public DateTime DataRegister { get; set; }
    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Name))
        {
            var firstLetter = this.Name[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                yield return new ValidationResult("A primeira letra do produto deve ser maiúscula", new[] { nameof(this.Name) });
            }
        }

        if (this.Stock <= 0)
        {
            yield return new ValidationResult("O estoque deve ser maior que zero", new[] { nameof(this.Stock) });
        }
    }
}
