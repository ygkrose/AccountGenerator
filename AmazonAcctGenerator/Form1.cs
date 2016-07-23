using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
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
        IWebDriver _driver = null;
        List<UserStruct> _users = null;
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
                        //if (headlist.IndexOf("browseruseragent") < birthidx)
                        //    birthidx++;
                        string data;
                        listBox1.Items.Clear();
                        _users = new List<UserStruct>();
                        while ((data = await csvReader.ReadLineAsync()) != null)
                        {
                            UserStruct us = new UserStruct();
                            string[] splitdata = data.Split(new char[] { ',' });
                            string _email = splitdata[emailidx];
                            string _birth = splitdata[birthidx];
                            string[] bary = _birth.Split(new char[] { '/' });
                            string birthmd = bary[0].PadLeft(2, '0') + bary[1].PadLeft(2, '0');
                            listBox1.Items.Add(splitdata[0] + ":" + _email.Replace("@", birthmd + "@"));
                            us.name = _email.Split(new char[] { '@' })[0];
                            us.email = _email.Replace("@", birthmd + "@");
                            us.birthday = _birth;
                            _users.Add(us);
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

        private void btn_create_Click(object sender, EventArgs e)
        {
            EdgeOptions eo = new EdgeOptions();
            eo.PageLoadStrategy = EdgePageLoadStrategy.Normal;
            try
            {
                //_driver = new EdgeDriver(eo);
                //System.Threading.Thread.Sleep(1500);
                //_driver.Navigate().GoToUrl("about:InPrivate");
                //_driver.Url = "www.amazon.com";
                //_driver.Navigate().GoToUrl("www.amazon.com");
                //_driver.Manage().Cookies.DeleteAllCookies();
                
                ChromeOptions co = new ChromeOptions();
                co.AddArgument("-incognito");

                listBox2.Items.Clear();
                //Parallel.For(0, 5, i => {
                //    _driver = new ChromeDriver(co);
                //    _driver.Manage().Cookies.DeleteAllCookies();
                //    forceDeleteCookieFile(_driver);
                //    _driver.Navigate().GoToUrl("https://www.amazon.com/ap/signin?_encoding=UTF8&openid.assoc_handle=usflex&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.pape.max_auth_age=0&openid.return_to=https%3A%2F%2Fwww.amazon.com%2F%3Fref_%3Dnav_signin");
                //    IWebElement getreport = _driver.FindElement(By.Id("createAccountSubmit"));
                //    getreport.Click();

                //    IWebElement username = _driver.FindElement(By.Id("ap_customer_name"));
                //    username.SendKeys(_users[i].name);
                //    IWebElement useremail = _driver.FindElement(By.Id("ap_email"));
                //    useremail.SendKeys(_users[i].email);
                //    IWebElement userpwd = _driver.FindElement(By.Id("ap_password"));
                //    userpwd.SendKeys("4rfv5tgb");
                //    IWebElement userpwdchk = _driver.FindElement(By.Id("ap_password_check"));
                //    userpwdchk.SendKeys("4rfv5tgb");

                //    IWebElement creatbtn = _driver.FindElement(By.Id("continue"));
                //    //creatbtn.Click();
                //    System.Threading.Thread.Sleep(2000);
                //    listBox2.Items.Add(_users[i].email);
                //    _driver.Quit();
                //});

                for (int i = 0; i < 5; i++)
                {
                    _driver = new ChromeDriver(co);
                    _driver.Manage().Cookies.DeleteAllCookies();
                    forceDeleteCookieFile(_driver);
                    _driver.Navigate().GoToUrl("https://www.amazon.com/ap/signin?_encoding=UTF8&openid.assoc_handle=usflex&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.pape.max_auth_age=0&openid.return_to=https%3A%2F%2Fwww.amazon.com%2F%3Fref_%3Dnav_signin");
                    IWebElement getreport = _driver.FindElement(By.Id("createAccountSubmit"));
                    getreport.Click();

                    IWebElement username = _driver.FindElement(By.Id("ap_customer_name"));
                    username.SendKeys(_users[i].name);
                    IWebElement useremail = _driver.FindElement(By.Id("ap_email"));
                    useremail.SendKeys(_users[i].email);
                    IWebElement userpwd = _driver.FindElement(By.Id("ap_password"));
                    userpwd.SendKeys("4rfv5tgb");
                    IWebElement userpwdchk = _driver.FindElement(By.Id("ap_password_check"));
                    userpwdchk.SendKeys("4rfv5tgb");

                    IWebElement creatbtn = _driver.FindElement(By.Id("continue"));
                    //creatbtn.Click();
                    System.Threading.Thread.Sleep(2000);
                    listBox2.Items.Add(_users[i].email);
                    _driver.Quit();
                }


            }
            catch (Exception err)
            {
                addMsg(err.Message);
                if (_driver != null)
                {
                    _driver.Quit();
                    _driver.Dispose();
                }
            }

        }

        private void forceDeleteCookieFile(IWebDriver browsertype)
        {
            string appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (browsertype.GetType().Name.ToString() == "ChromeDriver")
            {
                appPath = appPath.Replace("Roaming", "") + @"Local\Google\Chrome\User Data\Default";
            }
            
            DirectoryInfo di = new DirectoryInfo(appPath);
            if (di.Exists)
            {
                foreach (FileInfo f in di.GetFiles("*Cookie*.*"))
                {
                    f.Delete();
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChromeOptions co = new ChromeOptions();
            co.AddArgument("-incognito");
            ChromeDriver cd = new ChromeDriver(co);
            cd.Manage().Cookies.DeleteAllCookies();
            forceDeleteCookieFile(cd);
            cd.Navigate().GoToUrl("https://www.amazon.com");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //$("div.span12.info")
            ChromeOptions co = new ChromeOptions();
            co.AddArgument("-incognito");
            ChromeDriver cd = new ChromeDriver(co);
            cd.Manage().Cookies.DeleteAllCookies();
            forceDeleteCookieFile(cd);
            cd.Navigate().GoToUrl("http://www.ccyp.com");
        }
    }
}
