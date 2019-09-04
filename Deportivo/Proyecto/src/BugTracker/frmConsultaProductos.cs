using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTracker
{
    public partial class frmConsultaProductos : Form
    {
        public frmConsultaProductos()
        {
            InitializeComponent();
        }

        private void frmConsultaProducto_Load(object sender, EventArgs e)
        {

            //LLenar combos y limpiar grid
            LlenarCombo(cboMarca, DBHelper.GetDBHelper().ConsultaSQL("Select * from  Marcas where borrado=0"), "descripcion", "id_marca");
                      
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT TOP 20 * FROM Productos WHERE 1=1 AND BORRADO=0 ";

            // Dictionary: Representa una colección de claves y valores.
            // Dictionary: Solo se usa si pasamos por parámetro los filtros de la selección de los combos
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            DateTime fechaDesde;
            DateTime fechaHasta;
            if (DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) &&
                DateTime.TryParse(txtFechaHasta.Text, out fechaHasta))
            {
                //strSql += " AND (fecha_alta>=@fechaDesde AND fecha_alta<=@fechaHasta) ";
              //  strSql += " AND (fecha_alta>="+txtFechaDesde.Text + " AND fecha_alta<=" +txtFechaHasta.Text +")" ;
                //parametros.Add("fechaDesde", txtFechaDesde.Text);
                //parametros.Add("fechaHasta", txtFechaHasta.Text);
            }


            if (!string.IsNullOrEmpty(cboMarca.Text))
            {
                var idMarca = cboMarca.SelectedValue.ToString();
                //strSql += "AND (id_marca=@idMarca) ";
                strSql += "AND (id_marca="+ idMarca +") ";
                
                //parametros.Add("idMarca", idMarca);
            }

            

            strSql += " ORDER BY id_producto DESC";
            //dgvBugs.DataSource = DBHelper.GetDBHelper().ConsultaSQLConParametros(strSql, parametros);
            dgvProductos.DataSource = DBHelper.GetDBHelper().ConsultaSQL(strSql);
            if (dgvProductos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron coincidencias para el/los filtros ingresados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
                 
        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            // Datasource: establece el origen de datos de este objeto.
            cbo.DataSource = source;
            // DisplayMember: establece la propiedad que se va a mostrar para este ListControl.
            cbo.DisplayMember = display;
            // ValueMember: establece la ruta de acceso de la propiedad que se utilizará como valor real para los elementos de ListControl.
            cbo.ValueMember = value;
            //SelectedIndex: establece el índice que especifica el elemento seleccionado actualmente.
            cbo.SelectedIndex = -1;
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbl_marca_Click(object sender, EventArgs e)
        {

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
