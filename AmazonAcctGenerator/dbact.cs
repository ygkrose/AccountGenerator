using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;


namespace AmazonAcctGenerator
{
    public partial class Form1
    {
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=K:\VSPROJECTS\ACCOUNTGENERATOR\AMAZONACCTGENERATOR\MYDB.MDF;Integrated Security=False;User ID=amazon;Password=********;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        //private static string connstr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=K:\VSPROJECTS\ACCOUNTGENERATOR\AMAZONACCTGENERATOR\MYDB.MDF;Integrated Security=False;User ID=amazon;Password='wolf621030';Encrypt=False;ApplicationIntent=ReadWrite";
        private static string connstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VSProjects\AccountGenerator\AmazonAcctGenerator\mydb.mdf;Integrated Security=True";
        public static SqlConnection conn = new SqlConnection(connstr);
        public static IDbConnection dapper = new SqlConnection(connstr);
        //static MongoClient client = new MongoClient("mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct");
        public static SqlCommand getSqlCmd(string sqltxt)
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                return new SqlCommand(sqltxt, conn);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static DataTable getColRows(string tablename,string colname,string condition="")
        {
            DataTable rtn = new DataTable();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                if (!string.IsNullOrEmpty(condition))
                {
                    condition = " where " + condition;
                }
                SqlDataAdapter sdp = new SqlDataAdapter("select " + colname + " from " + tablename + condition, conn);
                sdp.Fill(rtn);
            }
            catch (Exception err)
            {
                throw err;
            }

            return rtn;
        }

    }
}
