using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio04__6_v3_
{
    public class Profesor
    {
        private string _dni;        
        private string _nombre;
        private string _telefono;
        private int _cursoTutor;
        private List<string> _asignaturas;

        public Profesor(string dni, string nombre, int cursoTutor, List<string> asignaturas, string telefono = null)
        {
            _dni = dni.ToUpper();
            _nombre = nombre;
            _telefono = telefono;
            _cursoTutor = cursoTutor;
            
            _asignaturas = _asignaturas == null ? new List<string>() : _asignaturas;
        }
    }
}
