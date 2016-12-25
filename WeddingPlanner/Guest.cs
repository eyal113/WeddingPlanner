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

namespace WeddingPlanner
{
     public class Guest
    {
        public string name { get; set; }
        public string phone { get; set; }
        public int NumOfGuest { get; set; }
        public bool SendMessage { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public string SmsMasage { get; set; }
        public bool SmsSent { get; set; }

         public Guest()
        {
            name = string.Empty;
            phone = string.Empty;
            NumOfGuest = 0;
            SendMessage = false;
            LongUrl = string.Empty;
            ShortUrl = string.Empty;
            SmsMasage = string.Empty;
            SmsSent = false;
        }
        public override string ToString()
        {
            string rtn = string.Empty;

                rtn +=  name + "\t" +
                        phone + "\t" +
                        NumOfGuest.ToString() + "\t" +
                        SendMessage.ToString() + "\t" +
                        LongUrl + "\t" +
                        ShortUrl + "\t" +
                        SmsSent.ToString();

            return rtn;
        }
    }

    public class GuestList : List<Guest>
    {
        public override string ToString()
        {
            string rtn = string.Empty;

            foreach(Guest gst in this)
            {
                rtn +=  gst.name + "\t" + 
                        gst.phone + "\t" +
                        gst.NumOfGuest.ToString() + "\t" +
                        gst.SendMessage.ToString() + "\t" +
                        gst.LongUrl + "\t" + 
                        gst.ShortUrl + "\t" +
                        gst.SmsSent.ToString() + Environment.NewLine;
            }

            return rtn;
        }

        public bool WriteToExcell()
        {
            try
            {
                Excel._Application MyApp = new Excel.ApplicationClass();
                MyApp.Visible = true;
                Excel.Workbook MyBook = MyApp.Workbooks.Add(1);
                Excel.Worksheet MySheet = (Excel.Worksheet)MyBook.Sheets[1];
                int lastRow = 0;

                Excel.Range range = MySheet.Columns["B"] as Excel.Range;
                range.Cells.NumberFormat = "@"; // format column B as text

                foreach (Guest gst in this)
                {
                    lastRow++;
                    MySheet.Cells[lastRow, 1] = gst.name;
                    MySheet.Cells[lastRow, 2] = gst.phone;
                    MySheet.Cells[lastRow, 3] = gst.NumOfGuest;
                    MySheet.Cells[lastRow, 4] = gst.LongUrl;
                    MySheet.Cells[lastRow, 5] = gst.ShortUrl;
                    MySheet.Cells[lastRow, 6] = gst.SmsMasage;
                    MySheet.Cells[lastRow, 7] = gst.SmsSent;

                }
                //MyBook.Save();
                //MyBook.Close();
                //MyApp.Quit();
                return true;
            }

            catch(Exception ex)
            {
                return false;
            }
        }
    }
}

