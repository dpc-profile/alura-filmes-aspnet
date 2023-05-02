using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Filme
{
    //O migration usa essa classe como base para criar as tabela
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