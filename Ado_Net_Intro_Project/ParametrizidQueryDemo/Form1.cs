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
using System.Configuration;

namespace ParametrizidQueryDemo
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection = null;
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        SqlCommand sqlCommand = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("insert into Authors values(@FirstName, @LastName);", sqlConnection);
                //SqlParameter sqlParameter = new SqlParameter();
                //sqlParameter.ParameterName = "@FirstName";
                //sqlParameter.SqlDbType = SqlDbType.VarChar;
                //sqlParameter.Value = textBox1.Text;
                sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = textBox1.Text;
                sqlCommand.Parameters.AddWithValue("@LastName", textBox2.Text);

                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection?.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("getBooksCount", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@AthorId", SqlDbType.Int).Value = textBox3.Text;
                SqlParameter sqlPar= new SqlParameter("@BookCount", SqlDbType.Int);
                sqlPar.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(sqlPar);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show(sqlPar.Value.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection?.Close();
            }
        }
    }
}
