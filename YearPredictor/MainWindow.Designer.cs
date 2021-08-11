
namespace Osu20XXML.WindowsForm

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
            this.MapPanel = new System.Windows.Forms.Panel();
            this.noResultsLabel = new System.Windows.Forms.Label();
            this.MapsLabel = new System.Windows.Forms.Label();
            this.fileLoadingProgressBar = new System.Windows.Forms.ProgressBar();
            this.DropPanel = new System.Windows.Forms.Panel();
            this.DropLabel = new System.Windows.Forms.Label();
            this.YearLabel = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileLoader = new System.ComponentModel.BackgroundWorker();
            this.ClearButton = new System.Windows.Forms.Button();
            this.LeftPageButton = new System.Windows.Forms.Button();
            this.RightPageButton = new System.Windows.Forms.Button();
            this.PageLabel = new System.Windows.Forms.TextBox();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.MappingStyleLabel = new System.Windows.Forms.Label();
            this.LoadOsuFolderButton = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.MapPanel.SuspendLayout();
            this.DropPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapPanel
            // 
            this.MapPanel.AutoScroll = true;
            this.MapPanel.AutoScrollMinSize = new System.Drawing.Size(0, 1);
            this.MapPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MapPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MapPanel.Controls.Add(this.noResultsLabel);
            this.MapPanel.Controls.Add(this.MapsLabel);
            this.MapPanel.Location = new System.Drawing.Point(404, 12);
            this.MapPanel.MaximumSize = new System.Drawing.Size(384, 302);
            this.MapPanel.MinimumSize = new System.Drawing.Size(384, 302);
            this.MapPanel.Name = "MapPanel";
            this.MapPanel.Size = new System.Drawing.Size(384, 302);
            this.MapPanel.TabIndex = 4;
            // 
            // noResultsLabel
            // 
            this.noResultsLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.noResultsLabel.Location = new System.Drawing.Point(3, 119);
            this.noResultsLabel.Name = "noResultsLabel";
            this.noResultsLabel.Size = new System.Drawing.Size(376, 75);
            this.noResultsLabel.TabIndex = 8;
            this.noResultsLabel.Text = "No search results";
            this.noResultsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.noResultsLabel.Visible = false;
            // 
            // MapsLabel
            // 
            this.MapsLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MapsLabel.Location = new System.Drawing.Point(3, 120);
            this.MapsLabel.Name = "MapsLabel";
            this.MapsLabel.Size = new System.Drawing.Size(378, 75);
            this.MapsLabel.TabIndex = 0;
            this.MapsLabel.Text = "Your maps will appear here after being imported";
            this.MapsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileLoadingProgressBar
            // 
            this.fileLoadingProgressBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.fileLoadingProgressBar.Location = new System.Drawing.Point(-2, 387);
            this.fileLoadingProgressBar.Name = "fileLoadingProgressBar";
            this.fileLoadingProgressBar.Size = new System.Drawing.Size(385, 19);
            this.fileLoadingProgressBar.TabIndex = 7;
            this.fileLoadingProgressBar.Visible = false;
            // 
            // DropPanel
            // 
            this.DropPanel.AllowDrop = true;
            this.DropPanel.BackColor = System.Drawing.Color.LightSlateGray;
            this.DropPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DropPanel.Controls.Add(this.DropLabel);
            this.DropPanel.Controls.Add(this.fileLoadingProgressBar);
            this.DropPanel.Location = new System.Drawing.Point(12, 12);
            this.DropPanel.Name = "DropPanel";
            this.DropPanel.Size = new System.Drawing.Size(384, 407);
            this.DropPanel.TabIndex = 5;
            this.DropPanel.Click += new System.EventHandler(this.DropPanel_Click);
            this.DropPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.DropPanel_DragDrop);
            this.DropPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.DropPanel_DragEnter);
            this.DropPanel.DragLeave += new System.EventHandler(this.DropPanel_DragLeave);
            // 
            // DropLabel
            // 
            this.DropLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DropLabel.Location = new System.Drawing.Point(-2, 163);
            this.DropLabel.Name = "DropLabel";
            this.DropLabel.Size = new System.Drawing.Size(385, 76);
            this.DropLabel.TabIndex = 0;
            this.DropLabel.Text = "Drop .osz or .osu files here or click to choose files";
            this.DropLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DropLabel.Click += new System.EventHandler(this.DropLabel_Click);
            // 
            // YearLabel
            // 
            this.YearLabel.BackColor = System.Drawing.Color.Transparent;
            this.YearLabel.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.YearLabel.Location = new System.Drawing.Point(636, 361);
            this.YearLabel.Name = "YearLabel";
            this.YearLabel.Size = new System.Drawing.Size(152, 63);
            this.YearLabel.TabIndex = 6;
            this.YearLabel.Text = "20XX";
            this.YearLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(404, 320);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(101, 23);
            this.ClearButton.TabIndex = 8;
            this.ClearButton.Text = "Clear All Maps";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // LeftPageButton
            // 
            this.LeftPageButton.Location = new System.Drawing.Point(662, 320);
            this.LeftPageButton.Name = "LeftPageButton";
            this.LeftPageButton.Size = new System.Drawing.Size(25, 23);
            this.LeftPageButton.TabIndex = 9;
            this.LeftPageButton.Text = "<";
            this.LeftPageButton.UseVisualStyleBackColor = true;
            this.LeftPageButton.Click += new System.EventHandler(this.leftPageButton_Click);
            // 
            // RightPageButton
            // 
            this.RightPageButton.Location = new System.Drawing.Point(763, 320);
            this.RightPageButton.Name = "RightPageButton";
            this.RightPageButton.Size = new System.Drawing.Size(25, 23);
            this.RightPageButton.TabIndex = 10;
            this.RightPageButton.Text = ">";
            this.RightPageButton.UseVisualStyleBackColor = true;
            this.RightPageButton.Click += new System.EventHandler(this.rightPageButton_Click);
            // 
            // PageLabel
            // 
            this.PageLabel.Enabled = false;
            this.PageLabel.Location = new System.Drawing.Point(693, 320);
            this.PageLabel.Name = "PageLabel";
            this.PageLabel.Size = new System.Drawing.Size(64, 23);
            this.PageLabel.TabIndex = 11;
            this.PageLabel.Text = "0/0";
            this.PageLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PageLabel.WordWrap = false;
            this.PageLabel.TextChanged += new System.EventHandler(this.pageLabel_TextChanged);
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(404, 349);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.PlaceholderText = "Enter search terms here...";
            this.SearchBox.Size = new System.Drawing.Size(384, 23);
            this.SearchBox.TabIndex = 12;
            this.SearchBox.WordWrap = false;
            this.SearchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // MappingStyleLabel
            // 
            this.MappingStyleLabel.Font = new System.Drawing.Font("Segoe UI", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MappingStyleLabel.Location = new System.Drawing.Point(404, 382);
            this.MappingStyleLabel.Name = "MappingStyleLabel";
            this.MappingStyleLabel.Size = new System.Drawing.Size(236, 56);
            this.MappingStyleLabel.TabIndex = 13;
            this.MappingStyleLabel.Text = "Mapping style:";
            // 
            // LoadOsuFolderButton
            // 
            this.LoadOsuFolderButton.Location = new System.Drawing.Point(511, 320);
            this.LoadOsuFolderButton.Name = "LoadOsuFolderButton";
            this.LoadOsuFolderButton.Size = new System.Drawing.Size(144, 23);
            this.LoadOsuFolderButton.TabIndex = 14;
            this.LoadOsuFolderButton.Text = "Load From Osu Folder";
            this.LoadOsuFolderButton.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 431);
            this.Controls.Add(this.LoadOsuFolderButton);
            this.Controls.Add(this.MappingStyleLabel);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.PageLabel);
            this.Controls.Add(this.RightPageButton);
            this.Controls.Add(this.LeftPageButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.YearLabel);
            this.Controls.Add(this.DropPanel);
            this.Controls.Add(this.MapPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Osu20XXML.WindowsForm.Properties.Resources.XX_Icon;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Osu20XX";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MapPanel.ResumeLayout(false);
            this.DropPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel MapPanel;
        private System.Windows.Forms.Panel DropPanel;
        private System.Windows.Forms.Label DropLabel;
        private System.Windows.Forms.Label YearLabel;
        private System.Windows.Forms.Label MapsLabel;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.ComponentModel.BackgroundWorker fileLoader;
        private System.Windows.Forms.ProgressBar fileLoadingProgressBar;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button LeftPageButton;
        private System.Windows.Forms.Button RightPageButton;
        private System.Windows.Forms.TextBox PageLabel;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Label noResultsLabel;
        private System.Windows.Forms.Label MappingStyleLabel;
        private System.Windows.Forms.Button LoadOsuFolderButton;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}

