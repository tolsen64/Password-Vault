using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Security;
using System.Windows.Forms;
using static RijndaelAES;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace BoycoT_Password_Vault
{
    internal abstract class AbstractDatabase
    {
        internal abstract string ConnectionString { get; set; }
        internal abstract bool TestConnection();
        internal abstract void CreateTable();
        internal abstract DataTable GetPasswords();
        internal abstract DataTable DeletePassword(int ID);
        internal abstract DataTable MergePassword(int ID, string CredentialName, string UserID, string Password, string Link);
    }

    internal class JsonDatabase : AbstractDatabase
    {
        private string _connStr = "";

        internal override string ConnectionString
        {
            get {
                FileInfo fi = new FileInfo(_connStr.DecryptBase64StringToText().ToUnsecureString());
                return $"{fi.Name.Replace(fi.Extension, "")}_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}{fi.Extension}";
            }
            set => _connStr = value;
        }

        internal override void CreateTable()
        {
            if (_connStr == "")
                throw new System.Exception("Connection String is empty.");
            if (!File.Exists(ConnectionString))
                File.WriteAllText(ConnectionString, "");
        }

        internal override DataTable DeletePassword(int ID)
        {
            DataTable dt = GetPasswords();

            if (dt.Rows.Count < 2)
                dt.Rows.Clear();
            else
                dt = dt.AsEnumerable().Where(row => int.Parse(row["ID"].ToString()) != ID).CopyToDataTable();

            File.WriteAllText(ConnectionString, JsonConvert.SerializeObject(dt, Formatting.Indented));
            return dt;
        }

        internal override DataTable GetPasswords()
        {
            DataTable dt;
            if (File.Exists(ConnectionString) && new FileInfo(ConnectionString).Length > 0)
                dt = JsonConvert.DeserializeObject<DataTable>(File.ReadAllText(ConnectionString));
            else
                dt = new DataTable();

            return dt;
        }

        internal override DataTable MergePassword(int ID, string CredentialName, string UserID, string Password, string Link)
        {
            DataTable dt = GetPasswords();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("CredentialName", typeof(string));
                dt.Columns.Add("UserID", typeof(string));
                dt.Columns.Add("Password", typeof(string));
                dt.Columns.Add("Link", typeof(string));
            }
            if (dt.Rows.Count > 0 && dt.AsEnumerable().Any(row => int.Parse(row["ID"].ToString()) == ID))
            {
                DataRow row = dt.AsEnumerable().First(r => int.Parse(r["ID"].ToString()) == ID);
                row["CredentialName"] = CredentialName;
                row["UserID"] = UserID;
                row["Password"] = Password;
                row["Link"] = Link;
            }
            else
            {
                DataRow row = dt.NewRow();
                row["ID"] = ID;
                row["CredentialName"] = CredentialName;
                row["UserID"] = UserID;
                row["Password"] = Password;
                row["Link"] = Link;
                dt.Rows.Add(row);
            }
            dt.AcceptChanges();
            File.WriteAllText(ConnectionString, JsonConvert.SerializeObject(dt, Formatting.Indented));
            return dt;
        }

        internal override bool TestConnection()
        {
            if (_connStr == "")
            {
                MessageBox.Show("Connection String is empty");
                return false;
            }
            CreateTable();
            if (!File.Exists(ConnectionString))
            {
                MessageBox.Show("Unable to create json database file.");
                return false;
            }
            return true;
        }
    }

    internal class SqlServerDatabase : AbstractDatabase
    {
        private string _connStr;
        internal override string ConnectionString {
            get => _connStr.DecryptBase64StringToText().ToUnsecureString();
            set => _connStr = value; 
        }

        internal override void CreateTable()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault' AND xtype='U') CREATE TABLE T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault (ID int NOT NULL PRIMARY KEY, CredentialName varchar(512), UserID varchar(512), Password varchar(512), Link varchar(512));";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal override DataTable GetPasswords()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = $"SELECT ID, CredentialName, UserID, Password, Link FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault ORDER BY CredentialName;";
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        internal override DataTable MergePassword(int ID, string CredentialName, string UserID, string Password, string Link)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = $"MERGE T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault T " +
                                      "USING (SELECT @ID ID, @CredentialName CredentialName, @UserID UserID, @Password Password, @Link Link) S " +
                                      "ON T.ID = S.ID " +
                                      "WHEN MATCHED THEN UPDATE SET T.CredentialName = S.CredentialName, T.UserID = S.UserID, T.Password = S.Password, T.Link = S.Link " +
                                      "WHEN NOT MATCHED THEN INSERT (ID, CredentialName, UserID, Password, Link) VALUES (ID, CredentialName, UserID, Password, Link); " +
                                      $"SELECT ID, CredentialName, UserID, Password, Link FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault ORDER BY CredentialName;";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    cmd.Parameters.Add("@CredentialName", SqlDbType.VarChar, 512).Value = CredentialName;
                    cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 512).Value = UserID;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 512).Value = Password;
                    cmd.Parameters.Add("@Link", SqlDbType.VarChar, 512).Value = Link == null || Link == "" ? "" : Link;
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        internal override DataTable DeletePassword(int ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = $"DELETE FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault WHERE ID = @ID ;SELECT ID, CredentialName, UserID, Password, Link FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault ORDER BY CredentialName;";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        internal override bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Unable to connect to SQL Server. Error: {ex.Message}");
                return false;
            }
        }
    }

    internal class MySqlDatabase : AbstractDatabase
    {
        private string _connStr;
        internal override string ConnectionString
        {
            get => _connStr.DecryptBase64StringToText().ToUnsecureString();
            set => _connStr = value;
        }

        internal override void CreateTable()
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("", conn))
                {
                    cmd.CommandText = $"CREATE TABLE IF NOT EXISTS T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault (ID int NOT NULL PRIMARY KEY, CredentialName varchar(512), UserID varchar(512), Password varchar(512), Link varchar(512));";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal override DataTable GetPasswords()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("", conn))
                {
                    cmd.CommandText = $"SELECT ID, CredentialName, UserID, Password, Link FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault ORDER BY CredentialName;";
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        internal override DataTable MergePassword(int ID, string CredentialName, string UserID, string Password, string Link)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("", conn))
                {
                    cmd.CommandText = $"INSERT INTO T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault (ID, CredentialName, UserID, Password, Link) " +
                                      "VALUES (@ID, @CredentialName, @UserID, @Password, @Link) " +
                                      "ON DUPLICATE KEY UPDATE CredentialName = @CredentialName, UserID = @UserID, Password = @Password, Link = @Link; " +
                                      $"SELECT ID, CredentialName, UserID, Password, Link FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault ORDER BY CredentialName;";
                    cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = ID;
                    cmd.Parameters.Add("@CredentialName", MySqlDbType.VarChar, 512).Value = CredentialName;
                    cmd.Parameters.Add("@UserID", MySqlDbType.VarChar, 512).Value = UserID;
                    cmd.Parameters.Add("@Password", MySqlDbType.VarChar, 512).Value = Password;
                    cmd.Parameters.Add("@Link", MySqlDbType.VarChar, 512).Value = Link;
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        internal override DataTable DeletePassword(int ID)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("", conn))
                {
                    cmd.CommandText = $"DELETE FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault WHERE ID = @ID ;SELECT ID, CredentialName, UserID, Password, Link FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault ORDER BY CredentialName;";
                    cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = ID;
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        internal override bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Unable to connect to MySql Database. Error: {ex.Message}");
                return false;
            }
        }
    }

    internal class SQLiteDatabase : AbstractDatabase
    {
        private string _connStr;
        internal override string ConnectionString
        {
            get
            {
                FileInfo fi = new FileInfo(_connStr.DecryptBase64StringToText().ToUnsecureString());
                return $"{fi.Name.Replace(fi.Extension, "")}_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}{fi.Extension}";
            }
            set => _connStr = value;
        }

        internal override void CreateTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                Debug.WriteLine($"ConnStr: {ConnectionString}");
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("", conn))
                {
                    cmd.CommandText = $"CREATE TABLE IF NOT EXISTS T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault (ID int NOT NULL PRIMARY KEY ON CONFLICT REPLACE, CredentialName varchar(512), UserID varchar(512), Password varchar(512), Link varchar(512));";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal override DataTable GetPasswords()
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("", conn))
                {
                    cmd.CommandText = $"SELECT ID, CredentialName, UserID, Password, Link FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault ORDER BY CredentialName;";
                    try
                    {
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return dt;
        }

        internal override DataTable MergePassword(int ID, string CredentialName, string UserID, string Password, string Link)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("", conn))
                {
                    cmd.CommandText = $"INSERT INTO T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault (ID, CredentialName, UserID, Password, Link) VALUES (@ID, @CredentialName, @UserID, @Password, @Link); " +
                                      $"SELECT ID, CredentialName, UserID, Password, Link FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault ORDER BY CredentialName;";
                    cmd.Parameters.Add("@ID", DbType.Int32).Value = ID;
                    cmd.Parameters.Add("@CredentialName", DbType.String, 512).Value = CredentialName;
                    cmd.Parameters.Add("@UserID", DbType.String, 512).Value = UserID;
                    cmd.Parameters.Add("@Password", DbType.String, 512).Value = Password;
                    cmd.Parameters.Add("@Link", DbType.String, 512).Value = Link;
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        internal override DataTable DeletePassword(int ID)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("", conn))
                {
                    cmd.CommandText = $"DELETE FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault WHERE ID = @ID ;SELECT ID, CredentialName, UserID, Password, Link FROM T_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}_PasswordVault ORDER BY CredentialName;";
                    cmd.Parameters.Add("@ID", DbType.Int32).Value = ID;
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        internal override bool TestConnection()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS T_TEST (ID int); DROP TABLE T_TEST;", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Unable to connect to SQLite Database. Error: {ex.Message}");
                return false;
            }
        }
    }

    internal class XMLDatabase : AbstractDatabase
    {
        private string _connStr = "";

        internal override string ConnectionString
        {
            get
            {
                FileInfo fi = new FileInfo(_connStr.DecryptBase64StringToText().ToUnsecureString());
                return $"{fi.Name.Replace(fi.Extension, "")}_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}{fi.Extension}";
            }
            set => _connStr = value;
        }

        internal override void CreateTable()
        {
            if (_connStr == "")
                throw new System.Exception("Connection String is empty.");
            if (!File.Exists(ConnectionString))
                File.WriteAllText(ConnectionString, "");
        }

        internal override DataTable DeletePassword(int ID)
        {
            DataTable dt = GetPasswords();

            if (dt.Rows.Count < 2)
                dt.Rows.Clear();
            else
                dt = dt.AsEnumerable().Where(row => int.Parse(row["ID"].ToString()) != ID).CopyToDataTable();

            dt.WriteXml(ConnectionString);
            return dt;
        }

        internal override DataTable GetPasswords()
        {
            if (File.Exists(ConnectionString) && new FileInfo(ConnectionString).Length > 0)
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(ConnectionString);
                    return ds.Tables[0];
                }
                catch
                {
                    return new DataTable("PasswordVault");
                }
            }
            else
                return new DataTable("PasswordVault");
        }

        internal override DataTable MergePassword(int ID, string CredentialName, string UserID, string Password, string Link)
        {
            DataTable dt = GetPasswords();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("CredentialName", typeof(string));
                dt.Columns.Add("UserID", typeof(string));
                dt.Columns.Add("Password", typeof(string));
                dt.Columns.Add("Link", typeof(string));
            }
            if (dt.Rows.Count > 0 && dt.AsEnumerable().Any(row => int.Parse(row["ID"].ToString()) == ID))
            {
                DataRow row = dt.AsEnumerable().First(r => int.Parse(r["ID"].ToString()) == ID);
                row["CredentialName"] = CredentialName;
                row["UserID"] = UserID;
                row["Password"] = Password;
                row["Link"] = Link;
            }
            else
            {
                DataRow row = dt.NewRow();
                row["ID"] = ID;
                row["CredentialName"] = CredentialName;
                row["UserID"] = UserID;
                row["Password"] = Password;
                row["Link"] = Link;
                dt.Rows.Add(row);
            }
            dt.AcceptChanges();
            dt.WriteXml(ConnectionString);
            return dt;
        }

        internal override bool TestConnection()
        {
            if (_connStr == "")
            {
                MessageBox.Show("Connection String is empty");
                return false;
            }
            CreateTable();
            if (!File.Exists(ConnectionString))
            {
                MessageBox.Show("Unable to create xml database file.");
                return false;
            }
            return true;
        }
    }
}
