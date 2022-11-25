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
    public partial class MenuInicio : Form
    {
        public MenuInicio()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            clientes cl = new clientes();
            cl.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            productos pr = new productos();
            pr.ShowDialog();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            facturacion fac = new facturacion();
            fac.ShowDialog();
        }

        private void btnFacturasS_Click(object sender, EventArgs e)
        {
            Facturas Fac = new Facturas();
            Fac.Show();
        }
    }
}
