using pantalla_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Albatros
{
    class ClsProductos
    {
        clsConexionBD bd = new clsConexionBD();        
        SqlCommand cmd;
        SqlDataReader dr;
        
        
        ///Agregar Cliente
        public void InsertarProductos(string Codigo,string Descripcion, double Cantidad, string Precio, int Impuesto)
        {
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("insert into Productos(Codigo, Descripcion, Precio, FK_ISV) VALUES ('" + Codigo + "', '" + Descripcion + "', '" + Convert.ToDouble(Precio) + "', '" + Impuesto + "')", bd.sc);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into Inventario VALUES (" + Codigo + ", " + Cantidad + ")", bd.sc);
                cmd.ExecuteNonQuery();
                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd.CerrarConexion();
            }
        }

        ///Editar Productos
        public void EditarProducto(string Descripcion, double Precio, int Impuesto, int CodigoProducto)
        {
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("update Productos set Descripcion =  '" + Descripcion + "', Precio = '" + Precio + "', FK_ISV = '" + Impuesto + "' where Codigo = " + CodigoProducto + "", bd.sc);
                cmd.ExecuteNonQuery();
                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd.CerrarConexion();
            }
        }

        ///Eliminar Producto
        public void EliminarProducto(int Codigo)
        {
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("Delete from Productos where Codigo = " + Codigo + ")", bd.sc);
                cmd.ExecuteNonQuery();
                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd.CerrarConexion();
            }
        }
        ///cargar Clientes en datagridview
        public void DatosProductos(DataGridView dgv)
        {
            try
            {
               bd.AbrirConexion();
               bd.CualquierTabla(dgv, "Select a.Codigo, a.Descripcion, a.Precio, b.Impuesto from Productos a inner join Impuestos b on a.FK_ISV = b.ID");
               bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd.CerrarConexion();
            }
        }

        public void llenarImpuestos(ComboBox cb)
        {
            bd.LlenarCb(cb, "Impuestos", "select * from Impuestos", "Impuesto");
        }

        public int CapturarIdCb(ComboBox cb, int ID)
        {
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("SELECT ID from Impuestos where Impuesto = " + cb.Text + "", bd.sc);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ID = Convert.ToInt32(dr["ID"].ToString());
                }

                bd.CerrarConexion();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return ID;
        }

        public int CapturarCodigoProduco(string txt)
        {
            int Codigo = 0;
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("SELECT Codigo from Productos where Codigo = '" + txt + "'", bd.sc);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Codigo = Convert.ToInt32(dr["Codigo"].ToString());
                }

                bd.CerrarConexion();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd.CerrarConexion();
            }
            return Codigo;
        }        
    }
}
