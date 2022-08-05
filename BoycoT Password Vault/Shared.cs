using static BoycoT_Password_Vault.Settings;

namespace BoycoT_Password_Vault
{
    static class Shared
    {
        internal static string SettingsFile;
        internal static string DbUserTableName;

        internal static  AbstractDatabase GetDatabase()
        {
            AbstractDatabase db = null;
            switch (settings.dbType)
            {
                case 0:
                    db = new JsonDatabase();
                    db.ConnectionString = settings.dbServer;
                    break;
                case 1:
                    db = new SqlServerDatabase();
                    db.ConnectionString = settings.dbConnStr;
                    break;
                case 2:
                    db = new MySqlDatabase();
                    db.ConnectionString = settings.dbConnStr;
                    break;
                case 3:
                    db = new SQLiteDatabase();
                    db.ConnectionString = settings.dbConnStr;
                    break;
                case 4:
                    db = new XMLDatabase();
                    db.ConnectionString = settings.dbServer;
                    break;
            }
            return db;
        }
    }
}
