//Clipboard Monitor.cs
//Copyright (C) 2013 Samarjeet Singh
namespace SS.Ynote.Classic
{
    #region Using Directives
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Drawing;
    #endregion

    public partial class ClipboardMonitor : Control
    {
        #region Constructor

        IntPtr nextClipboardViewer;
        public ClipboardMonitor()
        {
            this.BackColor = Color.Red;
            this.Visible = false;
            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
        }
        public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;
        protected override void Dispose(bool disposing)
        {
            try
            {
                ChangeClipboardChain(this.Handle, nextClipboardViewer);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        #endregion

        #region Win API

        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    OnClipboardChanged();
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        #region Event
        void OnClipboardChanged()
        {
            try
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (ClipboardChanged != null)
                {
                    ClipboardChanged(this, new ClipboardChangedEventArgs(iData));
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        #endregion
    }
    public class ClipboardChangedEventArgs : EventArgs
    {
        public readonly IDataObject DataObject;

        public ClipboardChangedEventArgs(IDataObject dataObject)
        {
            DataObject = dataObject;
        }
    }
}