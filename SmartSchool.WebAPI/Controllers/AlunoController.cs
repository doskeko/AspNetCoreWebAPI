using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>() {
            new Aluno(){
                ID = 1,
                Nome = "Bebedico",
                Sobrenome = "Modicao",
                Telefone = "123456"
            },
            new Aluno(){
                ID = 2,
                Nome = "Luka",
                Sobrenome = "Gatos",
                Telefone = "123456"
            },
            new Aluno(){
                ID = 3,
                Nome = "Xiri",
                Sobrenome = "Preto",
                Telefone = "123456"
            }
        };
        public AlunoController(){}   

        public IActionResult Get()
        {
            return Ok(Alunos);
            
        }
        [HttpGet("byId")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.ID == id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");
            return Ok(aluno);
        }
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string Sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(Sobrenome));
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");
            return Ok(aluno);
        }
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }
        [HttpPut]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }
        [HttpPatch]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}