//===========================================================
//
// Copyright (C) 2013 Samarjeet Singh
//
//===========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SS.Ynote.Classic
{
    static class Program
    {
        #region Entry Point
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ProcessOptions(args);
        }
        #endregion

        #region Processes Options

        static void ProcessOptions(string[] options)
        {
            if (options.Length == 0)
            {
                Application.Run(options.Length == 0 ? new Main(string.Empty,false) : new Main(options[0],false));
            }
            else
            {
                for (int i = 0; i < options.Length; i++)
                {
                    switch (options[i])
                    {
                        case "-session":  // string data
                            i++;
                            if (options.Length <= i) throw new ArgumentException(options[i]);
                              Application.Run(new Main(options[i],true));
                                             break;


                        case "-clean":
                               Properties.Settings.Default.Reset();
                            break;
                        case "-systemtray": Main m = new Main(string.Empty, false);
                            m.NotifyIcon.BalloonTipTitle = "Ynote Classic";
                            m.NotifyIcon.BalloonTipText = "Ynote Classic has been minimized to the System Tray.";
                            m.NotifyIcon.ShowBalloonTip(3000);
                            m.Hide();

                            break;

                        case "-zoom":
                            i++;
                              if (options.Length <= i) throw new ArgumentException(options[i]);
                   
                                 int _intParam = System.Int32.Parse(options[i]);
                                 Main mform = new Main(string.Empty, false);
                                 mform.Adjustzoom(_intParam);
                                 Application.Run(mform);
                            break;
                        case "-help": ShowHelp();
                         
                            break;
                        default: Application.Run(new Main(options[0],false));
                            break;

                    }
                }

            }
        }
        static void ShowHelp()
        {
           MessageBox.Show("SS.Ynote.Classic.exe -help (Show Help)\nSS.Ynote.Classic.exe -zoom (Adjust Zoom)\nSS.Ynote.Classic.exe -systemtray (Launch in System Tray)\n SS.Ynote.Classic.exe -session (Open Session)\nSS.Ynote.Classic.exe -clean (Launch Clean)","Command Line Arguments Help");
        }
        #endregion
    }
}
