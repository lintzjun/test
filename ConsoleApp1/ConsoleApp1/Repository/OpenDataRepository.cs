using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XML_Analysis.Models;

namespace XML_Analysis.repositories
{
    class repository
    {
        public SqlConnection Connection()
        {
            //建立連線
            string strConn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\linja\Source\Repos\test\ConsoleApp1\ConsoleApp1\App_Data\Database1.mdf;Integrated Security=True";
            SqlConnection myConn = new SqlConnection(ConnectionString.opendataDB);
            return myConn;
        }


        public void Insert_Data(SqlConnection conn, OpenData item)
        {
            try
            {
                conn.Open();

                string sql_Insert = "  INSERT INTO [Table](工會名稱, 地址, 電話 , 傳真) VALUES ( N'" + item.工會名稱 + "',N'" + item.地址 + "',N'" + item.電話 + "',N'" + item.傳真 + "')";

                SqlCommand mySqlCmd = new SqlCommand(sql_Insert, conn);

                mySqlCmd.ExecuteNonQuery();

            }
            catch (SqlException e)
            {

            }
            finally
            {
                conn.Close();
            }
        }

        public List<OpenData> Select_All_Data(SqlConnection conn, string name)
        {

            var result = new List<OpenData>();

            conn.Open();

            var sql_command = new SqlCommand("", conn);
            // SqlCommand mySqlCmd = new SqlCommand(string, conn);
            sql_command.CommandText = string.Format(@"Select 工會名稱,地址,電話,傳真 From sOpenData");
            /*
            if (!string.IsNullOrEmpty(name))
                sql_command.CommandText = $"{sql_command.CommandText}Where Category =N'{name}'";
            */
            var reader = sql_command.ExecuteReader();


            while (reader.Read())
            {
                var item = new OpenData();
                item.工會名稱 = reader.GetString(0);
                item.地址 = reader.GetString(1);
                item.電話 = reader.GetString(2);
                item.傳真 = reader.GetString(3);
                result.Add(item);
            }

            conn.Close();
            return result;

        }

    }
}