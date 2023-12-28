namespace SpaceInvadersClient
{
    partial class LeaderboardForm
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
            labelLoading = new Label();
            textResults = new TextBox();
            label2 = new Label();
            buttonExit = new Button();
            SuspendLayout();
            // 
            // labelLoading
            // 
            labelLoading.AutoSize = true;
            labelLoading.BackColor = Color.Transparent;
            labelLoading.Font = new Font("Consolas", 36F, FontStyle.Bold, GraphicsUnit.Point);
            labelLoading.ForeColor = SystemColors.ButtonHighlight;
            labelLoading.ImageAlign = ContentAlignment.TopLeft;
            labelLoading.Location = new Point(166, 343);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(284, 56);
            labelLoading.TabIndex = 1;
            labelLoading.Text = "loading...";
            // 
            // textResults
            // 
            textResults.BackColor = SystemColors.ButtonHighlight;
            textResults.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textResults.Location = new Point(95, 111);
            textResults.Multiline = true;
            textResults.Name = "textResults";
            textResults.ScrollBars = ScrollBars.Vertical;
            textResults.Size = new Size(426, 549);
            textResults.TabIndex = 6;
            // 
            // label2
            // 
            label2.BackColor = Color.SkyBlue;
            label2.Font = new Font("Consolas", 27F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(218, 42);
            label2.Name = "label2";
            label2.Size = new Size(180, 45);
            label2.TabIndex = 7;
            label2.Text = "RESULTS";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.SkyBlue;
            buttonExit.Cursor = Cursors.Hand;
            buttonExit.FlatAppearance.MouseDownBackColor = Color.SteelBlue;
            buttonExit.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Consolas", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            buttonExit.ForeColor = SystemColors.ButtonHighlight;
            buttonExit.Location = new Point(218, 696);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(180, 56);
            buttonExit.TabIndex = 8;
            buttonExit.Text = "← BACK";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // LeaderboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(600, 800);
            Controls.Add(buttonExit);
            Controls.Add(label2);
            Controls.Add(labelLoading);
            Controls.Add(textResults);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LeaderboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LeaderboardForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelLoading;
        private TextBox textResults;
        private Label label2;
        private Button buttonExit;
    }
}