using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
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

        public delegate void AddListItem(String myString);
        public AddListItem myDelegate;

        public Form1()
        {
            InitializeComponent();
            myDelegate = new AddListItem(AddListItemMethod);
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

        public void AddListItemMethod(String myItem)
        {
            listBox2.Items.Add(myItem);
            listBox2.Update();

        }

        private void clearMsgCtrl()
        {
            msgpanel.Controls.Clear();
        }
        public void addMsg(string txt)
        {
            Label l = new Label();
            l.Text = txt;
            l.AutoSize = true;
            msgpanel.Controls.Add(l);
            l.BringToFront();
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


        ChromeDriver ccypdriver = null;
        private void btn_ccyp_Click(object sender, EventArgs e)
        {
            ChromeOptions co = new ChromeOptions();
            co.AddArgument("-incognito");
            ccypdriver = new ChromeDriver(co);
            ccypdriver.Manage().Cookies.DeleteAllCookies();
            forceDeleteCookieFile(ccypdriver);
            ccypdriver.Navigate().GoToUrl("http://www.ccyp.com");
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //$("div.span12.info")
            if (ccypdriver != null)
            {
                IReadOnlyCollection<IWebElement> elems = ccypdriver.FindElementsByCssSelector("div.span12.info");
                listBox2.Items.Clear();
                foreach (IWebElement iwe in elems)
                {
                    if (iwe.FindElements(By.TagName("h1")).Count == 2)
                    {
                        //string _html = iwe.GetAttribute("innerHTML");
                        string chName = iwe.FindElements(By.TagName("h1"))[0].Text.Trim();
                        string enName = iwe.FindElements(By.TagName("h1"))[1].Text.Trim().Replace("'","\'");
                        string addr = iwe.FindElements(By.TagName("address"))[0].Text;
                        string[] partaddr = addr.Split(new char[] { '\r' });
                        if (partaddr.Length == 3)
                        {
                            string _tel = partaddr[0].Split(new char[] { '：' })[1];
                            string[] _addr = partaddr[1].Split(new char[] { '：' })[1].Split(new char[] { ',' });
                            string _addr1 = "", _addr2 = "", _city = "", _zip = "", _state = "";
                            if (_addr.Length == 3)
                            {
                                _addr1 = _addr[0].Trim();
                                _city = _addr[1].Trim();
                                _state = _addr[2].Trim().Split(new char[] { ' ' })[0].Trim();
                                _zip = _addr[2].Trim().Split(new char[] { ' ' })[1].Trim();
                            }
                            else if (_addr.Length == 4)
                            {
                                _addr1 = _addr[0].Trim();
                                _addr2 = _addr[1].Trim();
                                _city = _addr[2].Trim();
                                _state = _addr[3].Trim().Split(new char[] { ' ' })[0].Trim();
                                _zip = _addr[3].Trim().Split(new char[] { ' ' })[1].Trim();
                            }
                            try
                            {
                                string insertsql = "insert into shipping values (N'" + chName + "', '" + enName + "', '" + _addr1 + "', '" + _addr2 + "', '" + _city + "', '" + _state + "', '" + _zip + "', '" + _tel + "')";
                                getSqlCmd(insertsql).ExecuteNonQuery();
                                this.Invoke(myDelegate, new Object[] { chName + " " + enName });
                               // listBox2.Items.Add(chName + " " + enName);
                            }
                            catch (Exception err)
                            {
                                addMsg(err.Message + Environment.NewLine);
                            }
                        }
                    }
                }
                addMsg("save DB Finish!");
            }
        }

        private void btn_onedollor_Click(object sender, EventArgs e)
        {
            ChromeOptions co = new ChromeOptions();
            co.AddArgument("-incognito");
            ChromeDriver cd = new ChromeDriver(co);
            cd.Manage().Cookies.DeleteAllCookies();
            forceDeleteCookieFile(cd);
            cd.Navigate().GoToUrl("https://www.amazon.com/gp/aw/s/ref=aa_sbox_sort?fst=as%3Aoff&rh=n%3A165793011%2Cp_n_age_range%3A5442388011&bbn=165793011&sort=price-asc-rank&ie=UTF8&qid=1468937206");
        }


        private void btn_chrome_Click(object sender, EventArgs e)
        {
            ChromeOptions co = new ChromeOptions();
            co.AddArgument("-incognito");
            ChromeDriver cd = new ChromeDriver(co);
            cd.Manage().Cookies.DeleteAllCookies();
            forceDeleteCookieFile(cd);
            cd.Navigate().GoToUrl("https://www.amazon.com");
        }

        private void btn_edge_Click(object sender, EventArgs e)
        {
            EdgeOptions eo = new EdgeOptions();
            InternetExplorerOptions opt = new InternetExplorerOptions();
            //opt.ForceCreateProcessApi = true;
            opt.BrowserCommandLineArguments = "-private";
            InternetExplorerDriver iedrv = new InternetExplorerDriver(opt);
        }

        private void btn_fillbill_Click(object sender, EventArgs e)
        {
            addMsg(DateTime.Now.ToString());
        }
    }
}
