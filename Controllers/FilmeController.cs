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

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
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

    /// <summary>
    /// Retorna os primeiros 20 filmes de acordo com o intervalo
    /// </summary>
    /// <param name="skip">Qual o começo do intervalo</param>
    /// <param name="take">Quantos filmes serão retornados</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadFilmeDto> ListarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    }

    /// <summary>
    /// Retorna as informações do filme referente ao ID
    /// </summary>
    /// <param name="id">ID do filmes gerado na criação</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ListarFilmePorId(int id)
    {
        // Retorna o filme, e se não achar retorna null
        var filmeEncontrado = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filmeEncontrado == null) return NotFound();

        var filmeDto = _mapper.Map<ReadFilmeDto>(filmeEncontrado);

        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualiza uma ou mais informações do filme
    /// </summary>
    /// <param name="id">ID do filmes gerado na criação</param>
    /// <param name="patch"></param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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

    /// <summary>
    /// Deleta um filme
    /// </summary>
    /// <param name="id">ID do filmes gerado na criação</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a remoção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletarFilme(int id)
    {
        // Retorna o filme, e se não achar retorna null
        var filmeEncontrado = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filmeEncontrado == null) return NotFound();

        _context.Remove(filmeEncontrado);
        _context.SaveChanges();

        return NoContent();
    }
}