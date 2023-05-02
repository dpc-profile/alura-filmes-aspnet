using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

//É o que torna possivel fazer o map da classe
// CreateFilmeDto para Filme na func AdicionaFilme 
public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
    }
}