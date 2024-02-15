
using CrudUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CrudUser.ViewModels
{
    public class PessoaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public string DataNascimentoFormatada
        {
            get
            {
                return string.Format("{0:dd/MM/yyyy}", DataNascimento);
            }
        }

        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string CPF { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        public void Validar([FromServices] Conexao db)
        {
            if (string.IsNullOrEmpty(Nome))
                throw new ApplicationException("O campo Nome é obrigatório");

            if (Nome.Length > 200)
                throw new ApplicationException("O campo Nome só pode conter 200 caracteres");

            if (DataNascimento == null)
                throw new ApplicationException("O campo nascimento é obrigatório");

            if (DataNascimento > DateTime.Now.Date)
                throw new ApplicationException(string.Format("O campo DataNascimento não pode " + "ser maior que a data de hoje {0:dd/MM/yyyy}", DateTime.Now.Date));

            if (DataNascimento < new DateTime(1900, 1, 1))
                throw new ApplicationException(string.Format("O campo DataNascimento não pode " + "ser menor que {0:dd/MM/yyyy}", new DateTime(1900, 1, 1)));

            if (string.IsNullOrWhiteSpace(Sexo))
                throw new ApplicationException("O campo Sexo é obrigatório");

            if (Sexo.Length > 1)
                throw new ApplicationException("O campo Sexo só pode conter 1 caracter");

            if (string.IsNullOrWhiteSpace(EstadoCivil))
                throw new ApplicationException("O campo Estado Civil é obrigatório");

            if (EstadoCivil.Length > 20)
                throw new ApplicationException("O campo EstadoCivil só pode conter 20 caracter");

            if (string.IsNullOrWhiteSpace(CPF))
                throw new ApplicationException("O campo CPF é obrigatório");

            if (CPF.Length != 14)
                throw new ApplicationException("O campo CPF deve conter 11 caracteres");

            if (!ValidarCPF(CPF))
                throw new ApplicationException("CPF inválido");

            if (db.Pessoas.Any(c => c.CPF == CPF))
                throw new ApplicationException("CPF já cadastrado");

            if (string.IsNullOrWhiteSpace(CEP))
                throw new ApplicationException("O campo CEP é obrigatório");

            if (CEP.Length != 8)
                throw new ApplicationException("O campo CEP deve conter 8 caracteres");

            if (string.IsNullOrWhiteSpace(Endereco))
                throw new ApplicationException("O campo Endereço é obrigatório");

            if (Endereco.Length > 100)
                throw new ApplicationException("O campo Endereço só pode conter 100 caracteres");

            if (string.IsNullOrWhiteSpace(Numero))
                throw new ApplicationException("O campo Numero é obrigatório");

            if (Numero.Length > 10)
                throw new ApplicationException("O campo Numero só pode conter 10 caracteres");

            if (!string.IsNullOrWhiteSpace(Complemento))
            {
                if (Complemento.Length > 30)
                    throw new ApplicationException("O campo complemento só pode conter 30 caracteres");
            }

            if (string.IsNullOrWhiteSpace(Bairro))
                throw new ApplicationException("O campo Bairro é obrigatório");

            if (Bairro.Length > 50)
                throw new ApplicationException("O campo bairro só pode conter 50 caracteres");

            if (string.IsNullOrWhiteSpace(UF))
                throw new ApplicationException("O campo UF é obrigatório");

            if (UF.Length > 2)
                throw new ApplicationException("O campo UF só pode conter 2 caracteres");
        }

        public void Validar2 ()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new ApplicationException("O campo Nome é obrigatório");

            if (Nome.Length > 200)
                throw new ApplicationException("O campo Nome só pode conter 200 caracteres");

            if (DataNascimento == null)
                throw new ApplicationException("O campo nascimento é obrigatório");

            if (DataNascimento > DateTime.Now.Date)
                throw new ApplicationException(string.Format("O campo DataNascimento não pode " + "ser maior que a data de hoje {0:dd/MM/yyyy}", DateTime.Now.Date));

            if (DataNascimento < new DateTime(1900, 1, 1))
                throw new ApplicationException(string.Format("O campo DataNascimento não pode " + "ser menor que {0:dd/MM/yyyy}", new DateTime(1900, 1, 1)));

            if (string.IsNullOrWhiteSpace(Sexo))
                throw new ApplicationException("O campo Sexo é obrigatório");

            if (Sexo.Length > 1)
                throw new ApplicationException("O campo Sexo só pode conter 1 caracter");

            if (string.IsNullOrWhiteSpace(EstadoCivil))
                throw new ApplicationException("O campo Estado Civil é obrigatório");

            if (EstadoCivil.Length > 20)
                throw new ApplicationException("O campo EstadoCivil só pode conter 20 caracter");

            if (string.IsNullOrWhiteSpace(CPF))
                throw new ApplicationException("O campo CPF é obrigatório");

            if (CPF.Length != 14)
                throw new ApplicationException("O campo CPF deve conter 11 caracteres");

            if (!ValidarCPF(CPF))
                throw new ApplicationException("CPF inválido");

            if (string.IsNullOrWhiteSpace(CEP))
                throw new ApplicationException("O campo CEP é obrigatório");

            if (CEP.Length != 8)
                throw new ApplicationException("O campo CEP deve conter 8 caracteres");

            if (string.IsNullOrWhiteSpace(Endereco))
                throw new ApplicationException("O campo Endereço é obrigatório");

            if (Endereco.Length > 100)
                throw new ApplicationException("O campo Endereço só pode conter 100 caracteres");

            if (string.IsNullOrWhiteSpace(Numero))
                throw new ApplicationException("O campo Numero é obrigatório");

            if (Numero.Length > 10)
                throw new ApplicationException("O campo Numero só pode conter 10 caracteres");

            if (!string.IsNullOrWhiteSpace(Complemento))
            {
                if (Complemento.Length > 30)
                    throw new ApplicationException("O campo complemento só pode conter 30 caracteres");
            }

            if (string.IsNullOrWhiteSpace(Bairro))
                throw new ApplicationException("O campo Bairro é obrigatório");

            if (Bairro.Length > 50)
                throw new ApplicationException("O campo bairro só pode conter 50 caracteres");

            if (string.IsNullOrWhiteSpace(UF))
                throw new ApplicationException("O campo UF é obrigatório");

            if (UF.Length > 2)
                throw new ApplicationException("O campo UF só pode conter 2 caracteres");
        }


        public void TratarDados()
        {
            Nome = Nome.ToUpper().Trim();
            CEP = Regex.Replace(CEP, "[^0-9]", string.Empty);
            Endereco = Endereco.ToUpper().Trim();
            Numero = Numero.ToUpper().Trim();
            Bairro = Bairro.ToUpper().Trim();
            Cidade = Cidade.ToUpper().Trim();
        }


        public static bool ValidarCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
