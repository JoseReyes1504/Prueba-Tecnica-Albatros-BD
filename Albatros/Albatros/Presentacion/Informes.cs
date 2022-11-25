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
    public partial class Informes : Form
    {
        private string TipoReporte;
        private int CodigoFac;

        public string TipoReporte1 { get => TipoReporte; set => TipoReporte = value; }
        public int CodigoFac1 { get => CodigoFac; set => CodigoFac = value; }

        public Informes()
        {
            InitializeComponent();
        }

        

        private void Informes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'albatros.Productos' Puede moverla o quitarla según sea necesario.
            this.productosTableAdapter.Fill(this.albatros.Productos);
            // TODO: esta línea de código carga datos en la tabla 'albatros.Clientes' Puede moverla o quitarla según sea necesario.
            this.clientesTableAdapter.Fill(this.albatros.Clientes);
            // TODO: esta línea de código carga datos en la tabla 'albatros1.Productos' Puede moverla o quitarla según sea necesario.
            this.productosTableAdapter.Fill(this.albatros.Productos);
            if (TipoReporte1 == "Clientes")
            {
                reportViewer1.Visible = true;
                reportViewer2.Visible = false;
                reportViewer3.Visible = false;
                this.clientesTableAdapter.Fill(this.albatros.Clientes);
                this.reportViewer1.RefreshReport();
            }
            else if(TipoReporte1 == "Factura")
            {                
                reportViewer1.Visible = false;
                reportViewer2.Visible = true;
                reportViewer3.Visible = false;
                this.detallesFacturaTableAdapter.Fill(this.albatros.DetallesFactura, CodigoFac);
                this.reportViewer2.RefreshReport();
            }
            else
            {
                reportViewer1.Visible = false;
                reportViewer3.Visible = true;
                reportViewer2.Visible = false;
                
                this.reportViewer3.RefreshReport();
            }
        }
    }
}
