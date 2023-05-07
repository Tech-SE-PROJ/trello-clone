using Microsoft.Data.SqlClient;

namespace trello_clone.Server
{
    public static class LocalServerConnection
    {
        public static string ConnectionString => GetConnectionString();
        public static SqlConnection Connection => new SqlConnection(GetConnectionString());
        private static string GetConnectionString()
        {
            //"XYLA-DEMON\\SQLEXPRESS";
            //"BROXTON-LAPTOP";
            
            var connStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "BROXTON-LAPTOP",
                InitialCatalog = "trello_clone_DB",
                IntegratedSecurity = true,
                TrustServerCertificate = true,
            };

            return connStringBuilder.ConnectionString;
        }
    }
}
