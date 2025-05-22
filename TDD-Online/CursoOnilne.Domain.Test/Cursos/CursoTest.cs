using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursoOnilne.Domain.CursoFolder;
using CursoOnilne.Domain.Test.Cursos.Enums;


namespace CursoOnilne.Domain.Test.Cursos
{
    public class CursoTest
    {
        // criar um curso com os seguintes dados
        // nome, carga horaria, publico and valor do curso

        [Fact]
        public void Curso_CriarCurso_RetornaCurso()
        {
            var CursoEsperado = new
            {
                Nome = "Curso de TDD",
                CargaHoraria = (double)40,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (decimal) 100
            };
            
            var curso = new Curso(CursoEsperado.Nome, CursoEsperado.CargaHoraria, CursoEsperado.PublicoAlvo, CursoEsperado.Valor);

            Assert.Equal(CursoEsperado.Nome, curso.Nome);
        }

    }

}
