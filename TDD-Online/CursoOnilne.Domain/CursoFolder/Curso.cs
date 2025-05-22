using CursoOnilne.Domain.Test.Cursos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnilne.Domain.CursoFolder
{
    public  class Curso
    {
        public string Nome { get; private set; }

        public double CargaHoraria { get; private set; }

        public PublicoAlvo PublicoAlvo { get; set; }

        public decimal Valor { get; set; }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, decimal valor)
        {
            this.Nome = nome;
            this.CargaHoraria = cargaHoraria;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;
        }
    }
}
