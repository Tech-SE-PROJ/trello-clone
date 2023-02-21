using Microsoft.Data.SqlClient;

namespace trello_clone.Server
{
    public static class LocalServerConnection
    {
        public static string ConnectionString => GetConnectionString();
        public static SqlConnection Connection => new SqlConnection(GetConnectionString());
        private static string GetConnectionString()
        {
            var connStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "Broxton-Laptop",
                InitialCatalog = "trello_clone_DB",
                IntegratedSecurity = true,
                TrustServerCertificate = true,
            };

            return connStringBuilder.ConnectionString;
        }
    }
}
