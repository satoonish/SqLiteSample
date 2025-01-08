using System.Data.SQLite;
using System.Text;

namespace SqLiteSample.Core
{
    public class SampleDBAccess
    {
        private SQLiteConnectionStringBuilder _db;

        public SampleDBAccess()
        {
            _db = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };
        }

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

        public string CreateDB()
        {
            using (var connection = new SQLiteConnection(_db.ToString()))
            {
                connection.Open();

                // テーブル作成
                string createTableQuery = @"CREATE TABLE SampleTable (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Name TEXT NOT NULL,
                                            Age INTEGER NOT NULL
                                        );";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // データ挿入
                string insertDataQuery = @"INSERT INTO SampleTable (Name, Age) VALUES
                                       ('Alice', 25),
                                       ('Bob', 30),
                                       ('Charlie', 35);";
                using (var command = new SQLiteCommand(insertDataQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // データを取得して文字列として出力
                string selectQuery = "SELECT * FROM SampleTable;";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Id | Name    | Age");
                    sb.AppendLine("---|---------|----");

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int age = reader.GetInt32(2);

                        sb.AppendLine($"{id,2} | {name,-7} | {age,3}");
                    }

                    Console.WriteLine(sb.ToString());
                    return sb.ToString();
                }
            }
        }
    }
}
