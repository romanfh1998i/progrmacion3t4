
using System.Data;
using System.Data.SqlClient;

namespace Tarea3
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=MSI ;initial catalog=tarea;integrated security=true;");
        public Form1()
        {
            InitializeComponent();
            CargarProvincia();
        }
        public void CargarDistrito(int municipioId)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT codigodistrito,nombredistrito,estatudistrito FROM Distrito where CodigoMunicipio=@CodigoMunicipio ", conn);
            cmd.Parameters.AddWithValue("CodigoMunicipio", municipioId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            DataRow fila = dt.NewRow();
            fila["nombredistrito"] = "Selecione un Distrito";
            dt.Rows.InsertAt(fila, 0);
            comboBox1.ValueMember = "codigodistrito";
            comboBox1.DisplayMember = "nombredistrito";
            comboBox1.DataSource = dt;

        }
        public void CargarProvincia()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT CodigoProvincia,NombreProvincia,EstatusProvincia FROM Provincia", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            DataRow dr = dt.NewRow();
            dr["NombreProvincia"] = "Selecione una Provincia";
            dt.Rows.InsertAt(dr, 0);
            comboBox3.ValueMember = "CodigoProvincia";
            comboBox3.DisplayMember = "NombreProvincia";
            comboBox3.DataSource = dt;

        }
        public void CargarMunicipio(int provinciaId)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand($"SELECT CodigoMunicipio,NombreMunicipio,estatusMunicipio ,CodigoProvincia From municipio where CodigoProvincia={provinciaId}", conn);
            //cmd.Parameters.AddWithValue("CodigoProvincia", provinciaId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            DataRow nw = dt.NewRow();
            nw["NombreMunicipio"] = "Selecione un municipio";
            dt.Rows.InsertAt(nw, 0);
            comboBox2.ValueMember = "CodigoMunicipio";
            comboBox2.DisplayMember = "NombreMunicipio";
            comboBox2.DataSource = dt;


        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
            comboBox2.DataSource = null;

            int.TryParse(comboBox3.SelectedValue?.ToString(), out int provinciaId);

            if (provinciaId > 0)
            {
                CargarMunicipio(provinciaId);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(comboBox2.SelectedValue?.ToString(), out int municipioId);

            if (municipioId > 0)
            {
                CargarDistrito(municipioId);
            }
        }
    }
}