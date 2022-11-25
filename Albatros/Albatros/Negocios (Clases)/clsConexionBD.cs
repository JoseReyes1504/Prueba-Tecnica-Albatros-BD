using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace pantalla_1
{
    public class clsConexionBD
    {
        public SqlConnection sc = new SqlConnection();

        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;

        protected string sql;
        protected SqlCommand cmd;
        //--------------------sql-----------------------------------
        //Conexion con la base de datos ; en el dato sorce se pone un ."punto" para que funcione en cualquier computadora la conexion
        string conexion = "Data Source=LAPTOP-H2UB6CQP\\SQLEXPRESS;Initial Catalog=Albatros;Integrated Security=True";

        //--------------------sql-----------------------------------
        //funcion para poder abrir la conexion con la base datos con el string de CONEXION
        public clsConexionBD()
        {
            sc.ConnectionString = conexion;
        }        

        //--------------------sql-----------------------------------
        // funcion que se usara en cada parte del codigo para abrir una conexion con la Base de datos
        public void AbrirConexion()
        {
            try
            {
                sc.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR en la conexion" + ex, "Error de Conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //--------------------sql-----------------------------------
        // funcion que se usara en cada parte del codigo para cerrar una conexion con la Base de datos
        public void CerrarConexion()
        {
            sc.Close();
        }

        //funcion para cargar al datagridview para cualquier tabla de la base de datos 
        public void CualquierTabla(DataGridView dgv, string Query)
        {
            try
            {
                da = new SqlDataAdapter(Query, sc);
                dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error no ha sido posible establecer conexion" + ex.ToString()); ;
            }
        }
       
        //Funcion que sirve para sistema de busqueda de Producto se implementa en Formulario De Facturación
        public void BusquedaAvanzada(TextBox Producto, ListBox lbBusqueda)
        {
            try
            {
                cmd = new SqlCommand("Select Distinct Descripcion from Productos where Descripcion like '%" + Producto.Text + "%'", sc);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lbBusqueda.Items.Add(dr["Descripcion"]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha Producido un error " + ex.ToString());
            }
        }
        ////Funcion que sirve para sistema de busqueda de Nombre del cliente y se cargar al LISTBOX
        public void BusquedaCliente(TextBox Cliente, ListBox lbBusqueda, string Operacion)
        {
            try
            {
                cmd = new SqlCommand("Select Distinct Nombre_Cliente from Clientes where " + Operacion + " like '%" + Cliente.Text + "%'", sc);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lbBusqueda.Items.Add(dr["Nombre_Cliente"]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha Producido un error " + ex.ToString());
            }
        }

        //Funcion que sirve para llenar un combobox desde los datos de la base de datos
        public void LlenarCb(ComboBox cb, string Nombre, string Query, string DatoLlenar)
        {
            cb.Items.Clear();            
            try
            {                
                AbrirConexion();
                cmd = new SqlCommand(Query, sc);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr[DatoLlenar].ToString());
                }
                dr.Close();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error no se puede llenar el combo box" + ex.ToString());
            }
        }



        //funcion para cargar los graficos del Inventario
        public void GraficoInventario(Chart Char, string Query)
        {
            ArrayList Descripcion = new ArrayList();
            ArrayList Cant = new ArrayList();

            Descripcion.Clear();
            Cant.Clear();
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(Query, sc);
                cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Descripcion.Add(dr.GetString(0));
                    Cant.Add(dr.GetValue(1));
                }
                Char.Series[0].Points.DataBindXY(Descripcion, Cant);
                dr.Close();
                CerrarConexion();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Se ha producido un error" + ex.ToString());
            }
        }
    }
}
