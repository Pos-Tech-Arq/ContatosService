using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Domain.ValueObjects;

namespace ContatosService.Domain.Services;

public class BuscaContatoService : IBuscaContatosService
{
    private readonly IContatosRepository _contatosRepository;

    public BuscaContatoService(IContatosRepository contatosRepository)
    {
        _contatosRepository = contatosRepository;
    }

    public IEnumerable<Contato> Handle()
    {
        return _contatosRepository.GetAll();
    }
}