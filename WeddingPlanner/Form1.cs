using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Services;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Diagnostics;


namespace WeddingPlanner
{
    public enum smsType
    {
        Invitation,
        SMS
    }

    public partial class Form1 : Form
    {
        private static Excel._Application xlApp = null;
        private static Excel.Workbook xlWorkBook = null;
        private static Excel.Worksheet xlWorkSheet = null;
        private static Excel.Range last = null;
        private static Excel.Range range = null;
        private int lastUsedRow;
        private int lastUsedColumn;
        public GuestList ListOfGuest;
        BackgroundWorker SmsWork;
        BackgroundWorker ReadExcellWork;

        public string SmsServerUrl = string.Empty;
        public bool UseSortUrl = true;
        public readonly string SMS_STRING = "לאישור הגעה לחתונה של מור ואייל לחץ כאן";

        public bool debug = true;

        public smsType SmsType;
        public string smsText = string.Empty;
        public BindingSource bsrc;
        
        public Form1()
        {
            InitializeComponent();
            ListOfGuest = new GuestList();
            SmsWork = new BackgroundWorker();
            SmsWork.WorkerReportsProgress = true;
            SmsWork.DoWork += SmsWork_DoWork;
            SmsWork.ProgressChanged +=SmsWork_ProgressChanged;
            SmsWork.RunWorkerCompleted += SmsWork_RunWorkerCompleted;

            ReadExcellWork = new BackgroundWorker();
            ReadExcellWork.WorkerReportsProgress = true;
            ReadExcellWork.DoWork += ReadExcellWork_DoWork;
            ReadExcellWork.ProgressChanged += ReadExcellWork_ProgressChanged;
            ReadExcellWork.RunWorkerCompleted += ReadExcellWork_RunWorkerCompleted;

            //DTrace.AddListener(new DTraceListenerTextBox(OutputBox, DebugLevel.GeneralMsg | DebugLevel.GoodMsg | DebugLevel.VisualDSPMsg | DebugLevel.GeneralError));
            DTrace.AddListener(new DTraceListenerFile(DebugLevel.All));

            SmsType = tabControl1.SelectedTab.Text == smsType.Invitation.ToString() ? smsType.Invitation: smsType.SMS;

            if (debug)
                checkBox_shortUrl.Checked = false;

            BuildDataGridView();
        
        }


        private void BuildDataGridView()
        {
            dataGridView_Guest.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn   col0 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn   col1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn   col2 = new DataGridViewTextBoxColumn();
            DataGridViewCheckBoxColumn  col3 = new DataGridViewCheckBoxColumn();
            DataGridViewLinkColumn      col4 = new DataGridViewLinkColumn();
            DataGridViewLinkColumn      col5 = new DataGridViewLinkColumn();
            //DataGridViewTextBoxColumn   col5 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn   col6 = new DataGridViewTextBoxColumn();
            DataGridViewCheckBoxColumn  col7 = new DataGridViewCheckBoxColumn();

            col0.DataPropertyName = "name";
            dataGridView_Guest.Columns.Add(col0);
            dataGridView_Guest.Columns[0].Name = "name";

            col1.DataPropertyName = "phone";
            dataGridView_Guest.Columns.Add(col1);
            dataGridView_Guest.Columns[1].Name = "phone";

            col2.DataPropertyName = "NumOfGuest";
            dataGridView_Guest.Columns.Add(col2);
            dataGridView_Guest.Columns[2].Name = "NumOfGuest";

            col3.DataPropertyName = "SendMessage";
            dataGridView_Guest.Columns.Add(col3);
            dataGridView_Guest.Columns[3].Name = "SendMessage";

            col4.DataPropertyName = "LongUrl";
            dataGridView_Guest.Columns.Add(col4);
            dataGridView_Guest.Columns[4].Name = "LongUrl";

            col5.DataPropertyName = "ShortUrl";
            dataGridView_Guest.Columns.Add(col5);
            dataGridView_Guest.Columns[5].Name = "ShortUrl";

            col6.DataPropertyName = "SmsMasage";
            dataGridView_Guest.Columns.Add(col6);
            dataGridView_Guest.Columns[6].Name = "SmsMasage";

            col7.DataPropertyName = "SmsSent";
            dataGridView_Guest.Columns.Add(col7);
            dataGridView_Guest.Columns[7].Name = "SmsSent";

            bsrc = new BindingSource();
            bsrc.DataSource = ListOfGuest;
            dataGridView_Guest.DataSource = bsrc;
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            try
            {

                //  ******************test ******************
               // string ans = shortenIt("http://www.google.com/");
                //MessageBox.Show(ans);

                /* Test
                Guest test = new Guest()
                {
                    name = "eyal",
                    phone = "0549982590",
                    NumOfGuest = 2,
                    ShortUrl = "www.walla.co.il",
                    LongUrl = "http://www.walla.co.il",
                    SmsMasage = " hello eyal",
                    SmsSent = true
                };

                ListOfGuest.Add(test);

                ListOfGuest.WriteToExcell();
                */

                //OpenExcel(@"c:\Book1.xlsx");
                //ClosAndSaveExcel();
                //****************  end of test ******************

                ListOfGuest.WriteToExcell();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static string shortenIt(string url)
        {
            UrlshortenerService service = new UrlshortenerService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCXM9sdQAv8LYpAYuFERo6yRv3OiXHVluU",
                ApplicationName = "Wedding Planner",
            });

            var m = new Google.Apis.Urlshortener.v1.Data.Url();
            m.LongUrl = url;
            return service.Url.Insert(m).Execute().Id;
        }

        public bool OpenExcel(string path)
        {
            try
            {
                xlApp = new Excel.ApplicationClass();
                xlWorkBook = xlApp.Workbooks.Open(path);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];
                xlApp.DisplayAlerts = false;
                xlApp.ScreenUpdating = false;
                xlApp.Visible = false;
                xlApp.UserControl = false;
                xlApp.Interactive = false;

                last = xlWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                range = xlWorkSheet.get_Range("A1");

                lastUsedRow = last.Row;
                lastUsedColumn = last.Column;


                DTrace.WriteMessage(this, "open excel file: " + path, DebugLevel.GeneralMsg);
            }

            catch(Exception ex)
            {
                DTrace.WriteMessage(this, "can't open excel file: " + path, DebugLevel.GeneralError);
                return false;
            }

            return true;
        }

        public void ClosAndSaveExcel()
        {
            try
            {
                xlWorkBook.Save();
                xlWorkBook.Saved = true;
                xlWorkBook.Close();
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ex) 
            {
                DTrace.WriteMessage(this, "Error Closing Excel", DebugLevel.GeneralError);
            };
        }

        public void BuildForm(ref Guest gst)
        {
            string FormUrl = "https://docs.google.com/forms/d/e/1FAIpQLSf51RHJajspVomQ7sFjlZEacqcZ1BkbKgT5Urp4pOE6Q5elZg/viewform?";
            string Name = "&entry.129684258=";
            string accept = "&entry.79403481=כן";
            string NumOfGuest = "&entry.2051216139=";

            gst.LongUrl = FormUrl + Name + gst.name + accept + NumOfGuest + gst.NumOfGuest;
        }

        public bool ReadExcell(int inrow , int incolumn)
        {
            try
            {
                Guest guest = new Guest();

                guest.name = (xlWorkSheet.Cells[inrow, 1] as Excel.Range).Value2.ToString();
                guest.phone = (xlWorkSheet.Cells[inrow, 2] as Excel.Range).Value2.ToString();
                guest.NumOfGuest = int.Parse((xlWorkSheet.Cells[inrow, 3] as Excel.Range).Value2.ToString());
                guest.SendMessage = bool.Parse((xlWorkSheet.Cells[inrow, 4] as Excel.Range).Value2.ToString());
                guest.LongUrl = string.Empty;
                guest.ShortUrl = string.Empty;
                guest.SmsMasage = string.Empty;
                guest.SmsSent = false;


                BuildForm(ref guest);

                if (UseSortUrl)
                    guest.ShortUrl = shortenIt(guest.LongUrl);

                ListOfGuest.Add(guest);
                DTrace.WriteMessage(this, "Read from Excel \t" + guest.ToString(), DebugLevel.GeneralMsg);
                //xlWorkSheet.Cells[outrow, outcolumn] = guest.ShortUrl; disabled write to excel
                return true;
            }

            catch(Exception ex)
            {
                return false;
            }

        }


        private void button_readwrite_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Excel Files|*.xlsx;";

            if(fd.ShowDialog() == DialogResult.OK)
            {
                OpenExcel(fd.FileName);
                progressBar1.Minimum = 0;
                progressBar1.Maximum = lastUsedRow;
                progressBar1.Value = 0;
                progressBar1.Step = 1;
                ListOfGuest.Clear();
                ReadExcellWork.RunWorkerAsync();
            }
        }

        private void button_sms_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = ListOfGuest.Count() -1;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            if (textBox_url.Text != string.Empty)
                SmsServerUrl = textBox_url.Text;

            if(richTextBox_sms.Text != string.Empty)
                smsText = richTextBox_sms.Text;

            // start sending sms with 10s delay.
            SmsWork.RunWorkerAsync();
        }

        public bool SendSms(string apiurl , string phone , string str)
        {
            try
            {
                string getString = apiurl +"?phone=" + phone + "&text=" + str;
                var request = (HttpWebRequest)WebRequest.Create(getString);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string ans = string.Empty;
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    ans = reader.ReadToEnd();
                }

                if (ans.Contains("Mesage SENT!"))
                    return true;
                else
                    return false;
            }

            catch (Exception ex)
            {
                return false;
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClosAndSaveExcel();
        }


        private void SmsWork_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (Guest gst in ListOfGuest)
            {
                if (gst.SendMessage && !gst.SmsSent)
                {
                    string SmsContent = (SmsType == smsType.Invitation ? (SMS_STRING + gst.ShortUrl) : smsText);

                    if (debug) // send me all sms while debuging
                    {
                        /*

                        if (SendSms(SmsServerUrl, "0549982590", SmsContent))
                            gst.SmsSent = true;
                        else
                            gst.SmsSent = false;
                         * */
                    }
                    else
                    {
                        if (SendSms(SmsServerUrl, gst.phone, SmsContent))
                            gst.SmsSent = true;
                        else
                            gst.SmsSent = false;
                    }

                    DTrace.WriteMessage(this, "sms sent to \t" + gst.name, DebugLevel.GeneralMsg);
                    SmsWork.ReportProgress(ListOfGuest.IndexOf(gst));

                    if(debug)
                        Thread.Sleep(100);
                    else
                        Thread.Sleep(10000);
                    
                }

                else
                {
                    DTrace.WriteMessage(this, "sms not sent to \t" + gst.name, DebugLevel.GeneralMsg);
                    SmsWork.ReportProgress(ListOfGuest.IndexOf(gst));
                }
            }

        }

        void SmsWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        void SmsWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("sms sent successfully");
            UpdateGuestList();
        }

        void ReadExcellWork_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int row = 2; row <= lastUsedRow; row++)
            {
                ReadExcell(row,7);
                ReadExcellWork.ReportProgress(row);
                //Thread.Sleep(1000);
            }
        }
        void ReadExcellWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void ReadExcellWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ClosAndSaveExcel();
            button_sms.Enabled = true;
            button_send.Enabled = true;
            //MessageBox.Show("Done Reading From Excel");
            UpdateGuestList();
            progressBar1.Value = 0;
        }

        private void checkBox_shortUrl_CheckedChanged(object sender, EventArgs e)
        {
            this.UseSortUrl = checkBox_shortUrl.Checked;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            SmsType = tabControl1.SelectedTab.Text == smsType.Invitation.ToString() ? smsType.Invitation : smsType.SMS;
        }

        private void UpdateGuestList()
        {
            GuestList lst = new GuestList();
            lst.AddRange(ListOfGuest);

            foreach (Guest gst in ListOfGuest)
            {
                if (gst.SmsSent || !gst.SendMessage)
                    lst.Remove(gst);
            }
            bsrc.DataSource = lst;
            bsrc.ResetBindings(false);

            /*
            foreach (Guest gst in lst)
            {
                if(gst.SmsSent)
                    dataGridView_Guest.Rows[lst.IndexOf(gst)].DefaultCellStyle.BackColor = Color.Green;
                else
                    dataGridView_Guest.Rows[lst.IndexOf(gst)].DefaultCellStyle.BackColor = Color.Red;
            }
            */
        }

        private void dataGridView_Guest_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            /*
            DataGridView dgv = sender as DataGridView;
            Guest data = dgv.Rows[e.RowIndex].DataBoundItem as Guest;

            if(data.SendMessage )
                e.CellStyle.BackColor = Color.Green;
            */
        }

        private void dataGridView_Guest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // open web browser with the corresponding url
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                Process.Start(dataGridView_Guest[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

    }
}
    