using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace WeddingPlanner
{
    [FlagsAttribute]
    public enum DebugLevel
    {
        None = 0x00,
        GoodMsg = 0x01,
        VisualDSPMsg = 0x02,
        PortDebug = 0x04,
        GeneralError = 0x40,
        GeneralMsg = 0x80,
        GeneralDebug = 0x08,
        SpecificDebug = 0x10,
        All = 0xFFFF
    }

    /// <summary>
    /// Base class for all Trace Listeners
    /// </summary>
    public abstract class DTraceListener
    {
        public DebugLevel filter;

        public DTraceListener(DebugLevel filter)
        {
            this.filter = filter;
        }

        protected static Color MapMsgToColor(DebugLevel level)
        {
            switch (level)
            {
                case DebugLevel.GoodMsg: return Color.Green;
                case DebugLevel.VisualDSPMsg: return Color.Blue;
                case DebugLevel.GeneralError: return Color.Firebrick;
                case DebugLevel.GeneralMsg: return Color.Black;
                case DebugLevel.PortDebug: return Color.Sienna;
                case DebugLevel.GeneralDebug: return Color.DarkMagenta;
                default: return Color.DarkGoldenrod;
            }
        }

        public abstract void WriteLine(string text, DebugLevel level);
    }

    /// <summary>
    /// Class for a trace listener that outputs text to a RichTextBox
    /// </summary>
    public class DTraceListenerTextBox : DTraceListener
    {
        RichTextBox box;

        delegate void SetTextCallback(string text, DebugLevel filter, RichTextBox box);

        public DTraceListenerTextBox(RichTextBox box, DebugLevel filter)
            : base(filter)
        {
            this.box = box;
        }

        override public void WriteLine(string text, DebugLevel level)
        {
            try
            {
                if (box != null && !box.Disposing && !box.IsDisposed)
                {
                    if (box.InvokeRequired)
                        box.BeginInvoke(new SetTextCallback(WriteLineInCallBack), new object[] { text, level, box });
                    else
                        WriteLineInCallBack(text, level, box);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        void WriteLineInCallBack(string text, DebugLevel level, RichTextBox box)
        {
            try
            {
                if (box != null && !box.IsDisposed && !box.Disposing)
                {
                    if (box.Lines.Length > 1000)
                    {
                        int ind = box.GetFirstCharIndexFromLine(250);
                        box.Text = box.Text.Substring(ind);
                    }
                    box.SelectionStart = box.TextLength;
                    box.SelectionColor = MapMsgToColor(level);
                    box.SelectedText = text + "\n";
                    box.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }

    /// <summary>
    /// Class for a trace listener that outputs text to a Text File
    /// </summary>
    public class DTraceListenerFile : DTraceListener
    {
        string fname = "Log.txt";

        public DTraceListenerFile(DebugLevel filter)
            : base(filter)
        {
        }

        override public void WriteLine(string text, DebugLevel level)
        {
            string wtext = "[" + level + "]\t" + DateTime.Now.ToShortDateString() + "\t" + text;
            try
            {
                using (StreamWriter stream = new StreamWriter(fname, true))
                {
                    stream.WriteLine(wtext.ToCharArray());
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }

    /// <summary>
    /// Static class that implements Tracing of program messages. Diverts messages to all listening RichTextBoxes.
    /// </summary>
    public static class DTrace
    {
        static List<DTraceListener> listeners = new List<DTraceListener>();
        static public bool Verbose = true;

        public static void AddListener(DTraceListener listener)
        {
            listeners.Add(listener);
        }


        public static void WriteMessage(object sender, string errmsg, DebugLevel level)
        {
            errmsg = sender == null ? errmsg : sender.GetType().Name + ":: " + errmsg;
            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToLongTimeString(), trimString = " AMP";
            string line = timeString.Trim(trimString.ToCharArray()) + "." + ((int)(currentTime.Millisecond / 10)).ToString("00") + "\t" + errmsg;

            //write message to all listeners
            foreach (DTraceListener listener in listeners)
            {
                if ((listener.filter & level) != 0)
                    listener.WriteLine(line, level);
            }
        }
    }
}

