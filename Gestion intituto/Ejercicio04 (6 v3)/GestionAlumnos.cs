using Ejercicio01;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio04__6_v3_
{
    public partial class GestionAlumnos : Form
    {
        
        public GestionAlumnos()
        {
            InitializeComponent();
        }
        private SqlHelper helpAlumno, helpNotas, helpCurso;
        private int pos;
        private bool empezarValidar = false;

        private Alumno GetAlumnoActual()
        {
            Alumno alu = new Alumno(txtDni.Text, txtNombre.Text, int.Parse(txtCursoCodigo.Text));
            return alu;
        }

        private void TodosEnBlanco()
        {
            txtDni.BackColor = Color.White;
            txtNombre.BackColor = Color.White;
            txtTelefono.BackColor = Color.White;
            txtCursoCodigo.BackColor = Color.White;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCursoCodigo.Clear();
            txtCurso.Clear();

            empezarValidar = true;
            btnAnadir.Enabled = true;
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            Alumno aluActual = GetAlumnoActual();

            helpAlumno.AnadirAlumno(aluActual);
            pos = helpAlumno.RegistroMax - 1;

            MostrarAlumno(pos);

            //despues de añadir se dejara de validar y de poder añadir
            empezarValidar = false;
            btnAnadir.Enabled = false;
            //txtNombre.BackColor = Color.White;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Alumno aluActual = GetAlumnoActual();
            helpAlumno.ActualizarAlumno(aluActual, pos);

            // desactiva la validacion, el boton en si y poder añadir
            empezarValidar = false;
            btnActualizar.Enabled = false;
            btnAnadir.Enabled = false;
            //txtNombre.BackColor = Color.White;
        }

        private void Validacion(object sender, EventArgs e)
        {
            Alumno aluActual = GetAlumnoActual();

            if (empezarValidar)
            {
                //Si validar esta activo pero el txt coincide con lo que pone en la BD:
                //desactiva la validacion, actualizacion y poder añadir.
                if (!helpAlumno.ACambiadoAlumno(aluActual, pos))
                {
                    btnActualizar.Enabled = false;
                    btnAnadir.Enabled = false;
                    empezarValidar = false;
                    txtNombre.BackColor = Color.White;
                }
                else
                {
                    // esta es la validacion 
                    if (!Validar.Nombre(aluActual.Nombre))
                    {
                        txtNombre.BackColor = Color.FromArgb(255, 255, 150, 150);
                        btnAnadir.Enabled = false;
                        btnActualizar.Enabled = false;
                    }
                    else
                    {
                        txtNombre.BackColor = Color.FromArgb(255, 150, 255, 150);
                        btnAnadir.Enabled = true;
                        btnActualizar.Enabled = true;
                    }
                }
            }
            else
            {
                //si no esta validando pero el txt es diferente al registro en la BD:
                //empezara la validacion, actualizacion y poder añadir
                if (helpAlumno.ACambiadoAlumno(aluActual, pos))
                {
                    btnActualizar.Enabled = true;
                    btnAnadir.Enabled = true;
                    empezarValidar = true;

                    //se vuelve a validar porque durante un momento el txt
                    //se queda en blanco a persar de estarse validando
                    if (!Validar.Nombre(aluActual.Nombre))
                    {
                        txtNombre.BackColor = Color.FromArgb(255, 255, 150, 150);
                        btnAnadir.Enabled = false;
                        btnActualizar.Enabled = false;
                    }
                    else
                    {
                        txtNombre.BackColor = Color.FromArgb(255, 150, 255, 150);
                        btnAnadir.Enabled = true;
                        btnActualizar.Enabled = true;
                    }
                }
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            if (pos != 0)
            {
                //este codigo es para actualzar mientras te mueves
                Alumno aluActual = GetAlumnoActual();

                Alumno aluBD = helpAlumno.DevolverAlumno(pos, null);
                double[,] notas = helpNotas.DevolverNotas(aluBD.Dni);
                aluBD.Notas = notas;

                if (helpAlumno.ACambiadoAlumno(aluActual, pos))
                {
                    DialogResult preguntar;
                    preguntar = MessageBox.Show($"¿Quieres actualizar el curso {aluBD.Nombre}?",
                        "", MessageBoxButtons.YesNo);

                    if (preguntar == DialogResult.Yes)
                    {
                        //si Alumno es correto
                        if (Validar.Alumno(aluActual))
                        {
                            btnActualizar_Click(sender, e);
                        }
                        else { MessageBox.Show("El Alumno no es valido."); }
                    }
                }

                pos = 0;
                MostrarAlumno(pos);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {

        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {

        }

        private void GestionAlumnos_Load(object sender, EventArgs e)
        {
            helpAlumno = new SqlHelper("SELECT * FROM Alumno");
            helpNotas = new SqlHelper("SELECT * FROM Notas");
            helpCurso = new SqlHelper("SELECT * FROM Curso");
            pos = 0;
            MostrarAlumno(pos);
        }

        private void MostrarAlumno(int pos)
        {
            Alumno alu = helpAlumno.DevolverAlumno(pos, null);
            double[,] notas = helpNotas.DevolverNotas(alu.Dni);
            string nombreCurso = helpCurso.DevolverNombreCurso(alu.Curso);
            
            alu.Notas = notas;

            txtDni.Text = alu.Dni;
            txtNombre.Text = alu.Nombre;
            txtTelefono.Text = alu.Telefono;
            txtCursoCodigo.Text = alu.Curso.ToString();
            txtCurso.Text = nombreCurso;

            //dgvTablaNotas.
            lblRegistro.Text = $"{pos + 1} / {helpAlumno.RegistroMax}";
        }
    }
}
