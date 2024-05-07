using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using System.Windows.Forms;
using Ejercicio04__6_v3_;
using System.Net.Configuration;

namespace Ejercicio01
{
    public class SqlHelper
    {
        private DataSet dataSet;
        private SqlDataAdapter adapter;
        private SqlConnection conexion;
        private int registroMax;

        public int RegistroMax { get => registroMax; }

        public SqlHelper(string consulta)
        {
            string ruta = Path.GetFullPath(@"..\..\AppData\Instituto.mdf");

            string connect = $"Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename= {ruta};Integrated Security=True;Connect Timeout=30";

            conexion = new SqlConnection(connect);
            
            conexion.Open();

            dataSet = new DataSet();
            adapter = new SqlDataAdapter(consulta, conexion);
            adapter.Fill(dataSet);
            registroMax = dataSet.Tables[0].Rows.Count;

            conexion.Close();
        }        

        //----------------------------------//
        //     METODOS DE LOS CURSOS        //
        //----------------------------------//

        public int DevolverUltimoCodigoCurso()
        {
            int res;
            if (registroMax != 0)
            {
                int lastPos = registroMax - 1;
                DataRow row = dataSet.Tables[0].Rows[lastPos];
                res = (int)row[0];
            }
            else
            {
                res = 0;
            }

            //devuelve la ultima posicion
            //si no hay registro, devuelve 0
            return res;
        }

        public Curso DevolverCurso(int pos)
        {
            DataRow registro = dataSet.Tables[0].Rows[pos];
            int cod = (int)(registro[0]);
            string nombre = registro[1].ToString();
            
            Curso curso = new Curso(cod, nombre);

            return curso;
        }

        public void AnadirCurso(Curso nuevoCurso)
        {
            DataRow row = dataSet.Tables[0].NewRow();
            row[1] = nuevoCurso.Nombre;

            dataSet.Tables[0].Rows.Add(row);

            SqlCommandBuilder comando = new SqlCommandBuilder(adapter);

            adapter.Update(dataSet);
            dataSet.Clear();
            adapter.Fill(dataSet);

            registroMax = dataSet.Tables[0].Rows.Count;
        }

        public void ActualizarCurso(Curso curso, int pos)
        {
            DataRow row = dataSet.Tables[0].Rows[pos];
            row[1] = curso.Nombre;

            //dataSet.Tables[0].Rows[pos] = row;

            SqlCommandBuilder comando = new SqlCommandBuilder(adapter);
            adapter.Update(dataSet);
            dataSet.Clear();
            adapter.Fill(dataSet);

        }
        
        public bool ACambiadoCurso(Curso cursoActual, int pos)
        {
            bool res = false;
            
            DataRow row = dataSet.Tables[0].Rows[pos];
            Curso curso = new Curso((int)row[0], row[1].ToString());

            if (cursoActual.Nombre != curso.Nombre || cursoActual.CodCurso != curso.CodCurso)
            {
                res = true;
            }

            //este metodo devuelve true si el curso que se le pasa es diferente
            //al que hay en la BD
            return res;
        }

        public void EliminarCurso(int pos)
        {
            if (pos != -1)
            {
                dataSet.Tables[0].Rows[pos].Delete();

                SqlCommandBuilder comando = new SqlCommandBuilder(adapter);
                adapter.Update(dataSet);

                registroMax = dataSet.Tables[0].Rows.Count;
            }
        }

        //----------------------------------//
        //     METODOS DE LOS AlUMNOS       //
        //----------------------------------//

        public Alumno DevolverAlumno(int pos, double[,] notas)
        {
            DataRow row = dataSet.Tables[0].Rows[pos];

            string dni = row[0].ToString();
            string nombre = row[1].ToString();
            string tel = row[2].ToString();
            int cod = (int)row[3];

            Alumno alu = new Alumno(dni, nombre, cod, tel, notas);

            return alu;
        }

        //represento las notas con un array bidimensional porque en el almaceno
        //el numero del examen y la nota del mismo
        public double[,] DevolverNotas(string dni)
        {
            double[,] notas;            
            DataSet dataSetNotas = new DataSet();
            SqlDataAdapter adapterNotas = new SqlDataAdapter($"SELECT * FROM Notas WHERE dni = {dni}",
                conexion);
            DataRow row;

            //si el alumno tiene alguna nota
            if (registroMax != 0)
            {
                notas = new double[2, registroMax];

                for (int i = 0; i <= registroMax; i++)
                {
                    row = dataSetNotas.Tables[0].Rows[i];

                    notas[0, i] = (double)row[1];
                    notas[1, i] = (double)row[2];
                }
            }
            else
            {
                notas = null;
            }

            return notas;
        }

        public string DevolverNombreCurso(int cod)
        {
            string nombre = null;
            DataRow row;

            for (int i = 0; i < registroMax; i++)
            {
                row = dataSet.Tables[0].Rows[i];

                if ((int)row[0] == cod)
                {
                    nombre = row[1].ToString();
                }        
            }
            return nombre;
        }

        public bool ACambiadoAlumno(Alumno alumno, int pos)
        {
            bool res = false;

            DataRow row = dataSet.Tables[0].Rows[pos];

            if (alumno.Dni != row[0].ToString() || alumno.Nombre != row[1].ToString() ||
                alumno.Telefono != row[2].ToString() || alumno.Curso != (int)row[3])
            {
                res = true;
            }
            return res;
        }

        public void AnadirAlumno (Alumno alumnoNuevo)
        {
            DataRow row = dataSet.Tables[0].NewRow();                        

            row[0] = alumnoNuevo.Dni;
            row[1] = alumnoNuevo.Nombre;
            row[2] = alumnoNuevo.Telefono;
            row[3] = alumnoNuevo.Curso;

            SqlCommandBuilder comando = new SqlCommandBuilder(adapter);
            adapter.Update(dataSet);
            dataSet.Clear();
            adapter.Fill(dataSet);

            registroMax = dataSet.Tables[0].Rows.Count;
        }

        public void ActualizarAlumno(Alumno alumno, int pos)
        {
            DataRow row = dataSet.Tables[0].Rows[pos];
            row[0] = alumno.Dni;
            row[1] = alumno.Nombre;
            row[2] = alumno.Telefono;
            row[3] = alumno.Curso;

            SqlCommandBuilder comando = new SqlCommandBuilder(adapter);
            adapter.Update(dataSet);
            dataSet.Clear();
            adapter.Fill(dataSet);            
        }
    }
}
