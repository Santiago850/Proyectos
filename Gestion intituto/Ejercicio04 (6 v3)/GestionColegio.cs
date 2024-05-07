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
    public partial class GestionColegio : Form
    {
        public GestionColegio()
        {
            InitializeComponent();
        }

        private void btnCursos_Click(object sender, EventArgs e)
        {
            GestionCursos Gcursos = new GestionCursos();
            Gcursos.ShowDialog();
        }

        private void btnAlumnos_Click(object sender, EventArgs e)
        {
            GestionAlumnos Galumnos = new GestionAlumnos();
            Galumnos.ShowDialog();
        }

        private void btnProfesores_Click(object sender, EventArgs e)
        {
            GestionProfesores Gprofes = new GestionProfesores();
            Gprofes.ShowDialog();
        }
    }
}
