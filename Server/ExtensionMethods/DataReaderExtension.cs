using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace trello_clone.Server.ExtensionMethods
{
    public static class DataReaderExtension
    {
        public static string? GetSafeString(this SqlDataReader reader, string columnName)
        {
            if(!reader.IsDBNull(columnName)) return reader.GetString(columnName);

            return null;
        }

        public static Guid? GetSafeGuid(this SqlDataReader reader, string columnName)
        {
            if (!reader.IsDBNull(columnName)) return reader.GetGuid(columnName);
            
            return null;
        }

        public static int? GetSafeInt(this SqlDataReader reader, string columnName)
        {
            if (!reader.IsDBNull(columnName)) return reader.GetInt32(columnName);

            return null;
        }

        public static DateTime? GetSafeDateTime(this SqlDataReader reader, string columnName)
        {
            if (!reader.IsDBNull(columnName)) return reader.GetDateTime(columnName);

            return null;
        }
    }
}
