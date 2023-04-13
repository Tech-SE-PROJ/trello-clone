using Microsoft.Data.SqlClient;

namespace trello_clone.Server
{
    public static class LocalServerConnection
    {
        public static string ConnectionString => GetConnectionString();
        public static SqlConnection Connection => new SqlConnection(GetConnectionString());
        private static string GetConnectionString()
        {
            //var andrewDS = "XYLA-DEMON\\SQLEXPRESS";
            //var broxtonDS = "BROXTON-LAPTOP";

            var connStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "XYLA-DEMON\\SQLEXPRESS",
                InitialCatalog = "trello_clone_DB",
                IntegratedSecurity = true,
                TrustServerCertificate = true,
            };

            return connStringBuilder.ConnectionString;
        }
    }
}
