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
    class ClsClientes
    {
        clsConexionBD bd = new clsConexionBD();
        clsValidaciones val = new clsValidaciones();
        SqlCommand cmd;
        SqlDataReader dr;

        /// Sobrecargar de datos o Polimorfismo 
        
        ///Editar Cliente
        public void DatosClientes(string Nombre, string RTN, string Direccion, string Sexo,string ID)
        {
            bd.AbrirConexion();
            cmd = new SqlCommand("update Clientes set Nombre = '" + Nombre + "', RTN = '" + RTN + "', Direccion = '" + Direccion + "', Sexo = '" + Sexo + "' where RTN = " + ID + "", bd.sc);
            cmd.ExecuteNonQuery();            
            bd.CerrarConexion();
        }
        ///Agregar Cliente
        public void DatosClientes(string Nombre, string RTN, string Direccion, string Sexo)
        {
            bd.AbrirConexion();
            cmd = new SqlCommand("insert into Clientes(Nombre, RTN, Direccion, Sexo) VALUES ('" +Nombre + "', '" + RTN + "', '" + Direccion + "', '" + Sexo + "')", bd.sc);
            cmd.ExecuteNonQuery();
            bd.CerrarConexion();
        }
        ///cargar Clientes en datagridview
        public void DatosClientes(DataGridView dgv)
        {
            bd.AbrirConexion();
            bd.CualquierTabla(dgv, "Select * from Clientes");
            bd.CerrarConexion();
        }


        public string ValidarRTN(string txt)
        {
            string Codigo = "";
            try
            {
                bd.AbrirConexion();
                cmd = new SqlCommand("SELECT RTN from Clientes where RTN = '" + txt + "'", bd.sc);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Codigo = dr["RTN"].ToString();
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
