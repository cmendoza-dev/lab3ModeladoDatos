using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3ModeladoDatos
{
    public partial class Form2 : Form
    {
        private SqlConnection _connection;
        private string _tableName;

        public Form2(SqlConnection connection, string tableName)
        {
            InitializeComponent();
            _connection = connection;
            _tableName = tableName;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtProducto.TextChanged += txtProducto_TextChanged;
        }
        void CargarDatos()

        {
            try
            {
                //string query = $"SELECT * FROM {_tableName}";
                string query = $"select * from {_tableName}";
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "productos");
                    dataGridView1.DataSource = ds.Tables["productos"];
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtProducto.Text.Trim();

            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.cnx))
                {
                    cn.Open();
                    string query = "SELECT * FROM producto WHERE NombreProducto LIKE @searchText";
                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    da.SelectCommand.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    DataSet ds = new DataSet();
                    da.Fill(ds, "productos");

                    dataGridView1.DataSource = ds.Tables["productos"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar productos: " + ex.Message);
            }
        }
    

    private void btnTodos_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        //    string searchText = txtProducto.Text.Trim();

        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.cnx))
        //        {
        //            cn.Open();
        //            string query = "SELECT * FROM producto WHERE NombreProducto LIKE @searchText";
        //            SqlDataAdapter da = new SqlDataAdapter(query, cn);
        //            da.SelectCommand.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

        //            DataSet ds = new DataSet();
        //            da.Fill(ds, "productos");

        //            dataGridView1.DataSource = ds.Tables["productos"];
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al buscar productos: " + ex.Message);
        //    }
        //}


    }
}
