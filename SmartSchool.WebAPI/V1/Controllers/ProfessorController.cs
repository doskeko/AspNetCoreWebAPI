using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
         private readonly IRepository _repo;
         public readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
            
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null)
                return BadRequest("Professor não encontrado!");
                var ProfessorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professor);
        }
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
             var professor = _mapper.Map<Professor>(model);
          _repo.Add(professor);
            if (_repo.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            return BadRequest("Professor Não Cadastrado.");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id);
             if (professor == null)
                return BadRequest("Professor não encontrado!");
                _mapper.Map(model, professor);
            _repo.Update(professor);
            if (_repo.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            return BadRequest("Professor Não Cadastrado.");
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id);
             if (professor == null)
                return BadRequest("Professor não encontrado!");
                _mapper.Map(model, professor);
            _repo.Update(professor);
            if (_repo.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            return BadRequest("Professor Não Cadastrado.");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
             if (professor == null)
                return BadRequest("Aluno não encontrado!");
            _repo.Delete(professor);
            _repo.SaveChanges();
            return Ok();
        }
    }
}