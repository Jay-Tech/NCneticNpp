using System.Windows.Forms;

namespace NCneticNpp
{
    partial class ViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewForm));
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.xStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.yStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.zStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lrStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.glControl = new OpenTK.GLControl();
            this.panel = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnFileLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBar.Location = new System.Drawing.Point(0, 805);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(1872, 45);
            this.trackBar.TabIndex = 13;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.SystemColors.ControlDark;
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xStatusLabel,
            this.yStatusLabel,
            this.zStatusLabel,
            this.lrStatusLabel,
            this.fStatusLabel,
            this.sStatusLabel,
            this.toolStripDropDownButton1,
            this.toolStripProgressBar1});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1872, 31);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 14;
            this.statusStrip.Text = "statusStrip";
            // 
            // xStatusLabel
            // 
            this.xStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.xStatusLabel.Name = "xStatusLabel";
            this.xStatusLabel.Size = new System.Drawing.Size(55, 26);
            this.xStatusLabel.Text = "X = 0.000";
            // 
            // yStatusLabel
            // 
            this.yStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.yStatusLabel.Name = "yStatusLabel";
            this.yStatusLabel.Size = new System.Drawing.Size(59, 26);
            this.yStatusLabel.Text = "Y = 0.000";
            // 
            // zStatusLabel
            // 
            this.zStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.zStatusLabel.Name = "zStatusLabel";
            this.zStatusLabel.Size = new System.Drawing.Size(59, 26);
            this.zStatusLabel.Text = "Z = 0.000";
            // 
            // lrStatusLabel
            // 
            this.lrStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lrStatusLabel.Name = "lrStatusLabel";
            this.lrStatusLabel.Size = new System.Drawing.Size(58, 26);
            this.lrStatusLabel.Text = "L = 0.000";
            // 
            // fStatusLabel
            // 
            this.fStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.fStatusLabel.Name = "fStatusLabel";
            this.fStatusLabel.Size = new System.Drawing.Size(58, 26);
            this.fStatusLabel.Text = "F = 0.000";
            // 
            // sStatusLabel
            // 
            this.sStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.sStatusLabel.Name = "sStatusLabel";
            this.sStatusLabel.Size = new System.Drawing.Size(58, 26);
            this.sStatusLabel.Text = "S = 0.000";
            // 
            // toolStripDropDownButton1
            // 

            // 
            // loadFileToolStripMenuItem
            // 

            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 25);
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 31);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(1872, 774);
            this.glControl.TabIndex = 17;
            this.glControl.VSync = false;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.btnFileLoad);
            this.panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel.Location = new System.Drawing.Point(0, 31);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 758);
            this.panel.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 789);
            this.progressBar.MarqueeAnimationSpeed = 20;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1872, 16);
            this.progressBar.TabIndex = 18;
            this.progressBar.Value = 100;
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "Open File";
            // 
            // btnFileLoad
            // 
            this.btnFileLoad.Location = new System.Drawing.Point(13, 20);
            this.btnFileLoad.Name = "btnFileLoad";
            this.btnFileLoad.Size = new System.Drawing.Size(75, 23);
            this.btnFileLoad.TabIndex = 0;
            this.btnFileLoad.Text = "LoadFile";
            this.btnFileLoad.UseVisualStyleBackColor = true;
            this.btnFileLoad.Click += new System.EventHandler(this.btnFileLoad_Click);
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1872, 850);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.glControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.trackBar);
            this.Name = "ViewForm";
            this.Text = "frmMyDlg";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel xStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel yStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel zStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel lrStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel fStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel sStatusLabel;
        private OpenTK.GLControl glControl;
        public System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private Button btnFileLoad;
        private OpenFileDialog fileDialog;
    }
}