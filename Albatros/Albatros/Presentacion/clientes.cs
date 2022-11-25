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
using System.Data.SqlClient;

namespace Albatros.Presentacion
{
    public partial class clientes : Form
    {
        //Clases 
        clsConexionBD bd = new clsConexionBD();
        clsValidaciones val = new clsValidaciones();
        ClsClientes Cli = new ClsClientes();

        //Variables 
        string RTN = "";
        int ID = 0;
        string Sexo = "Masculino";

        /////////////////////////// VARIABLES PARA VALIDACIÓN RTN ////////////////////////////////////////////        
        char[] rtnA = new char[14];
        int D;
        int M;
        int A;
        string Departamento;
        string Municipio;
        string año;
        bool Valido = true;


        void mensaje()
        {
            MessageBox.Show("Error verifique los segundos dos digitos del RTN", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtRTN.Focus();
            txtRTN.SelectionStart = 2;
            txtRTN.SelectionLength = 2;
            Valido = false;
        }
        void ValidacionGeneral()
        {
            if (rtnA.Length >= 2)
            {
                Departamento = rtnA[0].ToString() + rtnA[1].ToString();
                D = Convert.ToInt32(Departamento);

                if (D >= 19 || D <= 0)
                {
                    MessageBox.Show("Error verifique los primeros dos digitos del RTN", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Valido = false;
                    txtRTN.Focus();
                    txtRTN.SelectionStart = 0;
                    txtRTN.SelectionLength = 2;
                }
            }

            if (rtnA.Length >= 4)
            {
                Municipio = rtnA[2].ToString() + rtnA[3].ToString();
                M = Convert.ToInt32(Municipio);
            }

            if (rtnA.Length >= 8)
            {
                año = rtnA[4].ToString() + rtnA[5].ToString() + rtnA[6].ToString() + rtnA[7].ToString();
                A = Convert.ToInt32(año);

                if (A <= 1922 || A >= 2008)
                {
                    MessageBox.Show("Error verifique los segundos 4 digitos del RTN", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRTN.Focus();
                    txtRTN.SelectionStart = 4;
                    txtRTN.SelectionLength = 4;
                    Valido = false;
                }
            }
            switch (D)
            {
                case 01:
                    if (M >= 9 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 02:
                    if (M >= 11 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 03:
                    if (M >= 22 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 04:
                    if (M >= 24 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 05:
                    if (M >= 13 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 06:
                    if (M >= 17 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 07:
                    if (M >= 20 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 08:
                    if (M >= 29 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 09:
                    if (M >= 7 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 10:
                    if (M >= 18 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 11:
                    if (M >= 5 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 12:
                    if (M >= 20 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 13:
                    if (M >= 22 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 14:
                    if (M >= 17 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 15:
                    if (M >= 24 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 16:
                    if (M >= 29 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 17:
                    if (M >= 10 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
                case 18:
                    if (M >= 12 || M <= 0)
                    {
                        mensaje();
                    }
                    break;
            }
        }
        public clientes()
        {
            InitializeComponent();
        }

        private void clientes_Load(object sender, EventArgs e)
        {
            Cli.DatosClientes(dgv);

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Letras(e, txtNombre);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ValidacionGeneral();

            if (btnAgregar.Text == "Editar")
            {
                if(txtNombre.Text == "" || txtRTN.Text == "" || txtDireccion.Text == "")
                {
                    MessageBox.Show("Seleccione un dato de la tabla para editar", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        if (txtNombre.Text != "" && txtRTN.Text != "" && txtDireccion.Text != "")
                        {
                            //Actualizamos los datos
                            Cli.DatosClientes(txtNombre.Text, txtRTN.Text, txtDireccion.Text, Sexo, txtRTN.Text);
                            MessageBox.Show("se Edito correctactamente el cliente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //actulizamos el datagridView
                            Cli.DatosClientes(dgv);
                            //Limpiamos todo el formulario
                            Limpiar();
                        }
                        else
                        {
                            MessageBox.Show("Datos vacios llene los datos", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se han podido Agregar los datos de Proveedores" + ex.ToString());
                        bd.CerrarConexion();
                    }
                }
            }
            else
            {
                RTN = Cli.ValidarRTN(txtRTN.Text);
                if (RTN == txtRTN.Text)
                {
                    MessageBox.Show("Ya existe una persona con este RTN", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        if (txtNombre.Text != "" && txtRTN.Text != "" && txtDireccion.Text != "")
                        {
                            //Agregamos el cliente
                            Cli.DatosClientes(txtNombre.Text, txtRTN.Text, txtDireccion.Text, Sexo);
                            MessageBox.Show("Los Datos Se Agregaron Correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //actulizamos el datagridView
                            Cli.DatosClientes(dgv);
                            //Limpiamos todo el formulario
                            Limpiar();
                            bd.CerrarConexion();
                        }
                        else
                        {
                            MessageBox.Show("Datos vacios llene los datos", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se han podido Agregar los datos de Proveedores" + ex.ToString());
                        bd.CerrarConexion();
                    }
                }                
            }
        }
           
        public void Limpiar()
        {
            txtNombre.Clear();
            txtRTN.Clear();
            txtDireccion.Clear();
            btnAgregar.Text = "Agregar";
            rbAgregar.Checked = true;
            txtRTN.ReadOnly = false;
            rbAgregar.Enabled = true;
            rbEditar.Checked = false;
        }

        private void rbAgregar_CheckedChanged(object sender, EventArgs e)
        {
            if(rbAgregar.Checked == true)
            {
                btnAgregar.Text = "Agregar";
            }
        }

        private void rbEditar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEditar.Checked == true)
            {
                btnAgregar.Text = "Editar";
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Informes infor = new Informes();
            infor.TipoReporte1 = "Clientes";
            infor.Show();                    
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                rbEditar.Enabled = true;
                btnAgregar.Enabled = false;
            }
            else
            {
                btnAgregar.Enabled = true;
                rbEditar.Enabled = false;
                
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = dgv.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            txtRTN.Text = dgv.Rows[e.RowIndex].Cells["RTN"].Value.ToString();
            txtDireccion.Text = dgv.Rows[e.RowIndex].Cells["Direccion"].Value.ToString();
            ID = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ID"].Value.ToString());
            rbAgregar.Enabled = false;
            rbEditar.Checked = true;
            btnAgregar.Text = "Editar";
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.LetrasNumeros(e, txtDireccion);
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

        private void txtRTN_TextChanged(object sender, EventArgs e)
        {
            rtnA = txtRTN.Text.ToCharArray();
        }

        private void rbMasculino_CheckedChanged(object sender, EventArgs e)
        {
            if(rbMasculino.Checked == true)
            {
                Sexo = "Masculino";
            }
        }

        private void rbFemenino_CheckedChanged(object sender, EventArgs e)
        {
            Sexo = "Femenino";
        }

        private void dgv_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = dgv.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
            txtRTN.Text = dgv.Rows[e.RowIndex].Cells["RTN"].Value.ToString();
            txtDireccion.Text = dgv.Rows[e.RowIndex].Cells["Direccion"].Value.ToString();            

            if (dgv.Rows[e.RowIndex].Cells["Sexo"].Value.ToString() == "Masculino")
            {
                rbMasculino.Checked = true;
            }
            else
            {
                rbFemenino.Checked = true;
            }

            rbAgregar.Enabled = false;
            rbEditar.Checked = true;
            btnAgregar.Text = "Editar";
            txtRTN.ReadOnly = true;

        }
    }
}
