using FilmesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data;

// Classe responsavel por fornecer o contexto para a criação
// dinamica das tabelas do database
public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> options) : base(options){}

    public DbSet<Filme> Filmes { get; set; }

}