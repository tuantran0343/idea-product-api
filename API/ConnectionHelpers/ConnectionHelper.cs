using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API.ConnectioHelper
{
    public static class ConnectionHelper
    {
        public static DataSet SelectQueryToDataSet(string sql, Dictionary<string, dynamic> Condition, string ConnStr)
        {
            DataSet myDataSet = new DataSet();
            var connection = new SqlConnection(ConnStr);

            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                foreach (var i in Condition.Keys)
                {
                    cmd.Parameters.AddWithValue(i.ToString(), Condition[i.ToString()]);
                }

                try
                {
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(myDataSet);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            connection.Close();

            return myDataSet;
        }

        public static DataTable SelectQueryToDataTable(string sql, Dictionary<string, dynamic> Condition, string ConnStr)
        {
            DataTable myDataSet = new DataTable();
            var connection = new SqlConnection(ConnStr);

            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                foreach (var i in Condition.Keys)
                {
                    cmd.Parameters.AddWithValue(i.ToString(), Condition[i.ToString()]);
                }

                try
                {
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(myDataSet);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            connection.Close();

            return myDataSet;
        }

    }




}