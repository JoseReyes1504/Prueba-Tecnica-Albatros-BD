using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pantalla_1;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Albatros
{
    class ClsFacturas
    {
        clsConexionBD bd = new clsConexionBD();
        SqlCommand cmd;
        SqlDataReader dr;
        
        public void LlenarCombox(ComboBox cb)
        {
            bd.LlenarCb(cb, "Cliente", "Select * From Clientes", "Nombre");
        }

        public int CapturarIdCb(ComboBox cb)
        {
            int ID = 0;
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("SELECT ID from Clientes where Nombre like '%" + cb.Text + "%'", bd.sc);
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

        public void Llenardgv(DataGridView dgv, int CodigoFactura)
        {
            try
            {
                bd.AbrirConexion();
                bd.CualquierTabla(dgv, "select b.ID, c.Codigo ,c.Descripcion, b.Cantidad, c.Precio, d.Impuesto, SUM((d.Impuesto / 100)* c.Precio)[Impuesto_Producto] ,SUM(b.Cantidad * c.Precio)[Total] from Factura a inner join DetalleFactura b ON a.ID = b.CodigoFact inner join Productos c ON b.CodigoProducto = c.Codigo inner join Impuestos d ON d.ID = c.FK_ISV where b.CodigoFact = " + CodigoFactura + " group by b.ID, c.Codigo, c.Descripcion, b.Cantidad, c.Precio, d.Impuesto");
                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ActulizarFactura(double TotalImpuesto, double Total, int CodigoFac)
        {
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("update Factura set TotalImpuesto = " + TotalImpuesto + "Total = " + Total+ " where ID = " + CodigoFac +"");
                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void CargarFacturas(DataGridView dgv)
        {
            try
            {
                bd.AbrirConexion();
                bd.CualquierTabla(dgv, "Select a.ID, b.Nombre [Cliente], a.TotalImpuesto, a.Total from Factura a inner join Clientes b ON a.CodigoCliente = b.ID");
                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void LlenarListBox(TextBox txt, ListBox lbProductos)
        {
            try
            {
                bd.AbrirConexion();

                if(txt.Text == "")
                {
                    lbProductos.Items.Clear();
                }
                else
                {
                    lbProductos.Items.Clear();
                    bd.BusquedaAvanzada(txt, lbProductos);
                }
                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha Producido un error " + ex.ToString());
                bd.CerrarConexion();
            }
        }

        public void SeleccionarDeListBox(ListBox lbProductos, TextBox txt1, TextBox txt2, TextBox txt3, TextBox txt4, Label Codigo, Label CodigoISV)
        {
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("Select a.Codigo, a.Descripcion, a.Precio, b.Impuesto, b.ID from Productos a inner join Impuestos b ON a.FK_ISV = b.ID where Descripcion = '" + lbProductos.SelectedItem.ToString() + "'", bd.sc);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Codigo.Text = dr["Codigo"].ToString();
                    CodigoISV.Text = dr["ID"].ToString();
                    txt1.Text = dr["Descripcion"].ToString();
                    txt2.Text = dr["Precio"].ToString();
                    txt3.Text = dr["Impuesto"].ToString();
                }
                else
                {
                    MessageBox.Show("No existe ningun Registo", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
                bd.CerrarConexion();
                dr.Close();
                txt4.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo llenar los campos" + ex.ToString());
                bd.CerrarConexion();
            }
            txt4.Focus();
        }


        public void CrearFactura( string Fecha, double Impuesto, double Total, int Cliente)
        {
            try
            {
                bd.AbrirConexion();
                // Se Crea La Factura
                cmd = new SqlCommand("insert into Factura VALUES ('" + Fecha + "', '" + Impuesto + "', '" + Total + "', '" + Cliente + "')", bd.sc);
                cmd.ExecuteNonQuery();                
                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd.CerrarConexion();
            }
        }


        public int ObtenerUltimaFactura(string Tabla)
        {
            int ID = 0;
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("select top 1 ID from " + Tabla +" order by ID DESC", bd.sc);
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
                bd.CerrarConexion();
            }
            return ID;
        }

        public void InsertarProductosDetalle(int CodigoProducto, int Cantidad, int CodigoFact)
        {
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("insert into DetalleFactura VALUES (" + Cantidad + ", " + CodigoProducto + ", " + CodigoFact + ")", bd.sc);
                cmd.ExecuteNonQuery();
                bd.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public double ObtenerTotalImpuesto(int CodigoFac)
        {
            double Total = 0;
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("select SUM((d.Impuesto / 100)* c.Precio)[Impuesto_Producto] from Factura a inner join DetalleFactura b ON a.ID = b.CodigoFact inner join Productos c ON b.CodigoProducto = c.Codigo inner join Impuestos d ON d.ID = c.FK_ISV where b.CodigoFact = "+ CodigoFac + "", bd.sc);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Total = Convert.ToDouble(dr["Impuesto_Producto"].ToString());
                }

                bd.CerrarConexion();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd.CerrarConexion();
            }
            return Total;
        }


        public double ObtenerTotalFactura(int CodigoFact)
        {
            double Total = 0;
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("select SUM(b.Cantidad * c.Precio)[Total_Factura] from Factura a inner join DetalleFactura b ON a.ID = b.CodigoFact inner join Productos c ON b.CodigoProducto = c.Codigo inner join Impuestos d ON d.ID = c.FK_ISV where b.CodigoFact = "+ CodigoFact + "", bd.sc);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Total = Convert.ToDouble(dr["Total_Factura"].ToString());
                }

                bd.CerrarConexion();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error  " + ex.ToString(), "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd.CerrarConexion();
            }
            return Total;
        }
    }
}
