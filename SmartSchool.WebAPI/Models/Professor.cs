using System.Collections.Generic;

namespace SmartSchool.WebAPI.Models
{
    public class Professor
    {
        public Professor() { }

        public Professor(int iD, string nome)
        {
            this.ID = iD;
            this.Nome = nome;
        }
        public int ID { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}