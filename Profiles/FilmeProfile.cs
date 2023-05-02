using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

// É o que torna possivel fazer o map da classe
// CreateFilmeDto para Filme na func AdicionaFilme 
public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>(); // Usado pelo create
        CreateMap<UpdateFilmeDto, Filme>(); // Tmb usado pelo patch, e pelo PUT(que não existe)
        CreateMap<Filme, UpdateFilmeDto>(); // Usado pelo patch
    }
}