
namespace YearPredictor
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.MapPanel = new System.Windows.Forms.Panel();
            this.MapsLabel = new System.Windows.Forms.Label();
            this.DropPanel = new System.Windows.Forms.Panel();
            this.DropLabel = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.YearLabel = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileLoader = new System.ComponentModel.BackgroundWorker();
            this.fileLoadingProgressBar = new System.Windows.Forms.ProgressBar();
            this.ControlPanel.SuspendLayout();
            this.MapPanel.SuspendLayout();
            this.DropPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ExitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Image = global::YearPredictor.Properties.Resources.exit;
            this.ExitButton.Location = new System.Drawing.Point(775, 0);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(0);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ExitButton.Size = new System.Drawing.Size(25, 25);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.ExitButton);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(800, 25);
            this.ControlPanel.TabIndex = 1;
            this.ControlPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ControlBar_MouseMove);
            // 
            // MapPanel
            // 
            this.MapPanel.AutoScroll = true;
            this.MapPanel.AutoScrollMinSize = new System.Drawing.Size(0, 1);
            this.MapPanel.AutoSize = true;
            this.MapPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MapPanel.Controls.Add(this.MapsLabel);
            this.MapPanel.Location = new System.Drawing.Point(404, 31);
            this.MapPanel.MaximumSize = new System.Drawing.Size(384, 314);
            this.MapPanel.MinimumSize = new System.Drawing.Size(384, 314);
            this.MapPanel.Name = "MapPanel";
            this.MapPanel.Size = new System.Drawing.Size(384, 314);
            this.MapPanel.TabIndex = 4;
            // 
            // MapsLabel
            // 
            this.MapsLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MapsLabel.Location = new System.Drawing.Point(0, 120);
            this.MapsLabel.Name = "MapsLabel";
            this.MapsLabel.Size = new System.Drawing.Size(381, 75);
            this.MapsLabel.TabIndex = 0;
            this.MapsLabel.Text = "Your maps will appear here after being imported";
            this.MapsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DropPanel
            // 
            this.DropPanel.AllowDrop = true;
            this.DropPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.DropPanel.Controls.Add(this.DropLabel);
            this.DropPanel.Location = new System.Drawing.Point(12, 31);
            this.DropPanel.Name = "DropPanel";
            this.DropPanel.Size = new System.Drawing.Size(384, 243);
            this.DropPanel.TabIndex = 5;
            this.DropPanel.Click += new System.EventHandler(this.DropPanel_Click);
            this.DropPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.DropPanel_DragEnter);
            this.DropPanel.DragLeave += new System.EventHandler(this.DropPanel_DragLeave);
            // 
            // DropLabel
            // 
            this.DropLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DropLabel.Location = new System.Drawing.Point(0, 85);
            this.DropLabel.Name = "DropLabel";
            this.DropLabel.Size = new System.Drawing.Size(384, 76);
            this.DropLabel.TabIndex = 0;
            this.DropLabel.Text = "Drop .osz or .osu files here or click to choose files";
            this.DropLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DropLabel.Click += new System.EventHandler(this.DropLabel_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // YearLabel
            // 
            this.YearLabel.AutoSize = true;
            this.YearLabel.BackColor = System.Drawing.Color.Transparent;
            this.YearLabel.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.YearLabel.Location = new System.Drawing.Point(37, 348);
            this.YearLabel.Name = "YearLabel";
            this.YearLabel.Size = new System.Drawing.Size(152, 72);
            this.YearLabel.TabIndex = 6;
            this.YearLabel.Text = "20XX";
            // 
            // fileDialog
            // 
            this.fileDialog.Filter = "osz files|*.osz|osu files|*.osu";
            this.fileDialog.Multiselect = true;
            // 
            // fileLoader
            // 
            this.fileLoader.WorkerReportsProgress = true;
            this.fileLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.fileLoader_DoWork);
            this.fileLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.fileLoader_ProgressChanged);
            this.fileLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.fileLoader_RunWorkerCompleted);
            // 
            // fileLoadingProgressBar
            // 
            this.fileLoadingProgressBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.fileLoadingProgressBar.Location = new System.Drawing.Point(404, 348);
            this.fileLoadingProgressBar.Name = "fileLoadingProgressBar";
            this.fileLoadingProgressBar.Size = new System.Drawing.Size(384, 15);
            this.fileLoadingProgressBar.TabIndex = 7;
            this.fileLoadingProgressBar.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::YearPredictor.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.fileLoadingProgressBar);
            this.Controls.Add(this.YearLabel);
            this.Controls.Add(this.DropPanel);
            this.Controls.Add(this.MapPanel);
            this.Controls.Add(this.ControlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ControlPanel.ResumeLayout(false);
            this.MapPanel.ResumeLayout(false);
            this.DropPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Panel MapPanel;
        private System.Windows.Forms.Panel DropPanel;
        private System.Windows.Forms.Label DropLabel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label YearLabel;
        private System.Windows.Forms.Label MapsLabel;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.ComponentModel.BackgroundWorker fileLoader;
        private System.Windows.Forms.ProgressBar fileLoadingProgressBar;
    }
}

