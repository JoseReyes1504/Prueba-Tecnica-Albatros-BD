using System;
using pantalla_1;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Albatros.Presentacion
{
    public partial class productos : Form
    {
        /////////////////////////////////////////////// Clases//////////////////////////////////////////////////////
        ClsProductos bdProductos = new ClsProductos();
        clsConexionBD bd = new clsConexionBD();
        clsValidaciones val = new clsValidaciones();

        /////////////////////////////////////////////// Variables //////////////////////////////////////////////////////
        int ID;
        int Codigo = 0;

        /////////////////////////////////////////////// Funciones //////////////////////////////////////////////////////
        public void Limpiar()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            cbImpuesto.Text = "";
            rbAgregar.Enabled = true;
            rbAgregar.Checked = true;
        }

        /// //////////////////////////////////////////// Funciones Botones //////////////////////////////////////////////////////

        public productos()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            switch (btnAgregar.Text)
            {
                case "Agregar":
                    Codigo = bdProductos.CapturarCodigoProduco(txtCodigo.Text);

                    if (txtCodigo.Text != "" && txtDescripcion.Text != "" && txtCantidad.Text != "" && txtPrecio.Text != "" && cbImpuesto.Text != "")
                    {
                        if (Codigo == Convert.ToInt32(txtCodigo.Text))
                        {
                            MessageBox.Show("Ya hay un producto registrado con este codigo", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {                            
                                bdProductos.InsertarProductos(txtCodigo.Text, txtDescripcion.Text, Convert.ToDouble(txtCantidad.Text) ,txtPrecio.Text, ID);
                                MessageBox.Show("Los Datos Se Agregaron Correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                bdProductos.DatosProductos(dgv);                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("Datos Vacios", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "Editar":
                    if (txtCodigo.Text != "" && txtDescripcion.Text != "" && txtCantidad.Text != "" && txtPrecio.Text != "" && cbImpuesto.Text != "")
                    {
                        ID = bdProductos.CapturarIdCb(cbImpuesto, ID);
                        bdProductos.EditarProducto(txtDescripcion.Text, Convert.ToDouble(txtPrecio.Text), ID, Convert.ToInt32(txtCodigo.Text));
                        MessageBox.Show("Los Datos Se Actualizaron Correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bdProductos.DatosProductos(dgv);
                    }

                        break;
                case "Eliminar":
                    bdProductos.EliminarProducto(Convert.ToInt32(txtCodigo.Text));
                    MessageBox.Show("El producto se elimino Correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bdProductos.DatosProductos(dgv);
                    break;
            }

            
        }

        private void productos_Load(object sender, EventArgs e)
        {
            bdProductos.llenarImpuestos(cbImpuesto);
            bdProductos.DatosProductos(dgv);
        }

        private void cbImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
           ID = bdProductos.CapturarIdCb(cbImpuesto, ID);            
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            Inventario inv = new Inventario();
            inv.Show();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }


        private void rbAgregar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAgregar.Checked == true)
            {
                btnAgregar.Text = "Agregar";
                txtCodigo.ReadOnly = false;
                txtCantidad.ReadOnly = false;
            }
        }

        private void rbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if(rbEliminar.Checked == true)
            {
                btnAgregar.Text = "Eliminar";
                txtCodigo.ReadOnly = true;
                txtCantidad.ReadOnly = true;
            }
        }

        private void rbEditar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEditar.Checked == true)
            {
                btnAgregar.Text = "Editar";
                txtCodigo.ReadOnly = true;
                txtCantidad.ReadOnly = true;
            }
        }

        /////////////////////////////////////////////// Validar los textbox //////////////////////////////////////////////////////
        /// los textbox solo aceptaran el tipo de entrada que le indicamos con la clase validaciones
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);   
        }

        private void cbImpuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.NoModificar(e);            
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.LetrasNumeros(e, txtDescripcion);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDescripcion.Text = dgv.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
            txtCodigo.Text = dgv.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();            
            txtPrecio.Text = dgv.Rows[e.RowIndex].Cells["Precio"].Value.ToString();
            cbImpuesto.Text = dgv.Rows[e.RowIndex].Cells["Impuesto"].Value.ToString();
            
            rbAgregar.Enabled = false;
            rbEditar.Checked = true;
            btnAgregar.Text = "Editar";
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            bd.CualquierTabla(dgv, "Select * from Productos where Descripcion like '%" + txtFiltro.Text+ "%'");
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Informes inf = new Informes();
            inf.TipoReporte1 = "Productos";
            inf.Show();
        }
    }
}
