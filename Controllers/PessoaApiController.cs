using CrudUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CrudUser.Controllers
{
    [Route("api/pessoa")]
    public class PessoaApiController : ControllerBase
    {
        [Route("verificar-cpf-ja-cadastrado")]
        [HttpGet]
        public IActionResult VerificarCpfJaCadastrado(string cpf, [FromServices] Conexao db)
        {
            bool existeCpf = db.Pessoas.Any(c => c.CPF == cpf);
            return Ok(new { resultado = existeCpf });
        }
    }
}
