
using pantalla_1;
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
    public partial class facturacion : Form
    {
        ClsFacturas fac = new ClsFacturas();
        clsValidaciones val = new clsValidaciones();

        int ID = 0;
        int CodigoFact = 0;                
        string Fecha = "";
        int CodigoDetalleFactura = 0;

        public facturacion()
        {
            InitializeComponent();
        }

        public void ImprimirFactura()
        {
            Informes inf = new Informes();
            inf.TipoReporte1 = "Factura";
            inf.CodigoFac1 = CodigoFact;
            inf.Show();
        }

        private void facturas_Load(object sender, EventArgs e)
        {
            fac.LlenarCombox(cbClientes);
        }

        private void cbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ID = fac.CapturarIdCb(cbClientes);           
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            fac.LlenarListBox(txtBusqueda, lbProductos);
        }

        private void lbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            fac.SeleccionarDeListBox(lbProductos, txtDescripcion, txtPrecio, txtImpuesto, txtCantidad, lbCodigo, LbImpuestoID);
            btnAgregar.Enabled = true;
            btnAgregar.Text = "Agregar";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(cbClientes.Text == "")
            {
                cbClientes.DroppedDown = true;
            }
            else
            {
                if(txtCantidad.Text == "0" )
                {
                    MessageBox.Show("Cantidad de producto no puede ser 0", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCantidad.Focus();                    
                }
                else
                {
                    if (btnAgregar.Text == "Agregar")
                    {
                        if (CodigoFact == 0)
                        {
                            Fecha = String.Format("{0:MM/dd/yyyy}", DtFecha.Value);
                            fac.CrearFactura(Fecha, Convert.ToDouble(txtImpuestoTotal.Text), Convert.ToDouble(txtTotal.Text), ID);
                            CodigoFact = fac.ObtenerUltimaFactura("Factura");
                            InsertarDatosYActualizar();
                        }
                        else
                        {
                            InsertarDatosYActualizar();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Se Edito el producto");
                        Limpiar();
                    }
                }         
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CodigoDetalleFactura = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ID"].Value.ToString());            

            DialogResult R = new DialogResult();

            R = MessageBox.Show("Desea Editar Este producto", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(R == DialogResult.Yes)
            {
                btnAgregar.Text = "Editar";
                txtDescripcion.Text = dgv.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
                txtCantidad.Text = dgv.Rows[e.RowIndex].Cells["Cantidad"].Value.ToString();
                txtImpuesto.Text = dgv.Rows[e.RowIndex].Cells["Impuesto"].Value.ToString();
                txtPrecio.Text = dgv.Rows[e.RowIndex].Cells["Precio"].Value.ToString();
                txtCantidad.Focus();
            }
            else
            {
                btnAgregar.Text = "Agregar";
            }                       
        }

        public void LimpiarTodo()
        {
            Limpiar();
            txtBusqueda.Clear();
            cbClientes.Text = "";
            txtTotal.Text = "0";
            txtImpuestoTotal.Text = "0";
            fac.Llenardgv(dgv, CodigoFact);
        }

        public void Limpiar()
        {
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            txtImpuesto.Clear();
            btnAgregar.Enabled = false;
        }

        public void InsertarDatosYActualizar()
        {
            fac.InsertarProductosDetalle(Convert.ToInt32(lbCodigo.Text), Convert.ToInt32(txtCantidad.Text), CodigoFact);
            fac.Llenardgv(dgv, CodigoFact);
            txtTotal.Text = fac.ObtenerTotalFactura(CodigoFact).ToString();
            txtImpuestoTotal.Text = fac.ObtenerTotalImpuesto(CodigoFact).ToString();
            Limpiar();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "" || txtCantidad.Text == "0")
            {
                btnAgregar.Enabled = false;
            }
            else
            {
                btnAgregar.Enabled = true;
            }
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFactura();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void cbClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.NoModificar(e);            
        }

        private void cbClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Back)
            {
                MessageBox.Show("No se puede eliminar el texto del combobox", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            DialogResult R = new DialogResult();

            R = MessageBox.Show("Desea Imprimir la Factura?", "Infomacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(R == DialogResult.Yes)
            {
                ImprimirFactura();
                fac.ActulizarFactura(Convert.ToDouble(txtTotal.Text), Convert.ToDouble(txtImpuestoTotal.Text), CodigoFact);
                MessageBox.Show("Compra Realizada", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CodigoFact = 0;
                LimpiarTodo();
            }
            else
            {
                fac.ActulizarFactura(Convert.ToDouble(txtTotal.Text), Convert.ToDouble(txtImpuestoTotal.Text), CodigoFact);
                MessageBox.Show("Compra Realizada", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CodigoFact = 0;
                LimpiarTodo();
            }
            
        }
    }
}
