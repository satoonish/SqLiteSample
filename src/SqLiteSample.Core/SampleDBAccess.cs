using System.Data.SQLite;

namespace SqLiteSample.Core
{
    public class SampleDBAccess
    {
        public string GetSQLiteVersion()
        {
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };

            using (var cn = new SQLiteConnection(sqlConnectionSb.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = "select sqlite_version()";
                    var result = cmd.ExecuteScalar();
                    return result.ToString();
                }
            }
        }
    }
}
