using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;

    public FilmeController(FilmeContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ListarFilmePorId),
                               new { id = filme.Id },
                               filme);
    }

    [HttpGet]
    public IEnumerable<Filme> ListarFilmes(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20)
    {
        return _context.Filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult ListarFilmePorId(int id)
    {
        var filmeEncontrado = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filmeEncontrado == null) return NotFound();
        return Ok(filmeEncontrado);
    }
}