using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace AmazonAcctGenerator
{

    public partial class Form1 : Form
    {
        IWebDriver _driver = null;
        List<UserStruct> _users = null;

        public delegate void AddListItem(String myString);
        public AddListItem Delegate_listbox1;
        public AddListItem Delegate_listbox2;
        ManualResetEvent _pauseEvent = new ManualResetEvent(true);

        private const string pwd = "4esz5rdx";
        public Form1()
        {
            InitializeComponent();
            Delegate_listbox2 = new AddListItem(AddListItemMethod);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ctlStatus(frmStatus.load);
            getCardNo();
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

        private void getCardNo()
        {
            using (SqlDataReader dr = getSqlCmd("select * from card where status=1").ExecuteReader())
            {
                while (dr.Read())
                {
                    cardpickup.Items.Add(dr["CardID"].ToString().Trim() + "," + dr["bmonth"].ToString().Trim() + "/" + dr["byear"].ToString().Trim());
                }
            }

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
                            string _email = splitdata[emailidx].Trim();
                            string _birth = splitdata[birthidx].Trim();
                            string[] bary = _birth.Split(new char[] { '/' });
                            string birthmd = bary[0].PadLeft(2, '0') + bary[1].PadLeft(2, '0');
                            listBox1.Items.Add(splitdata[0] + ":" + _email.Replace("@", birthmd + "@"));
                            us.serial = splitdata[0];
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

        private async void btn_create_Click(object sender, EventArgs e)
        {
            string msg = "";
            listBox2.Items.Clear();
            msg = await Task.Run(() => doCreatAccount());
            addMsg(msg);
        }


        private string doCreatAccount()
        {
            string rtnmsg = "";
            try
            {
                getallemail();
                
                for (int i = 0; i < _users.Count; i++)
                {
                    if (allEmail.Exists(
                        delegate (DataRow bk)
                        {
                            return bk["email"].ToString().Trim() == _users[i].email;
                        }
                        ))
                    {
                        continue;
                    }


                    _driver = newAWebDriver("chrome");

                    _driver.Navigate().GoToUrl("https://www.amazon.com/ap/signin?_encoding=UTF8&openid.assoc_handle=usflex&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.pape.max_auth_age=0&openid.return_to=https%3A%2F%2Fwww.amazon.com%2F%3Fref_%3Dnav_signin");
                    IWebElement getreport = _driver.FindElement(By.Id("createAccountSubmit"));
                    getreport.Click();

                    IWebElement username = _driver.FindElement(By.Id("ap_customer_name"));
                    username.SendKeys(_users[i].name);
                    IWebElement useremail = _driver.FindElement(By.Id("ap_email"));
                    useremail.SendKeys(_users[i].email);
                    IWebElement userpwd = _driver.FindElement(By.Id("ap_password"));
                    userpwd.SendKeys(pwd);
                    IWebElement userpwdchk = _driver.FindElement(By.Id("ap_password_check"));
                    userpwdchk.SendKeys(pwd);

                    IWebElement creatbtn = _driver.FindElement(By.Id("continue"));
                    creatbtn.Click();


                    IWebElement resultpage = _driver.FindElement(By.Id("a-page"));
                    if (resultpage != null)
                    {
rechk:
                        IReadOnlyList<IWebElement> ls = _driver.FindElements(By.Id("nav-tools"));
                        if (ls.Count == 1)
                        {
                            btn_resume.Enabled = false;
                            IWebElement navpage = _driver.FindElement(By.Id("nav-tools"));
                            if (navpage != null && navpage.GetAttribute("innerText").IndexOf("Hello") > -1)
                                this.Invoke(Delegate_listbox2, new Object[] { _users[i].serial + ":" + _users[i].email + addAccount(_users[i]) });
                        }
                        else
                        {
                            if (resultpage.GetAttribute("innerText").IndexOf("Email address already in use") > -1)
                            {
                                this.Invoke(Delegate_listbox2, new Object[] { _users[i].serial + ":" + _users[i].email + "Email address already in use" });
                            }
                            else if (resultpage.GetAttribute("innerText").IndexOf("a problem") > -1)
                            {
                                rtnmsg = ("detect robot error! Stop Running program.");
                                _pauseEvent.Reset();
                                while (true)
                                {
                                    //IWebElement _userpwd = _driver.FindElement(By.Id("ap_password"));
                                    //userpwd.SendKeys(pwd);
                                    //IWebElement _userpwdchk = _driver.FindElement(By.Id("ap_password_check"));
                                    //userpwdchk.SendKeys(pwd);
                                    btn_resume.Enabled = true;
                                    _pauseEvent.WaitOne(Timeout.Infinite);
                                    if (_pauseEvent.WaitOne(0)) break;
                                }
                                goto rechk;
                                //break;
                            }
                            else
                            {
                                rtnmsg= ("unknow error! Stop Running program.");
                                break;
                            }
                        }
                    }

                    _driver.Quit();
                }
            }
            catch (Exception err)
            {
                rtnmsg = (err.Message);
                if (_driver != null)
                {
                    _driver.Quit();
                    _driver.Dispose();
                }
            }
            return rtnmsg;
        }

        private IWebDriver newAWebDriver(string typ,int sizew=120,int sizeh=200)
        {
            if (typ == "edge")
            {
                EdgeOptions eo = new EdgeOptions();
                eo.AddAdditionalCapability("ForceCreateProcessApi", true);
                eo.AddAdditionalCapability("BrowserCommandLineArguments", "private");
                eo.PageLoadStrategy = EdgePageLoadStrategy.Normal;
                
                EdgeDriver iedrv = new EdgeDriver(eo);
                return iedrv;
            }
            else if (typ == "chrome")
            {
                ChromeOptions co = new ChromeOptions();
                co.AddArgument("-incognito");
                co.AddArgument("window-size=" + sizew.ToString() + "," + sizeh.ToString() );
                ChromeDriver cd = new ChromeDriver(co);
                cd.Manage().Cookies.DeleteAllCookies();
                forceDeleteCookieFile(cd);
                return cd;
            }
            return null;
        }

        List<DataRow> allEmail = null;
        private void getallemail()
        {
            allEmail = getColRows("account", "email").AsEnumerable().ToList();
        }

        private string addAccount(UserStruct user)
        {
            string rtn = "";
            string insSql = "insert into account values ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + user.email + "','" + pwd + "','','','','','created',0,null)";
            try
            {
                getSqlCmd(insSql).ExecuteNonQuery();
            }
            catch (Exception err)
            {
                rtn=(err.Message);
            }
            return rtn;
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
            ccypdriver = newAWebDriver("chrome") as ChromeDriver;
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
                                this.Invoke(Delegate_listbox2, new Object[] { chName + " " + enName });
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

        private void btn_chrome_Click(object sender, EventArgs e)
        {
            ChromeDriver cd = newAWebDriver("chrome",800,600) as ChromeDriver;
            cd.Navigate().GoToUrl("https://www.amazon.com");
        }

        private void btn_edge_Click(object sender, EventArgs e)
        {
            EdgeDriver iedrv = newAWebDriver("edge") as EdgeDriver;
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

  

        ChromeDriver cdbid;
        private void btn_onedollor_Click_1(object sender, EventArgs e)
        {
            cdbid = newAWebDriver("chrome",800,600) as ChromeDriver;
            cdbid.Navigate().GoToUrl("https://www.amazon.com/gp/aw/s/ref=aa_sbox_sort?fst=as%3Aoff&rh=n%3A165793011%2Cp_n_age_range%3A5442388011&bbn=165793011&sort=price-asc-rank&ie=UTF8&qid=1468937206");
        }

        shipper _currentShipper = null;
        UserStruct _currentBuyer = null;
        private void btn_fillbill_Click(object sender, EventArgs e)
        {
            if (cdbid != null)
            {
                string _pitem = cdbid.FindElementById("productDetails_detailBullets_sections1").FindElements(By.TagName("td"))[2].Text;
                //add to cart button
                IWebElement addcart = cdbid.FindElementById("add-to-cart-button");
                if (addcart == null) { addMsg("can't find add to cart btn!"); return; }
                addcart.Click();
                //process to checkout button
                IWebElement chkout = cdbid.FindElementById("hlb-ptc-btn-native");
                if (chkout==null) { addMsg("can't find process to checkout button!"); return; }
                chkout.Click();
                //sign in form
                if (cdbid.FindElementByName("signIn") != null)
                {
                    //get account from db here
                    _currentBuyer = getRndBuyer();
                    _currentBuyer.pitm = _pitem.Trim();
                    cdbid.FindElementById("ap_email").SendKeys(_currentBuyer.email);
                    cdbid.FindElementById("ap_password").SendKeys(_currentBuyer.pwd);
                    cdbid.FindElementById("signInSubmit").Click();
                }
                this.Invoke(Delegate_listbox2, new Object[] { _currentBuyer.email });
                //fill shipping data
                //if (cdbid.FindElementById("newShippingAddressFormFromIdentity")!=null)
                //{
                    //get shipping addr from db here
                    _currentShipper = getRndshipper();
                    cdbid.FindElementById("enterAddressFullName").SendKeys(_currentShipper.enname);
                    cdbid.FindElementById("enterAddressAddressLine1").SendKeys(_currentShipper.addr1);
                    cdbid.FindElementById("enterAddressAddressLine2").SendKeys(_currentShipper.addr2);
                    cdbid.FindElementById("enterAddressCity").SendKeys(_currentShipper.city);
                    cdbid.FindElementById("enterAddressStateOrRegion").SendKeys(_currentShipper.state);
                    cdbid.FindElementById("enterAddressPostalCode").SendKeys(_currentShipper.zipcode);
                    cdbid.FindElementById("enterAddressPhoneNumber").SendKeys(_currentShipper.phone);
                    cdbid.FindElementByName("shipToThisAddress").Click();
                //}
                if (cdbid.FindElementsByClassName("a-button-text").Count>0)
                    cdbid.FindElementByClassName("a-button-text").Click();
                else if (cdbid.FindElementsByClassName("a-button-input").Count > 0)
                    cdbid.FindElementByClassName("a-button-input").Click();
                System.Threading.Thread.Sleep(1500);
                //card info
                cdbid.FindElementById("ccName").SendKeys(_currentShipper.enname);

                string[] ccds = cardpickup.Text.Split(new char[] { ',' });
                cdbid.FindElementById("addCreditCardNumber").SendKeys(ccds[0].Trim());

                string[] limit = ccds[1].Split(new char[] { '/' });
                cdbid.FindElementsByClassName("a-dropdown-prompt")[0].Click();
                cdbid.FindElementsByLinkText(limit[0])[0].Click();

                //SelectElement sem = new SelectElement(cdbid.FindElementById("ccMonth"));
                //sem.SelectByText(cardpickup.Text.Split(new char[] { ',' })[1].Split(new char[] { '/' })[0]);

                cdbid.FindElementsByClassName("a-dropdown-prompt")[1].Click();
                cdbid.FindElementsByLinkText(limit[1])[0].Click();
                //SelectElement sey = new SelectElement(cdbid.FindElementById("ccYear"));
                //sey.SelectByText(cardpickup.Text.Split(new char[] { ',' })[1].Split(new char[] { '/' })[1]);

                cdbid.FindElementById("ccAddCard").Click();

                System.Threading.Thread.Sleep(2300);
                
                cdbid.FindElementById("continue-top").Click();
                //cdbid.FindElementByName("placeYourOrder1").Click();

            }
        }

        private UserStruct getRndBuyer()
        {
            DataTable tmptable = getColRows("account", "top 1 *", "pitem='' and status='created' order by NEWID()");
            if (tmptable.Rows.Count == 0) { addMsg("no suitable buyer for use"); return null; }
            UserStruct us = new UserStruct()
            {
                email = tmptable.Rows[0]["email"].ToString().Trim(),
                 pwd = tmptable.Rows[0]["pwd"].ToString().Trim()
            };
            
            return us;
        }

        private shipper getRndshipper()
        {
            DataTable tmptable = getColRows("shipping", "top 1 *", " 1=1 order by NEWID()");
            if (tmptable.Rows.Count == 0) { addMsg("no suitable shipper for use"); return null; }
            shipper ship = new shipper()
            {
                enname = tmptable.Rows[0]["enname"].ToString().Trim(),
                addr1 = tmptable.Rows[0]["addr1"].ToString().Trim(),
                addr2 = tmptable.Rows[0]["addr2"].ToString().Trim(),
                city = tmptable.Rows[0]["city"].ToString().Trim(),
                state = tmptable.Rows[0]["state"].ToString().Trim(),
                zipcode = tmptable.Rows[0]["zipcode"].ToString().Trim(),
                phone = tmptable.Rows[0]["tel"].ToString().Trim()
            };

            return ship;
        }


        DataGrid dg1 = null;
        private void tabledata_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void fillDataGrid()
        {
            if (dg1 == null)
            {
                dg1 = new DataGrid();
                dg1.ContextMenu = new ContextMenu(new MenuItem[] { new MenuItem("save", new EventHandler((o,e) => {                    
                    if (MessageBox.Show("R U Sure?","Warning", MessageBoxButtons.YesNo)== DialogResult.Yes)
                    {
                        uptTableData(dg1.CurrentRowIndex);
                    }
                }))}
                );
            }
            else
                dg1.DataSource = null;
            tableLayoutPanel1.Controls.Add(dg1);
            tableLayoutPanel1.SetRow(dg1, 4);
            tableLayoutPanel1.SetColumnSpan(dg1, 2);
            dg1.Dock = DockStyle.Fill;
            dg1.BringToFront();
            if (string.IsNullOrEmpty(tabledata.Text.Trim()))
            {
                listBox1.Visible = true;
                listBox2.Visible = true;
                dg1.SendToBack();
            }
            else
            {
                listBox1.Visible = false;
                listBox2.Visible = false;

                dg1.DataSource = getColRows(tabledata.Text, "*");
            }
        }

        private void uptTableData(int rowidx)
        {
            string uptsql = "";
            switch (tabledata.Text.Trim())
            {
                case "account":
                    uptsql = "update account set status='"+dg1[dg1.CurrentRowIndex,7].ToString().Trim()+"' where email='"+ dg1[dg1.CurrentRowIndex, 1].ToString().Trim() + "'";
                    break;
                case "card":
                    int s = dg1[dg1.CurrentRowIndex, 4].ToString() == "False" ? 1 : 0;
                    uptsql = "update card set status=" + s + " where CardId='" + dg1[dg1.CurrentRowIndex, 0].ToString().Trim() + "'";
                    break;
                default:
                    MessageBox.Show("not yet!");
                    return;
            }
            IDbCommand cmd = dapper.CreateCommand();
            if (dapper.State == ConnectionState.Closed)
                dapper.Open();
            cmd.CommandText = uptsql;
            if (cmd.ExecuteNonQuery() == 1)
            {
                addMsg("update success!");
                fillDataGrid();
            }
            else
                addMsg("update nothing!");
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
        }

        private void btn_upt_Click(object sender, EventArgs e)
        {
            if (_currentBuyer == null | _currentShipper == null) return;
            IDbCommand cmd = dapper.CreateCommand();
            if (dapper.State == ConnectionState.Closed)
                dapper.Open();
            cmd.CommandText = "update account set rcvname='" + _currentShipper.enname + "' , tel='" + _currentShipper.phone + "', pitem='" + _currentBuyer.pitm + "',cardno='" +
                cardpickup.Text.Split(new char[] { ',' })[0].Trim() + "',status='Ready',pdate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where email='" + _currentBuyer.email.Trim() + "'";
            if (cmd.ExecuteNonQuery() == 1)
            {
                addMsg("update success!");
                fillDataGrid();
            }
            else
                addMsg("update fail!");
        }

        private void btn_resume_Click(object sender, EventArgs e)
        {
            _pauseEvent.Set();
        }
    }
}
