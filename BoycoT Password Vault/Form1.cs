using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static BoycoT_Password_Vault.Settings;
using static BoycoT_Password_Vault.SystemMenu;

namespace BoycoT_Password_Vault
{
    public partial class Form1 : Form
    {
        private bool NeedsLogin = false;
        private Timer appTimeoutTimer = new Timer();

        public Form1()
        {
            InitializeComponent();

            IntPtr MenuHandle = GetSystemMenu(Handle, false);
            InsertMenu(MenuHandle, 0, MF_BYPOSITION, SHOW_SETTINGS_DIALOG, "Password Manager Settings");
            InsertMenu(MenuHandle, 1, MF_BYPOSITION, EXPORT_TO_CSV, "Export Passwords");
            InsertMenu(MenuHandle, 2, MF_BYPOSITION, ABOUT_THIS_PROGRAM, "About This Program");
            InsertMenu(MenuHandle, 3, MF_BYPOSITION | MF_SEPARATOR, 0, String.Empty);

            dataGridView1.AutoGenerateColumns = false; // without this, the CellFormatting event doesn't fire after the initial bind.
            dataGridView1.DataSource = dtCredentials.DefaultView;

            appTimeoutTimer.Tick += (object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
            AdjustAppTimer();
        }

        private void AdjustAppTimer(bool stopTimer = false)
        {
            if (settings.appTimeout > decimal.Parse("0") && WindowState != FormWindowState.Minimized && stopTimer == false)
            {
                appTimeoutTimer.Stop();
                appTimeoutTimer.Interval = (int)(settings.appTimeout * 60000);
                appTimeoutTimer.Start();
            }
            else
            {
                appTimeoutTimer.Stop();
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                switch (m.WParam.ToInt32())
                {
                    case SHOW_SETTINGS_DIALOG:
                        ShowSettingsDialog();
                        break;
                    case EXPORT_TO_CSV:
                        ExportToCSV();
                        break;
                    case ABOUT_THIS_PROGRAM:
                        System.Diagnostics.Process.Start("https://github.com/tolsen64/Password-Vault");
                        break;
                }
            }
            base.WndProc(ref m);
        }

        private bool Authenticate()
        {
            AdjustAppTimer(true);
            bool success = false;
            if (settings.userUsePIN)
            {
                using (FormPIN form = new FormPIN())
                {
                    form.ShowDialog();
                    success = form.PinCorrect;
                }
            }
            else
            {
                using (LoginForm form = new LoginForm())
                {
                    form.Text = "Credentials Please...";
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (settings.appUserID == form.id.ToHash().ToUnsecureString() && settings.appPassword == form.pwd.ToHash().ToUnsecureString())
                            success = true;
                    }
                }
            }
            AdjustAppTimer();
            return success;
        }

        private void ShowSettingsDialog()
        {
            AdjustAppTimer(true);
            using (LoginForm form = new LoginForm())
            {
                form.Text = "Credentials Please...";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (settings.appUserID == form.id.ToHash().ToUnsecureString() && settings.appPassword == form.pwd.ToHash().ToUnsecureString())
                    {
                        using (FormSettings dlg = new FormSettings())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                if (dlg.MergeSettings) MergeExistingPasswordsIntoNewDatabase();

                                dataGridView1.DataSource = null;
                                LoadSettings();
                                dataGridView1.DataSource = dtCredentials.DefaultView;
                            }
                        }
                    }
                }
            }
            AdjustAppTimer();
        }

        private void ExportToCSV()
        {
            if (Authenticate())
            {
                string ExportFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"export_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}.csv");
                using (StreamWriter sw = new StreamWriter(ExportFile, false))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        string colID = row.Cells["colID"].Value.ToString();
                        string colName = row.Cells["colName"].Value.ToString();
                        string colUserID = row.Cells["colUserID"].Value.ToString();
                        string colPassword = row.Cells["colPassword"].Value.ToString();
                        string colLink = row.Cells["colLink"].Value.ToString();

                        if (colUserID.Length > 0) colUserID = colUserID.DecryptBase64StringToText().ToUnsecureString();
                        if (colPassword.Length > 0) colPassword = colPassword.DecryptBase64StringToText().ToUnsecureString();
                        if (colLink.Length > 0) colLink = colLink.DecryptBase64StringToText().ToUnsecureString();

                        sw.WriteLine($@"""{colID}"",""{colName.Replace("\"", "\"\"")}"",""{colUserID.Replace("\"", "\"\"")}"",""{colPassword.Replace("\"", "\"\"")}"",""{colLink.Replace("\"", "\"\"")}""");
                    }
                }
                MessageBox.Show($@"Passwords have been exported to:

{ExportFile}

You should move the file to a secure location.");
            }
        }



        private void MergeExistingPasswordsIntoNewDatabase()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int colID = int.Parse(row.Cells["colID"].Value.ToString());
                string colName = row.Cells["colName"].Value.ToString();
                string colUserID = row.Cells["colUserID"].Value.ToString();
                string colPassword = row.Cells["colPassword"].Value.ToString();
                string colLink = row.Cells["colLink"].Value.ToString();

                if (colName.Length > 0) colName = colName.ToSecureString().EncryptTextToBase64String();

                dtCredentials = Shared.GetDatabase().MergePassword(colID, colName, colUserID, colPassword, colLink);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dtCredentials.DefaultView;
        }

        #region ToolBar Buttons
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AdjustAppTimer(true);
            using (FormCredential form = new FormCredential())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    int NewID = dtCredentials.NewID();
                    string CredentialName = form.CredentialName.EncryptTextToBase64String();
                    string UserID = form.UserID.EncryptTextToBase64String();
                    string Password = form.Password.EncryptTextToBase64String();
                    string Link = form.Link.EncryptTextToBase64String();
                    DataTable dt = Shared.GetDatabase().MergePassword(NewID, CredentialName, UserID, Password, Link);
                    if (dtCredentials.Columns.Count == 0)
                    {
                        dataGridView1.DataSource = null;
                        dt.Rows[0]["CredentialName"] = form.CredentialName.ToUnsecureString();
                        dtCredentials = dt;
                        dataGridView1.DataSource = dtCredentials.DefaultView;
                    }
                    else
                    {
                        dtCredentials.Rows.Add(new object[] { NewID, form.CredentialName.ToUnsecureString(), UserID, Password, Link });
                        dtCredentials.AcceptChanges();
                    }
                }
            }
            AdjustAppTimer();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            AdjustAppTimer(true);
            if (MessageBox.Show($"Delete these {dataGridView1.SelectedRows.Count} credentials?", "Delete Credentials", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                List<int> lst = new List<int>();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    lst.Add(int.Parse(row.Cells["colID"].Value.ToString()));
                    Shared.GetDatabase().DeletePassword(int.Parse(row.Cells["colID"].Value.ToString()));
                }                  
                dtCredentials.AsEnumerable().Where(r => lst.Contains(int.Parse(r["ID"].ToString()))).ToList().ForEach(r => r.Delete());
                dtCredentials.AcceptChanges();
            }
            AdjustAppTimer();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
                return;

            AdjustAppTimer(true);
            if (Authenticate())
            {
                using (FormCredential form = new FormCredential())
                {
                    using (DataGridViewRow row = dataGridView1.SelectedRows[0])
                    {
                        form.CredentialID = int.Parse(row.Cells["colID"].Value.ToString());
                        form.CredentialName = row.Cells["colName"].Value.ToString().ToSecureString();    //.DecryptBase64StringToText();
                        form.UserID = row.Cells["colUserID"].Value.ToString().DecryptBase64StringToText();
                        form.Password = row.Cells["colPassword"].Value.ToString().DecryptBase64StringToText();
                        if (row.Cells["colLink"].Value != null)
                            form.Link = row.Cells["colLink"].Value.ToString().DecryptBase64StringToText();

                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            int ID = form.CredentialID;
                            string CredentialName = form.CredentialName.EncryptTextToBase64String();
                            string UserID = form.UserID.EncryptTextToBase64String();
                            string Password = form.Password.EncryptTextToBase64String();
                            string Link = form.Link.EncryptTextToBase64String();
                            Shared.GetDatabase().MergePassword(ID, CredentialName, UserID, Password, Link);
                            DataRow dr = dtCredentials.AsEnumerable().First(r => int.Parse(r["ID"].ToString()) == ID);
                            dr["CredentialName"] = CredentialName.DecryptBase64StringToText().ToUnsecureString();
                            dr["UserID"] = UserID;
                            dr["Password"] = Password;
                            dr["Link"] = Link;
                            dtCredentials.AcceptChanges();
                        }
                    }
                }
            }
            AdjustAppTimer();
        }

        #endregion

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {
                //case "colName":
                //    e.Value = e.Value.ToString().DecryptBase64StringToText().ToUnsecureString();
                //    break;
                case "colUserID":
                    e.Value = btnShowUserID.Checked ? e.Value.ToString().DecryptBase64StringToText().ToUnsecureString() : "Copy UserID";
                    break;
                case "colPassword":
                    e.Value = btnShowPassword.Checked ? e.Value.ToString().DecryptBase64StringToText().ToUnsecureString() : "Copy Password";
                    break;
                case "colLink":
                    if (e.Value != DBNull.Value && e.Value != null && (string)e.Value != "")
                        e.Value = btnShowLink.Checked ? e.Value.ToString().DecryptBase64StringToText().ToUnsecureString() : "Open Link";
                    break;
            }
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "colUserID":
                    e.ToolTipText = "Copy UserID to Clipboard";
                    break;
                case "colPassword":
                    e.ToolTipText = "Copy Password to Clipboard"; 
                    break;
                case "colLink":
                    e.ToolTipText = "Open Link in default browser";
                    break;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AdjustAppTimer();
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    Clipboard.SetText(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().DecryptBase64StringToText().ToUnsecureString());
                }
                else if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewLinkColumn)
                {
                    Process.Start(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().DecryptBase64StringToText().ToUnsecureString());
                }
            }
        }

    private void Form1_Resize(object sender, EventArgs e)
        {
            if (NeedsLogin)
            {
                Hide();

                if (Authenticate())
                {
                    Show();

                    if (settings.appTimeout > decimal.Parse("0.00"))
                    {
                        appTimeoutTimer.Interval = (int)(settings.appTimeout * 60000);
                        appTimeoutTimer.Start();
                    }
                }
                else
                    Application.Exit();
            }
            NeedsLogin = WindowState == FormWindowState.Minimized;
            AdjustAppTimer();
        }

        private void btnsHideUnhide_CheckedChanged(object sender, EventArgs e)
        {
            AdjustAppTimer();
            ToolStripButton btn = (ToolStripButton)sender;
            if (btn.Checked)
            {
                if (Authenticate())
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dtCredentials.DefaultView;
                }
                else
                {
                    btn.Checked = false;
                }
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dtCredentials.DefaultView;
            }
        }

        SortOrder sortOrder = SortOrder.None;

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AdjustAppTimer();
            if (e.ColumnIndex == 1)
            {
                switch (sortOrder)
                {
                    case SortOrder.None:
                    case SortOrder.Descending:
                        sortOrder = SortOrder.Ascending;
                        break;
                    default:
                        sortOrder = SortOrder.Descending;
                        break;
                }

                dtCredentials.DefaultView.Sort = "CredentialName " + (sortOrder == SortOrder.Ascending ? "ASC" : "DESC");
                Debug.WriteLine(dtCredentials.DefaultView.Sort);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AdjustAppTimer();
            if (dtCredentials.Columns.Count > 0)
                dtCredentials.DefaultView.RowFilter = $"CredentialName LIKE '%{txtSearch.Text}%'";
        }
    }
}
