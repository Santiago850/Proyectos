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
    public partial class GestionCursos : Form
    {
        public GestionCursos()
        {
            InitializeComponent();
        }

        private SqlHelper helper;
        private int pos;
        private bool empezarValidar = false;        

        private void MostrarRegistro(int pos)
        {
            // si no hay registros en la BD
            if (helper.RegistroMax == 0)
            {
                btnLimpiar_Click(null, null);
            }
            else //si si los hay
            {
                Curso curso = helper.DevolverCurso(pos);
                txtCodigo.Text = curso.CodCurso.ToString();
                txtNombre.Text = curso.Nombre;
            }

            lblRegistro.Text = $"{pos + 1} / {helper.RegistroMax}";
        }

        private Curso GetCursoActual()
        {           
            return new Curso(int.Parse(txtCodigo.Text), txtNombre.Text);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();

            // este if es por si no hay registros en la BD
            int lastPos = helper.DevolverUltimoCodigoCurso();
            if (lastPos != 0)
            {
                int nuevaPos = lastPos + 1;
                txtCodigo.Text = nuevaPos.ToString();
            }
            else
            {
                txtCodigo.Clear();
            }
            empezarValidar = true;
            btnAnadir.Enabled = true;
        }

        private void GestionCursos_Load(object sender, EventArgs e)
        {
            helper = new SqlHelper("SELECT * FROM Curso");

            pos = helper.RegistroMax == 0 ? -1 : 0;

            MostrarRegistro(pos);
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            if (pos != 0)
            {
                //este codigo es para actualzar mientras te mueves
                Curso cursoActual = GetCursoActual();
                Curso cursoBD = helper.DevolverCurso(pos);
                if (helper.ACambiadoCurso(cursoActual, pos))
                {
                    DialogResult preguntar;
                    preguntar = MessageBox.Show($"¿Quieres actualizar el curso {cursoBD.Nombre}?",
                        "", MessageBoxButtons.YesNo);

                    if (preguntar == DialogResult.Yes)
                    {
                        //si nombre es correto
                        if (Validar.NombreCurso(cursoActual.Nombre))
                        {
                            btnActualizar_Click(sender, e);
                        }
                        else { MessageBox.Show("El nombre no es valido."); }
                    }
                }
                
                pos = 0;
                MostrarRegistro(pos);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                //este codigo es para actualzar mientras te mueves
                Curso cursoActual = GetCursoActual();
                Curso cursoBD = helper.DevolverCurso(pos);
                if (helper.ACambiadoCurso(cursoActual, pos))
                {
                    DialogResult preguntar;
                    preguntar = MessageBox.Show($"¿Quieres actualizar el curso {cursoBD.Nombre}?",
                        "", MessageBoxButtons.YesNo);

                    if (preguntar == DialogResult.Yes)
                    {
                        //si nombre es correto
                        if (Validar.NombreCurso(cursoActual.Nombre))
                        {
                            btnActualizar_Click(sender, e);
                        }
                        else { MessageBox.Show("El nombre no es valido."); }
                    }
                }

                pos--;
                MostrarRegistro(pos);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (pos < helper.RegistroMax - 1)
            {
                //este codigo es para actualzar mientras te mueves
                Curso cursoActual = GetCursoActual();
                Curso cursoBD = helper.DevolverCurso(pos);
                if (helper.ACambiadoCurso(cursoActual, pos))
                {
                    DialogResult preguntar;
                    preguntar = MessageBox.Show($"¿Quieres actualizar el curso {cursoBD.Nombre}?",
                        "", MessageBoxButtons.YesNo);

                    if (preguntar == DialogResult.Yes)
                    {
                        //si nombre es correto
                        if (Validar.NombreCurso(cursoActual.Nombre))
                        {
                            btnActualizar_Click(sender, e);
                        }
                        else { MessageBox.Show("El nombre no es valido."); }
                    }
                }

                pos++;
                MostrarRegistro(pos);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (pos != helper.RegistroMax - 1)
            {
                //este codigo es para actualzar mientras te mueves
                Curso cursoActual = GetCursoActual();
                Curso cursoBD = helper.DevolverCurso(pos);
                if (helper.ACambiadoCurso(cursoActual, pos))
                {
                    DialogResult preguntar;
                    preguntar = MessageBox.Show($"¿Quieres actualizar el curso {cursoBD.Nombre}?",
                        "", MessageBoxButtons.YesNo);

                    if (preguntar == DialogResult.Yes)
                    {
                        //si nombre es correto
                        if (Validar.NombreCurso(cursoActual.Nombre))
                        {
                            btnActualizar_Click(sender, e);
                        }
                        else { MessageBox.Show("El nombre no es valido."); }
                    }
                }

                pos = helper.RegistroMax -1;
                MostrarRegistro(pos);
            }
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            Curso cursoActual = GetCursoActual();

            helper.AnadirCurso(cursoActual);
            pos = helper.RegistroMax - 1;
            MostrarRegistro(pos);

            //despues de añadir se dejara de validar y de poder añadir
            empezarValidar = false;
            txtNombre.BackColor = Color.White;
            btnAnadir.Enabled = false;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Curso nuevoCurso = GetCursoActual();
            helper.ActualizarCurso(nuevoCurso, pos);

            // desactiva la validacion, el boton en si y poder añadir
            empezarValidar = false;
            btnActualizar.Enabled = false;
            btnAnadir.Enabled = false;
            txtNombre.BackColor = Color.White;
        }

        private void Validacion(object sender, EventArgs e)
        {
            Curso cursoActual = GetCursoActual();
            
            if (empezarValidar)
            {
                //Si validar esta activo pero el txt coincide con lo que pone en la BD:
                //desactiva la validacion, actualizacion y poder añadir.
                if (!helper.ACambiadoCurso(cursoActual, pos))
                {
                    btnActualizar.Enabled = false;
                    btnAnadir.Enabled = false;
                    empezarValidar = false;
                    txtNombre.BackColor = Color.White;
                }
                else
                {
                    // esta es la validacion 
                    if (!Validar.NombreCurso(cursoActual.Nombre))
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
                if (helper.ACambiadoCurso(cursoActual, pos))
                {
                    btnActualizar.Enabled = true;
                    btnAnadir.Enabled = true;
                    empezarValidar = true;

                    //se vuelve a validar porque durante un momento el txt
                    //se queda en blanco a persar de estarse validando
                    if (!Validar.NombreCurso(cursoActual.Nombre))
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Solo se pueden eliminar registros si HAY registros en la BD
            if (helper.RegistroMax != 0)
            {
                Curso curso = helper.DevolverCurso(pos);

                DialogResult preguntar;
                preguntar = MessageBox.Show($"¿Estas seguro que quieres eliminar el curso {curso.Nombre}?",
                    "", MessageBoxButtons.YesNo);

                if (preguntar == DialogResult.Yes)
                {
                    helper.EliminarCurso(pos);

                    // cambio la posicion, en caso que sea el primer registro vuelvo al primero
                    if (pos != 0)
                    {
                        pos--;
                        MostrarRegistro(pos);
                    }
                    else
                    {
                        pos = 0;
                        MostrarRegistro(pos);
                    }
                }
            }
        }
    }
}
