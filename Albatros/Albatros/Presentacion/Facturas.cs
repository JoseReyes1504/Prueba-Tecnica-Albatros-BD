using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Albatros.Presentacion
{
    public partial class Facturas : Form
    {
        ClsFacturas bdFacturas = new ClsFacturas();
            
        public Facturas()
        {
            InitializeComponent();
        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            bdFacturas.CargarFacturas(dgv);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblCodigoFac.Text = dgv.Rows[e.RowIndex].Cells["ID"].Value.ToString();

            bdFacturas.Llenardgv(dgv2, Convert.ToInt32(lblCodigoFac.Text));
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Informes inf = new Informes();
            inf.TipoReporte1 = "Factura";
            inf.CodigoFac1 = Convert.ToInt32(lblCodigoFac.Text);
            inf.Show();
        }
    }
}
