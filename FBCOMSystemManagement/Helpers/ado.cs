using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace FBCOMSystemManagement.Helpers
{
    public class ado
    {
        public SqlCommand cmd;
        public SqlConnection cnx = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=LocalDBFBCCOM;Trusted_Connection=True;MultipleActiveResultSets=true");
        public void connecter_bd()
        {

            if (cnx.State == ConnectionState.Closed)
            {
                cnx.Open();
            }
        }
        public void deconnecter_bd()
        {
            if (cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }
    }
}
