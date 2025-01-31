using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BoycoT_Password_Vault
{

    internal class SettingsRoot
    {
        public decimal appTimeout { get; set; } = 0;
        public string appUserID { get; set; } = "";
        public string appPassword { get; set; } = "";
        public bool userUsePIN { get; set; } = false;
        public string userPIN { get; set; } = "";
        public int dbType { get; set; } = 0;
        public int dbPort { get; set; } = 0;
        public int dbAuthType { get; set; } = 0;
        public string dbServer { get; set; } = "";
        public string dbDatabase { get; set; } = "";
        public string dbUserID { get; set; } = "";
        public string dbPassword { get; set; } = "";
        public string dbConnStr { get; set; } = "";
    }

    internal static class Settings
    {
        internal static SettingsRoot settings = null;
        internal static DataTable dtCredentials = new DataTable();

        // LoadSettings is called from Program.cs
        internal static bool LoadSettings(bool mergeSettings = false)
        {
            if (!File.Exists(Shared.SettingsFile))
            {
                settings = new SettingsRoot();
                settings.appTimeout = 1.00M;
                settings.dbServer = $"database_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}.json";
                settings.dbType = 0;
                SaveSettings();
                return false;
            }

            settings = JsonConvert.DeserializeObject<SettingsRoot>(File.ReadAllText(Shared.SettingsFile));
            AbstractDatabase db = Shared.GetDatabase();
            Debug.WriteLine(db.ConnectionString);
            if (db.ConnectionString != "" && db.TestConnection())
            {
                db.CreateTable();
                dtCredentials = db.GetPasswords();
                foreach (DataRow dr in dtCredentials.Rows)
                    dr["CredentialName"] = dr["CredentialName"].ToString().DecryptBase64StringToText().ToUnsecureString();
            }
            else
            {
                MessageBox.Show("You will need to go into the \"Password Manager Settings\" and reconfigure the database parameters.");
            }

            return true;
        }

        internal static void SaveSettings()
        {
            File.WriteAllText(Shared.SettingsFile, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
    }
}
