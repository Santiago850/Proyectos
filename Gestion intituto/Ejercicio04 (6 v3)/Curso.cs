using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio04__6_v3_
{
    public class Curso
    {
        private int _codCurso;
        private string _nombre;

        public int CodCurso { get { return _codCurso;} }
        public string Nombre { get { return _nombre;} set { _nombre = value; } }

        public Curso(int codCurso, string nombre)
        {
            _codCurso = codCurso;
            _nombre = nombre;
        }
    }
}
