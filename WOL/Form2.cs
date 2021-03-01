using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WOL
{
    public partial class AddUserLink : Form
    {
        public string path = Environment.GetLogicalDrives()[0] + "Users\\" + Environment.UserName + "\\Documents\\WOL.exe_UserLink.txt";
        public AddUserLink()
        {
            InitializeComponent();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://wol.gg/");
        }
        private void AddUserLink_Load(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                this.Hide();
                var newForm = new Form1();
                newForm.ShowDialog();
                this.Close();
            }
            label4.Text = "Data will be written to: " + path;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string UserInformation = DisplayUserName.Text.Trim() + "|" + ProfileLink.Text.Trim();
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            File.WriteAllText(path, UserInformation);

            this.Hide();
            var newForm = new Form1();
            newForm.ShowDialog();
            this.Close();
        }
    }
}