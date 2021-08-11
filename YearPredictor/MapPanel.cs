using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Osu20XXML.WindowsForm

{
    public class MapPanel : System.Windows.Forms.Panel
    {
        public MapInfo associatedMapInfo;
        MainWindow mainRef;

        private Button DeleteButton;
        private Label NameLabel;
        private Label DiffLabel;


        public MapPanel(MapInfo mapInfo, int position, MainWindow mainRef)
        {
            this.mainRef = mainRef;
            associatedMapInfo = mapInfo;

            this.Name = mapInfo.MapName + "_" + mapInfo.DiffName + "_panel";
            this.Location = new Point(0, position * 50);
            this.Size = new Size(382, 50);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(206)))), ((int)(((byte)(207)))));
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Click += new System.EventHandler(this.Map_Clicked);

            DeleteButton = new Button();
            DeleteButton.AutoSize = false;
            DeleteButton.Location = new Point(0, 0);
            DeleteButton.Size = new Size(50, 50);
            DeleteButton.BackColor = Color.Red;
            DeleteButton.Text = "X";
            DeleteButton.Click += new System.EventHandler(this.Delete_Clicked);

            NameLabel = new Label();
            NameLabel.AutoSize = false;
            NameLabel.Location = new Point(50, 0);
            NameLabel.Size = new Size(332,25);
            NameLabel.TextAlign = ContentAlignment.MiddleLeft;
            NameLabel.Text = associatedMapInfo.MapName;
            NameLabel.Click += new System.EventHandler(this.Map_Clicked);

            DiffLabel = new Label();
            DiffLabel.AutoSize = false;
            DiffLabel.Location = new Point(50, 25);
            DiffLabel.Size = new Size(332, 25);
            DiffLabel.TextAlign = ContentAlignment.MiddleLeft;
            DiffLabel.Text = associatedMapInfo.DiffName;
            DiffLabel.Click += new System.EventHandler(this.Map_Clicked);

            this.Controls.Add(DeleteButton);
            this.Controls.Add(NameLabel);
            this.Controls.Add(DiffLabel);
        }

        public void move(int newY)
        {
            this.Location = new Point(0, newY);
        }

        private void Delete_Clicked(object sender, System.EventArgs e)
        {
            mainRef.DeleteMap(this);
        }

        private void Map_Clicked(object sender, System.EventArgs e)
        {
            mainRef.EvaluateMap(this.associatedMapInfo);
        }
    }
}
