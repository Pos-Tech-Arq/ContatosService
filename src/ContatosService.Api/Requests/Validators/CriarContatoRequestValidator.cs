using FluentValidation;

namespace ContatosService.Api.Requests.Validators;

public class CriarContatoRequestValidator : AbstractValidator<CriaContatoRequest>
{
    public CriarContatoRequestValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório.")
            .EmailAddress()
            .WithMessage("Email tem que ser válido.");

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.")
            .Length(1, 100)
            .WithMessage("O nome deve conter de 1 a 100 caracteres.");

        RuleFor(c => c.Telefone)
            .NotEmpty()
            .WithMessage("Telefone é obrigatório.");

        RuleFor(c => c.Telefone.Ddd)
            .NotEmpty()
            .WithMessage("Ddd é obrigatório.")
            .Length(2)
            .WithMessage("O Ddd deve conter 2 caracteres.")
            .Matches(@"^\d+$")
            .WithMessage("O campo Ddd deve conter apenas números.");

        RuleFor(c => c.Telefone.Numero)
            .NotEmpty()
            .WithMessage("Numero é obrigatório.")
            .Length(8, 9)
            .WithMessage("O nome deve conter de 8 a 9 caracteres.")
            .Matches(@"^\d+$")
            .WithMessage("O campo Numero deve conter apenas números.");;
    }
}