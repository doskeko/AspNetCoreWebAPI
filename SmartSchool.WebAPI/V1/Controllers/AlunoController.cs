using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <sumary>
    ///
    /// </sumary>
    /// <returns></returns>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        public readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }


        /// <summary>
        /// Retorna alunos pela disciplina
        /// </summary>
        /// <returns></returns>
        [HttpGet("ByDisciplina/{id}")]
        public async Task<IActionResult> GetByDisciplinaId(int id)
        {
            var result = await _repo.GetAllAlunosByDisciplinaIdAsync(id, false);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
          
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");
           
            var alunoDto = _mapper.Map<AlunoRegistrarDto>(aluno);
          
            return Ok(alunoDto);
        }
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
           
            _repo.Add(aluno);
           
            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            return BadRequest("Aluno Não Cadastrado!");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);
           
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");
           
            _mapper.Map(model, aluno);
          
            _repo.Update(aluno);
         
            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
           
            return BadRequest("Aluno Não Atualizado!");
        }
        // api/aluno/{id}
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoPatchDto model){
            var aluno = _repo.GetAlunoById(id);
           
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");
           
            _mapper.Map(model, aluno);
            
            _repo.Update(aluno);
          
            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoPatchDto>(aluno));
          
            return BadRequest("Aluno Não Atualizado!");
        }
        // api/aluno/{id}/trocarEstado
        [HttpPatch("{id}/trocarEstado")]
        public IActionResult trocarEstado(int id, TrocaEstadoDto trocaEstado){
            var aluno = _repo.GetAlunoById(id);
            
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");            
            
            aluno.Ativo = trocaEstado.Estado;
            
            _repo.Update(aluno);
            
            if (_repo.SaveChanges()){
                var msn = aluno.Ativo ? "ativado" : "desativado";
                return Ok(new { message = $"Aluno {msn} com sucesso!"});
            }                
            return BadRequest("Aluno Não Atualizado!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
       
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");
        
            _repo.Delete(aluno);
        
            if (_repo.SaveChanges())
                return Ok(aluno);
        
            return BadRequest("Aluno Não Deletado!");
        }
    }
}