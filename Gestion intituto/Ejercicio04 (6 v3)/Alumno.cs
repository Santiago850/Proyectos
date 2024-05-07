using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio04__6_v3_
{
    public class Alumno
    {
        private string _dni;
        private string _nombre;
        private string _telefono;
        private int _codCurso;
        private double[,] _notas; 
        
        public string Dni { get { return _dni; } set { _dni = value.ToUpper(); } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Telefono { get { return _telefono; } set { _telefono = value; } }
        public int Curso { get { return _codCurso; } set { _codCurso = value; } }
        public double[,] Notas { get { return _notas; } set { _notas = value; } }

        public Alumno(string dni, string nombre, int codCurso, string telefono = null, double[,] notas = null)
        {
            _dni = dni.ToUpper();
            _nombre = nombre;
            _telefono = telefono;
            _codCurso = codCurso;
            _notas = notas;
        }


    }
}
