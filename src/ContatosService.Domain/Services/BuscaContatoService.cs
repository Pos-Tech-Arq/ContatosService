using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;

namespace ContatosService.Domain.Services;

public class BuscaContatoService : IBuscaContatosService
{
    private readonly IContatosRepository _contatosRepository;
    private readonly IRegiaoRepository _regiaoRepository;

    public BuscaContatoService(IContatosRepository contatosRepository, IRegiaoRepository regiaoRepository)
    {
        _contatosRepository = contatosRepository;
        _regiaoRepository = regiaoRepository;
    }

    public IEnumerable<Contato> Handle()
    {
        IEnumerable<Contato> contatos = _contatosRepository.GetAll();
        contatos.All(c => { c.AdicionaRegiao(_regiaoRepository); return true; });
        return contatos;
    }
}