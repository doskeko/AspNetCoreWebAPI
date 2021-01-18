using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        public Repository(SmartContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDiciplinas)
                .ThenInclude(ad => ad.Disciplina)
                .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.ID);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDiciplinas)
                .ThenInclude(ad => ad.Disciplina)
                .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking()
                        .OrderBy(a => a.ID)
                        .Where(aluno => aluno.AlunosDiciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDiciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking()
                        .OrderBy(a => a.ID)
                        .Where(aluno => aluno.ID == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                .ThenInclude(d => d.AlunosDiciplinas)
                .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking().OrderBy(p => p.ID);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                .ThenInclude(d => d.AlunosDiciplinas)
                .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking().OrderBy(p => p.ID)
                    .Where(professor => professor.Disciplinas.Any(d => d.ID == disciplinaId));

            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeAluno)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                            .ThenInclude(d => d.AlunosDiciplinas)
                            .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking().OrderBy(p => p.ID)
                         .Where(professor => professor.ID == professorId);

            return query.FirstOrDefault();
        }
    }
}