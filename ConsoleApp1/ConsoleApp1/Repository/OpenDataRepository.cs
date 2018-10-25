using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace OpenData.repository
{
    class Repositorys
    {
        public SqlConnection Connection()
        {
            //建立連線
            string strConn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\linja\Source\Repos\test\ConsoleApp1\ConsoleApp1\App_Data\Database1.mdf;Integrated Security=True";
            SqlConnection myConn = new SqlConnection(strConn);
            return myConn;
        }

        public void Insert_Data_SQL(SqlConnection conn, OpenData item)
        {
            conn.Open();

            string sql_Insert = "  INSERT INTO STable(工會名稱, 地址, 電話 , 傳真) VALUES ( N'" + item.工會名稱 + "',N'" + item.地址 + "',N'" + item.電話 + "',N'" + item.傳真 + "')";

            SqlCommand mySqlCmd = new SqlCommand(sql_Insert, conn);

            mySqlCmd.ExecuteNonQuery();

            conn.Close();
        }

    }
}