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
    public partial class Inventario : Form
    {
        clsConexionBD bd = new clsConexionBD();
        SqlCommand cmd;
        string ID;
        string QueryGraficoRecienIngresado = "select TOP 5 b.Descripcion, a.Cantidad from Inventario a inner join Productos b On a.CodigoProducto = b.Codigo order by CodigoProducto DESC";


        public Inventario()
        {
            InitializeComponent();
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            bd.CualquierTabla(dgv, "select a.CodigoProducto, b.Descripcion, a.Cantidad, b.Precio, c.Impuesto from Inventario a inner join Productos b ON a.CodigoProducto = b.Codigo inner join Impuestos c ON b.FK_ISV = c.ID");
            bd.GraficoInventario(Cprod, QueryGraficoRecienIngresado);
            
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
        }
    }
}
