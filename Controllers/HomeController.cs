using CrudOfUser.Models;
using CrudUser.Models;
using CrudUser.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudUser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public ActionResult Index([FromServices] Conexao db2)
        {
            List<Pessoas> pessoasModels = db2.Pessoas.ToList();
            List<PessoaViewModel> pessoasVms = new List<PessoaViewModel>();
            foreach (Pessoas item in pessoasModels)
            {
                PessoaViewModel pessoaVm = new PessoaViewModel();
                pessoaVm.Id = item.Id;
                pessoaVm.Nome = item.Nome;
                pessoaVm.DataNascimento = item.DataNascimento;
                pessoaVm.Sexo = item.Sexo;
                pessoaVm.EstadoCivil = item.EstadoCivil;
                pessoaVm.CPF = item.CPF;
                pessoaVm.CEP = item.CEP;
                pessoaVm.Endereco = item.Endereco;
                pessoaVm.Numero = item.Numero;
                pessoaVm.Complemento = item.Complemento;
                pessoaVm.Bairro = item.Bairro;
                pessoaVm.Cidade = item.Cidade;
                pessoaVm.UF = item.UF;

                pessoasVms.Add(pessoaVm);
            }

            return View(pessoasVms);
        }

        public IActionResult EditarUsuario()
        {
            return View();
        }


        public IActionResult Cadastrar()
        {
            if (TempData["mensagemSucesso"] != null)
            {
                ViewBag.mensagemSucesso = TempData["mensagemSucesso"];
            }

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarPost(PessoaViewModel dados, [FromServices] Conexao db)
        {
            dados.TratarDados();
            dados.Validar(db);
            Pessoas model = new Pessoas();
            model.Nome = dados.Nome;
            model.DataNascimento = dados.DataNascimento;
            model.Sexo = dados.Sexo;
            model.EstadoCivil = dados.EstadoCivil;
            model.CPF = dados.CPF;
            model.CEP = dados.CEP;
            model.Endereco = dados.Endereco;
            model.Numero = dados.Numero;
            model.Complemento = dados.Complemento;
            model.Cidade = dados.Cidade;
            model.UF = dados.UF;
            model.Bairro = dados.Bairro;

            db.Pessoas.Add(model);
            db.SaveChanges();

            TempData["mensagemSucesso"] = "Pessoa cadastrada com sucesso";

            return RedirectToAction("Cadastrar");
        }


        [HttpPost]
        [Route("excluir", Name = "excluirPost")]
        public ActionResult Excluir(int id, [FromServices] Conexao db3)
        {
            Pessoas model = db3.Pessoas.First(c => c.Id == id);
            db3.Pessoas.Remove(model);
            db3.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Editar(PessoaViewModel dados, [FromServices] Conexao db4)
        {

                Pessoas model2 = db4.Pessoas.First(c => c.Id == dados.Id);


                string cpfOriginal = model2.CPF;
                dados.TratarDados();
 
                dados.Validar2();


                if (dados.CPF != cpfOriginal)
                {
                    
                     return RedirectToAction("Error");
                }


                model2.Nome = dados.Nome;
                model2.DataNascimento = dados.DataNascimento;
                model2.Sexo = dados.Sexo;
                model2.EstadoCivil = dados.EstadoCivil;
                model2.CPF = dados.CPF;
                model2.CEP = dados.CEP;
                model2.Endereco = dados.Endereco;
                model2.Numero = dados.Numero;
                model2.Complemento = dados.Complemento;
                model2.Cidade = dados.Cidade;
                model2.UF = dados.UF;
                model2.Bairro = dados.Bairro;


                db4.Pessoas.Update(model2);
                db4.SaveChanges();

                return RedirectToAction("Index");



        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




