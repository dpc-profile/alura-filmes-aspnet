using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();
    private static int id = 0;

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id = id++;
        filmes.Add(filme);
        return CreatedAtAction(
                nameof(ListarFilmePorId),
                new { id = filme.Id},
                filme);
    }

    [HttpGet]
    public IEnumerable<Filme> ListarFilmes(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 20)
    {
        return filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult ListarFilmePorId(int id)
    {
        var filmeEncontrado = filmes.FirstOrDefault(filme => filme.Id == id);

        if (filmeEncontrado == null) return NotFound();
        return Ok(filmeEncontrado);
    }
}