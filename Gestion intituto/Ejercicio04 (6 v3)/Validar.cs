using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ejercicio04__6_v3_
{
    public static class Validar
    {
        public static bool Dni(string dni)
        {
            bool res = false;

            if (Regex.IsMatch(dni, @"[0-9]{8}[A-Z]{1}") && dni.Length == 9)
            {
                res = true;
            }
            return res;
        }

        public static bool Nombre(string nombre)
        {
            bool res = true;

            if (Regex.IsMatch(nombre, @"[0-9ºª´#~€¬´!€/*<>=+`¡'?¿}{)(]+$"))
            {
                res = false;
            }
            return res;
        }

        public static bool NombreCurso(string nombre)
        {
            bool res = false;

            if (Regex.IsMatch(nombre, @"[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                res = true;
            }
            return res;
        }

        public static bool Telefono(string telefono)
        {
            bool res = false;

            if (Regex.IsMatch(telefono, @"[0-9]+$") && telefono.Length == 9)
            {
                res = true;
            }
            return res;
        }

        public static bool CodCurso(int cod)
        {
            bool res = false;

            if (Regex.IsMatch(cod.ToString(), @"[1-9]+$"))
            {
                res = true;
            }
            return res;
        }

        public static bool Alumno(Alumno alumno)
        {
            bool res = true;

            if (!Validar.Dni(alumno.Dni)) { res = false; }
            if (!Validar.Nombre(alumno.Nombre)) { res = false; }

            if (alumno.Telefono != null)
            {
                if (!Validar.Telefono(alumno.Telefono)) { res = false; }
            }

            if (!Validar.CodCurso(alumno.Curso)) { res = false; }

            return res;
        }
    }
}