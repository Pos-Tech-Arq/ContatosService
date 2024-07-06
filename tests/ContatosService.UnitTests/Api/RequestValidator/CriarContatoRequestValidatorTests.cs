using ContatosService.Api.Requests;
using ContatosService.Api.Requests.Validators;
using FluentValidation.TestHelper;

namespace ContatosService.UnitTests.Api.RequestValidator
{
    public class CriarContatoRequestValidatorTests
    {
        private readonly CriarContatoRequestValidator _validator;

        public CriarContatoRequestValidatorTests()
        {
            _validator = new CriarContatoRequestValidator();
        }

        [Fact]
        public void Deve_Falhar_Quando_Email_Esta_Vazio()
        {
            var request = new CriaContatoRequest { Email = "" };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Email);
        }

        [Fact]
        public void Deve_Falhar_Quando_Email_Eh_Invalido()
        {
            var request = new CriaContatoRequest { Email = "email_invalido" };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Email);
        }

        [Fact]
        public void Deve_Falhar_Quando_Nome_Esta_Vazio()
        {
            var request = new CriaContatoRequest { Nome = "" };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Nome);
        }

        [Fact]
        public void Deve_Passar_Quando_Todos_Os_Campos_Sao_Validos()
        {
            var request = new CriaContatoRequest
            {
                Email = "email@valido.com",
                Nome = "Nome Valido",
                Telefone = new TelefoneRequest { Ddd = "12", Numero = "12345678" }
            };

            var result = _validator.TestValidate(request);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Deve_Falhar_Quando_Telefone_Ddd_Nao_Tem_2_Caracteres()
        {
            var request = new CriaContatoRequest { Telefone = new TelefoneRequest { Ddd = "1" } };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Telefone.Ddd);
        }

        [Fact]
        public void Deve_Falhar_Quando_Telefone_Ddd_Nao_Eh_Numero()
        {
            var request = new CriaContatoRequest { Telefone = new TelefoneRequest { Ddd = "AB" } };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Telefone.Ddd);
        }

        [Fact]
        public void Deve_Falhar_Quando_Telefone_Numero_Nao_Tem_8_Ou_9_Caracteres()
        {
            var request = new CriaContatoRequest { Telefone = new TelefoneRequest { Numero = "1234567" } };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Telefone.Numero);
        }

        [Fact]
        public void Deve_Falhar_Quando_Telefone_Numero_Nao_Eh_Numero()
        {
            var request = new CriaContatoRequest { Telefone = new TelefoneRequest { Numero = "1234ABCD" } };
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Telefone.Numero);
        }
    }
}