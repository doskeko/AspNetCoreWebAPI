namespace SmartSchool.WebAPI.Models
{
    public class AlunoDisciplina
    {
        public AlunoDisciplina() { }
        public AlunoDisciplina(int alunoID, int disciplinaID)
        {
            this.AlunoID = alunoID;
            this.DisciplinaID = disciplinaID;
        }
        public int AlunoID { get; set; }
        public Aluno Aluno { get; set; }
        public int DisciplinaID { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}