using Microsoft.AspNetCore.Mvc;
using AppAzure.Context;
using AppAzure.Models;


namespace AppAzure.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly RHContext _context;

        public FuncionarioController(RHContext context)
        {

            _context = context;
        }
        public IActionResult Index()
        {
            var funcionarios = _context.Funcionarios.ToList();


            return View(funcionarios);
        }

        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Criar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Funcionarios.Add(funcionario);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(funcionario);
        }

        public IActionResult Detalhes(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);

            if (funcionario == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(funcionario);
        }


        public IActionResult Editar(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);

            if (funcionario == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(funcionario);
        }

        [HttpPost]
        public IActionResult Editar(Funcionario funcionario)
        {
            var funcionarioBanco = _context.Funcionarios.Find(funcionario.Id);

            funcionarioBanco.Nome = funcionario.Nome;
            funcionarioBanco.Endereco = funcionario.Endereco;
            funcionarioBanco.Ramal = funcionario.Ramal;
            funcionarioBanco.EmailProfissional = funcionario.EmailProfissional;
            funcionarioBanco.Departamento = funcionario.Departamento;
            funcionarioBanco.Salario = funcionario.Salario;
            funcionarioBanco.DataAdmissao = funcionario.DataAdmissao;


            _context.Funcionarios.Update(funcionarioBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            var contato = _context.Funcionarios.Find(id);

            if (contato == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(contato);

        }

        [HttpPost]
        public IActionResult Excluir(Funcionario funcionario)
        {
            var funcionarioBanco = _context.Funcionarios.Find(funcionario.Id);

            _context.Funcionarios.Remove(funcionarioBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }


        public IActionResult PesquisaData(DateTime data, DateTime data1)
        {

            var tarefas = _context.Funcionarios.Where(x => x.DataAdmissao >= data && x.DataAdmissao <= data1).ToList();

            return View(tarefas);
        }


        public IActionResult PesquisaNome(string nome)
        {

            var tarefas = _context.Funcionarios.Where(x => x.Nome.Contains(nome)).ToList();

            return View(tarefas);
        }

        public IActionResult PesquisaDepartamento(string departamento)
        {

            var tarefas = _context.Funcionarios.Where(x => x.Departamento.Contains(departamento)).ToList();

            return View(tarefas);
        }


    }
}
