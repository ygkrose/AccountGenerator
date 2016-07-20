using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmazonAcctGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ctlStatus(frmStatus.load);
        }

        private void ctlStatus(frmStatus fs)
        {
            //if (fs == frmStatus.load)
            //{
            //    btn_open.Enabled = true;
            //    btn_create.Enabled = false;
            //    btn_export.Enabled = false;
            //}
        }

        private async void btn_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = "c:";
            of.DefaultExt = "csv";
            of.AddExtension = true;
            try
            {
                if (of.ShowDialog() == DialogResult.OK)
                {
                    clearMsgCtrl();
                    using (StreamReader csvReader = new StreamReader(of.OpenFile()))
                    {
                        //first line deteced
                        string headerline = csvReader.ReadLine().ToLower();
                        List<string> headlist = new List<string>(headerline.Split(new char[] { ',' }));
                        int emailidx = headlist.IndexOf("emailaddress");
                        int birthidx = headlist.IndexOf("birthday");
                        if (headlist.IndexOf("browseruseragent") < birthidx)
                            birthidx++;
                        string data ;
                        while ((data = await csvReader.ReadLineAsync())!=null)
                        {
                            string _email = data.Split(new char[] { ',' })[emailidx];
                            string _birth = data.Split(new char[] { ',' })[birthidx];
                            string[] bary = _birth.Split(new char[] { '/' });
                            string birthmd = string.Format("00", bary[0]) + string.Format("00",bary[1]);
                        }
                    }
                    
                }
            }
            catch (Exception err)
            {
                clearMsgCtrl();
                addMsg(err.Message);
            }
        }


        private void clearMsgCtrl()
        {
            msgpanel.Controls.Clear();
        }
        private void addMsg(string txt)
        {
            Label l = new Label();
            l.Text = txt;
            l.AutoSize = true;
            msgpanel.Controls.Add(l);
        }
    }
}
