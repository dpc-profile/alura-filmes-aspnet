using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // func de retorno IActionResult por ser mais abrangente e generico
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        // Converte o filmeDTo para Filme
        Filme filme = _mapper.Map<Filme>(filmeDto);

        // Grava as informações
        _context.Filmes.Add(filme);
        _context.SaveChanges();

        // Retorna no Header a localização daquele item
        // Ex: https://localhost:7298/Filme/6
        return CreatedAtAction(nameof(ListarFilmePorId),
                               new { id = filme.Id },
                               filme);
    }

    [HttpGet]
    public IEnumerable<Filme> ListarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _context.Filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult ListarFilmePorId(int id)
    {
        // Retorna o filme, e se não achar retorna null
        var filmeEncontrado = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filmeEncontrado == null) return NotFound();

        return Ok(filmeEncontrado);
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        // Retorna o filme, e se não achar retorna null
        var filmeEncontrado = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filmeEncontrado == null) return NotFound();

        // Converte 
        var filmeParaAtualiza = _mapper.Map<UpdateFilmeDto>(filmeEncontrado);

        // Ver o modelState
        // Verifica se o filmeParaAtualiza é valido
        patch.ApplyTo(filmeParaAtualiza, ModelState);

        if (!TryValidateModel(filmeParaAtualiza)) return ValidationProblem(ModelState);

        // Grava as informações
        _mapper.Map(filmeParaAtualiza, filmeEncontrado);
        _context.SaveChanges();

        return NoContent();
    }
}