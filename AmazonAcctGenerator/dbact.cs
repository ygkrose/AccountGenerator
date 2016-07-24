using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAcctGenerator
{
    public partial class Form1
    {
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=K:\VSPROJECTS\ACCOUNTGENERATOR\AMAZONACCTGENERATOR\MYDB.MDF;Integrated Security=False;User ID=amazon;Password=********;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        //private static string connstr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=K:\VSPROJECTS\ACCOUNTGENERATOR\AMAZONACCTGENERATOR\MYDB.MDF;Integrated Security=False;User ID=amazon;Password='wolf621030';Encrypt=False;ApplicationIntent=ReadWrite";
        private static string connstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=K:\VSProjects\AccountGenerator\AmazonAcctGenerator\mydb.mdf;Integrated Security=True";
        public static SqlConnection conn = new SqlConnection(connstr);
        
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

    }
}
