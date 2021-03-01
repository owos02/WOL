using System;
using System.Windows.Forms;
using System.IO;

namespace WOL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public bool set = true;
        public bool load = true;
        public bool autoretry = true;
        public string path = Environment.GetLogicalDrives()[0]+"Users\\"+ Environment.UserName+"\\Documents\\WOL.exe_UserLink.txt";
        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.HtmlDocument document = this.webBrowser1.Document;
            try
            {
                autoretry = false;
                set = false;
                listBox1.Items.Clear();
                listBox1.Items.Add("Level: " + document.GetElementById("level").InnerText.Split(' ')[1]);
                listBox1.Items.Add("Hours: " + document.GetElementById("time-hours").InnerText.Split('h')[0]);
                listBox1.Items.Add("Minutes: " + document.GetElementById("time-minutes").InnerText.Split('m')[0]);
                listBox1.Items.Add("Days: " + document.GetElementById("time-days").InnerText.Split('d')[0]);
                LoadText.Text = "Reload";
            }
            catch (NullReferenceException)
            {
                autoretry = true;
                listBox1.Items.Add("Site is still");
                listBox1.Items.Add("Loading.");
                listBox1.Items.Add("Please Wait.");
                LoadText.Text = "[Retries Automatically]";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (set)
            {
                string filecontent = File.ReadAllText(path);
                string username = filecontent.Split('|')[0];
                string userlink = filecontent.Split('|')[1];

                try
                {
                    webBrowser1.Url = new Uri(userlink);
                }
                catch (UriFormatException)
                {
                    MessageBox.Show("The provided User-Link is not a URL");
                    throw;
                }
                UserName.Text = username;

                LoadText.PerformClick();
                set = false;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (load)
            {
                load = false;
            }
        }
        private void UserName_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(File.ReadAllText(path).Split('|')[1]) ;
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (autoretry)
            {
                LoadText.PerformClick();
            }   
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            File.Delete(path);   
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string filecontent = File.ReadAllText(path);
            string username = filecontent.Split('|')[0];
            string userlink = filecontent.Split('|')[1];
            UserName.Text = username;
        }
    }
}
