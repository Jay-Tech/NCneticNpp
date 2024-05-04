﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NCneticCore;
using NCneticCore.View;
using System.Globalization;

using System.IO;
using static System.Windows.Forms.LinkLabel;

namespace NCneticNpp
{
    public partial class ViewForm : Form
    {
        ncView view;
        private NcJob job;

        int DownMouseX = 0;
        int DownMouseY = 0;
        static string iniFilePath = null;
        static ViewForm frmMyDlg = null;
        static Bitmap tbBmp = NCneticNpp.Properties.Resources.icon;
        static Bitmap tbBmp_tbTab = NCneticNpp.Properties.Resources.icon;
        static Icon tbIcon = null;
        private ncMachine mach;


        static bool styling = true;
        static int cam = 0;
        public int currentLine = -1;

        public string currentFile = "";

        public event SelChangedEventHandler SelChanged;
        public delegate void SelChangedEventHandler(object source, SelChangedEventArgs e);

        public class SelChangedEventArgs : EventArgs
        {
            private int Line;

            public SelChangedEventArgs(int l)
            {
                Line = l;
            }
            public int GetLine()
            {
                return Line;
            }
        }

        public event CloseClickEventHandler CloseClick;
        public delegate void CloseClickEventHandler(object source, CloseClickEventArgs e);
        public class CloseClickEventArgs : EventArgs { }

        public ViewForm()
        {
            InitializeComponent(); 
            mach = new ncMachine();
            view = new ncView(new ncViewOptions());
            view.IniGraphicContext(this.Handle);

            view.ViewPortLoad(glControl.Width, glControl.Height);

            IniEvents();
        }

        void IniEvents()
        {
            glControl.Resize += new EventHandler((s, ea) =>
            {
                view.ViewChangeSize(glControl.Width, glControl.Height);
            });
            glControl.Load += new EventHandler((s, ea) =>
            {
                view.ViewPortLoad(glControl.Width, glControl.Height);
            });
            glControl.Paint += new PaintEventHandler((s, ea) =>
            {
                view.ViewPortPaint();
                glControl.SwapBuffers();
            });

            glControl.MouseDown += new MouseEventHandler((s, ea) =>
            {
                DownMouseX = ea.X;
                DownMouseY = ea.Y;

                if (ea.Button == MouseButtons.Left)
                {
                    view.MouseMoveSelect(ea.X, ea.Y);
                }
            });
            glControl.MouseWheel += new MouseEventHandler((s, ea) =>
            {
                view.WheelZoom(ea.X, ea.Y, ea.Delta);
            });
            glControl.MouseMove += new MouseEventHandler((s, ea) =>
            {
                if (ea.Button == MouseButtons.Middle)
                {
                    view.MousePan(ref DownMouseX, ref DownMouseY, ea.X - DownMouseX, ea.Y - DownMouseY);
                }
                else if (ea.Button == MouseButtons.Right)
                {
                    view.MouseRotate(ref DownMouseX, ref DownMouseY, ea.X - DownMouseX, ea.Y - DownMouseY);
                }
                else if (ea.Button == MouseButtons.None)
                {
                    view.MouseHighlight(ea.X, ea.Y);
                }

            });
            glControl.MouseDoubleClick += new MouseEventHandler((s, ea) =>
            {
                view.Recenter();
            });

            view.Refresh += new EventHandler((s, ea) =>
            {
                glControl.Invalidate();
            });

            view.MoveSelected += new ncView.MoveSelectedkEventHandler((s,ea) =>
            {
                if(job==null)return;
                int selId = job.MoveList.FindIndex(x => x.MoveGuid == ea.guid);

                if (selId >= 0 && selId < job.MoveList.Count)
                {
                    view.SelectMove(job.MoveList[selId]);
                    SelChanged?.Invoke(this, new SelChangedEventArgs(job.MoveList[selId].Line));
                    trackBar.Value = selId;
                }
                else
                {
                    SelChanged?.Invoke(this, new SelChangedEventArgs(-1));
                }
            });

            trackBar.ValueChanged += new EventHandler((s, ea) =>
            {
                if (job.MoveList.Any() && trackBar.Value > 0 && trackBar.Value < job.MoveList.Count)
                {
                    view.SelectMove(job.MoveList[trackBar.Value]);
                    SelChanged?.Invoke(this, new SelChangedEventArgs(job.MoveList[trackBar.Value].Line));
                }
            });
        }

        public void LoadFile(string file, string text, ncMachine mach, int cam)
        {
            currentFile = file;
          

            ReadFile(currentFile);
            List<string> gList = new List<string>();
            foreach (var line in GCodeList)
            {
                gList.Add(line.GCode);
            }
            job = new NcJob
            {
                FileName = file,
                TextList = gList
               
            };

            job.Process(mach);

            progressBar.Style = ProgressBarStyle.Marquee;

            job.EndProcessing += new EventHandler((s, ea) =>
            {
                view.LoadJob(job);
               
                SetCam(cam);

                trackBar.Minimum = 0;
                trackBar.Maximum = job.MoveList.Count() - 1;
                trackBar.Value = trackBar.Minimum;
                trackBar.LargeChange = trackBar.Maximum / 20;

                progressBar.Style = ProgressBarStyle.Continuous;
                

               
            });
            

            ResetSelection();
            SetSelection(1);
            lbGcodeList.Text = currentFile;
        }
        string _line  = string.Empty;
        public List<GCodeLine> GCodeList = new List<GCodeLine>();
        private int _index = 0;
        private void ReadFile(string file)
        {
            StreamReader sr = new StreamReader(file);
            _line = sr.ReadLine();
            while (_line != null)
            {
                if (!string.IsNullOrEmpty(_line) && !_line.StartsWith("(") && !_line.EndsWith(")"))
                {
                    GCodeList.Add(new GCodeLine(_line,_index));
                    _index++;
                }
                _line = sr.ReadLine();

            }
            sr.Close();
            var bs = new BindingSource();
            bs.DataSource = GCodeList;

            lbGcodeList.DataSource = GCodeList;
            lbGcodeList.DisplayMember = "GCode";
            lbGcodeList.ValueMember = "LineNumber";
            ResetSelection();


        }
        private void LbGcodeList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var index = lbGcodeList.SelectedIndex;
            SetSelection(index);
        }
        public void SetSelection(int line)
        {
            if(job?.MoveList ==null)return;
            ncMove sel = job.MoveList.Find(x => x.MoveGuid == view.SelGuid);

            if (sel != null)
            {
                if (sel.Line == line)
                {
                    DisplayProperties(sel);
                    return;
                }
            }

            int selId = job.MoveList.FindIndex(x => x.Line == line);

            if (selId >= 0 && selId < job.MoveList.Count)
            {
                sel = job.MoveList[selId];

                currentLine = line;
                view.SelectMove(sel);

                if (trackBar.Value != selId)
                {
                    trackBar.Value = selId;
                }

                DisplayProperties(sel);
            }
        }

        public void ResetSelection()
        {
            view.SelectMove(new ncMove());

            trackBar.Value = 0;

            xStatusLabel.Text = "X = 0.000 (0.000 + 0.000)";
            yStatusLabel.Text = "Y = 0.000 (0.000 + 0.000)";
            zStatusLabel.Text = "Z = 0.000 (0.000 + 0.000)";
            lrStatusLabel.Text = "L = 0.000";
            fStatusLabel.Text = "F = 0.000";
            sStatusLabel.Text = "S = 0.000";
        }

        private void DisplayProperties(ncMove move)
        {
            string sb;

            // X *********************************************************************************************************************
            sb = string.Empty;
            sb = "X = " + move.P.X.ToString("0.000", CultureInfo.InvariantCulture);
            sb += " (" + move.P0.X.ToString("0.000", CultureInfo.InvariantCulture);
            if (move.P.X - move.P0.X < 0)
            {
                sb += " - " + Math.Abs(move.P.X - move.P0.X).ToString("0.000", CultureInfo.InvariantCulture) + ")";
            }
            else
            {
                sb += " + " + Math.Abs(move.P.X - move.P0.X).ToString("0.000", CultureInfo.InvariantCulture) + ")";
            }
            if (move.Type == ncMove.MoveType.CircularCW || move.Type == ncMove.MoveType.CircularCCW)
            {
                sb += "; I = " + move.C.X.ToString("0.000", CultureInfo.InvariantCulture);
                sb += " (" + move.P0.X.ToString("0.000", CultureInfo.InvariantCulture);
                if (move.C.X - move.P0.X < 0)
                {
                    sb += " - " + Math.Abs(move.C.X - move.P0.X).ToString("0.000", CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    sb += " + " + Math.Abs(move.C.X - move.P0.X).ToString("0.000", CultureInfo.InvariantCulture) + ")";
                }
            }
            xStatusLabel.Text = sb;

            // Y *********************************************************************************************************************
            sb = string.Empty;
            sb += "Y = " + move.P.Y.ToString("0.000", CultureInfo.InvariantCulture);
            sb += " (" + move.P0.Y.ToString("0.000", CultureInfo.InvariantCulture);
            if (move.P.Y - move.P0.Y < 0)
            {
                sb += " - " + Math.Abs(move.P.Y - move.P0.Y).ToString("0.000", CultureInfo.InvariantCulture) + ")";
            }
            else
            {
                sb += " + " + Math.Abs(move.P.Y - move.P0.Y).ToString("0.000", CultureInfo.InvariantCulture) + ")";
            }
            if (move.Type == ncMove.MoveType.CircularCW || move.Type == ncMove.MoveType.CircularCCW)
            {
                sb += "; J = " + move.C.Y.ToString("0.000", CultureInfo.InvariantCulture);
                sb += " (" + move.P0.Y.ToString("0.000", CultureInfo.InvariantCulture);
                if (move.C.Y - move.P0.Y < 0)
                {
                    sb += " - " + Math.Abs(move.C.Y - move.P0.Y).ToString("0.000", CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    sb += " + " + Math.Abs(move.C.Y - move.P0.Y).ToString("0.000", CultureInfo.InvariantCulture) + ")";
                }
            }
            yStatusLabel.Text = sb;

            // Z *********************************************************************************************************************
            sb = string.Empty;
            sb += "Z = " + move.P.Z.ToString("0.000", CultureInfo.InvariantCulture);
            sb += " (" + move.P0.Z.ToString("0.000", CultureInfo.InvariantCulture);
            if (move.P.Z - move.P0.Z < 0)
            {
                sb += " - " + Math.Abs(move.P.Z - move.P0.Z).ToString("0.000", CultureInfo.InvariantCulture) + ")";
            }
            else
            {
                sb += " + " + Math.Abs(move.P.Z - move.P0.Z).ToString("0.000", CultureInfo.InvariantCulture) + ")";
            }
            if (move.Type == ncMove.MoveType.CircularCW || move.Type == ncMove.MoveType.CircularCCW)
            {
                sb += "; K = " + move.C.Z.ToString("0.000", CultureInfo.InvariantCulture);
                sb += " (" + move.P0.Z.ToString("0.000", CultureInfo.InvariantCulture);
                if (move.C.Z - move.P0.Z < 0)
                {
                    sb += " - " + Math.Abs(move.C.Z - move.P0.Z).ToString("0.000", CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    sb += " + " + Math.Abs(move.C.Z - move.P0.Z).ToString("0.000", CultureInfo.InvariantCulture) + ")";
                }
            }
            zStatusLabel.Text = sb;

            // INFO ******************************************************************************************************************

            sb = string.Empty;
            sb += "L = " + move.Length.ToString("0.000", CultureInfo.InvariantCulture);
            if (move.Type == ncMove.MoveType.CircularCW || move.Type == ncMove.MoveType.CircularCCW)
            {
                sb += "; R = " + move.R.ToString("0.000", CultureInfo.InvariantCulture);
            }
            lrStatusLabel.Text = sb;

            fStatusLabel.Text = "F = " + move.F.ToString("0.000", CultureInfo.InvariantCulture);
            sStatusLabel.Text = "S = " + move.S.ToString("0.000", CultureInfo.InvariantCulture);
        }

        public void SetCam(int cam)
        {
            ncViewOptions opts = new ncViewOptions();

            switch (cam)
            {
                case 1:
                    opts.DefaultView = ncViewOptions.CameraView.xYZ;
                    break;

                case 2:
                    opts.DefaultView = ncViewOptions.CameraView.xyZ;
                    break;

                case 3:
                    opts.DefaultView = ncViewOptions.CameraView.XyZ;
                    break;

                case 4:
                    opts.DefaultView = ncViewOptions.CameraView.XY;
                    break;

                case 5:
                    opts.DefaultView = ncViewOptions.CameraView.XZ;
                    break;

                case 6:
                    opts.DefaultView = ncViewOptions.CameraView.YZ;
                    break;

                case 0:
                default:
                    opts.DefaultView = ncViewOptions.CameraView.XYZ;
                    break;
            }

            view.UpdateViewOpts(opts);
            view.Recenter();
        }

        private void btnFileLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog1.FileName;
                LoadFile(file, " ", mach, 0);
            }
        }

        //protected override void WndProc(ref Message m)
        //{
        ////Listen for the closing of the dockable panel as the result of Npp native close ("cross") button on the window
        //switch (m.Msg)
        //{
        //    case 78: // Win32.WM_NOTIFY
        //        var notify = (ScNotificationHeader)Marshal.PtrToStructure(m.LParam, typeof(ScNotificationHeader));
        //        if (notify.Code == (int)DockMgrMsg.DMN_CLOSE)
        //        {
        //            CloseClick?.Invoke(this, new CloseClickEventArgs());
        //            currentLine = -1;
        //            currentFile = "";
        //        }
        //        break;
        //}
        //base.WndProc(ref m);
        //}
    }
}
public class GCodeLine
{
    public int LineNumber { get; set; }
    public string GCode { get; set; }

    public GCodeLine(string text, int lineNumber)
    {
        GCode = text;
        LineNumber = lineNumber;
    }
}