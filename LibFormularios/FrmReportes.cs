using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibClases;
namespace LibFormularios
{
    public partial class FrmReportes : Form
    {
        CDocVenta aDocVenta = new CDocVenta();
        CDetalleVenta aDetalleVenta = new CDetalleVenta();
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void CboReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DgvReportes.DataSource = aDocVenta.GenerarReporte(CboReportes.Text);
        }
    }
}
