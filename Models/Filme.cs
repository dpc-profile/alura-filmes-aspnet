using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Filme
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Titulo { get; set; }

    [Required]
    public string Genero { get; set; }

    [Required]
    [Range(1, 360)]
    public int Duracao { get; set; }
    
}