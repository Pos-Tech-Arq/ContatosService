using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Domain.ValueObjects;
using ContatosService.Infra.Services;

namespace ContatosService.Domain.Services;

public class AtualizaContatoService : IAtualizaContatoService
{
    private readonly IContatosRepository _contatosRepository;

    private readonly IRegiaoRepository _regiaoRepository;
    private readonly IBuscaRegiaoService _buscaRegiaoService;

    public AtualizaContatoService(IContatosRepository contatosRepository, IRegiaoRepository regiaoRepository, IBuscaRegiaoService buscaRegiaoService)
    {
        _contatosRepository = contatosRepository;
        _regiaoRepository = regiaoRepository;
        _buscaRegiaoService = buscaRegiaoService;
    }

    public async Task Handle(AtualizaContatoCommand command)
    {
        var telefone = new Telefone(command.Ddd, command.Numero);
        var contato = new Contato(command.Id, command.Nome, command.Email, telefone);
        await contato.AdicionaRegiao(_regiaoRepository, _buscaRegiaoService);

        await _contatosRepository.Update(contato);
    }
}