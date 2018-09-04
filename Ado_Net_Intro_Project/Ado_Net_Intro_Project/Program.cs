using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Ado_Net_Intro_Project
{
    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        static SqlConnection sqlConnection = null;
        static SqlCommand sqlCommand = null;
        static SqlDataReader reader = null;
        static void Main(string[] args)
        {
            //InsertToAuthors();
            SelectFromAuthors();
            Console.ReadKey();  
        }

        private static void SelectFromAuthors()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand("select * from books;select * from authors", sqlConnection);

            int line = 0;
            try
            {
                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(reader.GetName(i) + "\t");
                            }
                            Console.WriteLine();
                            line++;
                        }
                        Console.WriteLine($"{reader[0]} {reader[0]} {reader.GetString(2)}");
                    }
                    line = 0;
                } while (reader.NextResult());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void InsertToAuthors()
        {
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("insert into Authors values('Mikhail', 'Shaphutinsky');", sqlConnection);
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
    }
}
