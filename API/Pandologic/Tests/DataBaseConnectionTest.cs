using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace Tests
{
    [TestClass]
    public class DataBaseConnectionTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-FMJUMSNP\ELAD;Initial Catalog=PandologicDataBase;Integrated Security=True;");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Jobs", sqlConnection);
            DataTable jobsTable = new DataTable();
            sqlDataAdapter.Fill(jobsTable);

            foreach(DataRow row in jobsTable.Rows)
            {
                doSomeThing();
            }
        }

        public void doSomeThing()
        {

        } 
    }
}
