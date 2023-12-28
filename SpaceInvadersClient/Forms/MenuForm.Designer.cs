namespace SpaceInvadersClient
{
    partial class MenuForm
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
            buttonResults = new Button();
            buttonPlay = new Button();
            labelLoading = new Label();
            SuspendLayout();
            // 
            // buttonResults
            // 
            buttonResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonResults.BackColor = Color.SkyBlue;
            buttonResults.Cursor = Cursors.Hand;
            buttonResults.FlatAppearance.MouseDownBackColor = Color.SteelBlue;
            buttonResults.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            buttonResults.FlatStyle = FlatStyle.Flat;
            buttonResults.Font = new Font("Consolas", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            buttonResults.ForeColor = SystemColors.ButtonHighlight;
            buttonResults.Location = new Point(173, 394);
            buttonResults.Name = "buttonResults";
            buttonResults.Size = new Size(260, 70);
            buttonResults.TabIndex = 2;
            buttonResults.Text = "RESULTS";
            buttonResults.UseVisualStyleBackColor = false;
            // 
            // buttonPlay
            // 
            buttonPlay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonPlay.BackColor = Color.SkyBlue;
            buttonPlay.Cursor = Cursors.Hand;
            buttonPlay.FlatAppearance.MouseDownBackColor = Color.SteelBlue;
            buttonPlay.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            buttonPlay.FlatStyle = FlatStyle.Flat;
            buttonPlay.Font = new Font("Consolas", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            buttonPlay.ForeColor = SystemColors.ButtonHighlight;
            buttonPlay.Location = new Point(173, 281);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(260, 70);
            buttonPlay.TabIndex = 3;
            buttonPlay.Text = "PLAY";
            buttonPlay.UseVisualStyleBackColor = false;
            // 
            // labelLoading
            // 
            labelLoading.AutoSize = true;
            labelLoading.BackColor = Color.Transparent;
            labelLoading.Font = new Font("Consolas", 36F, FontStyle.Bold, GraphicsUnit.Point);
            labelLoading.ForeColor = SystemColors.ButtonHighlight;
            labelLoading.ImageAlign = ContentAlignment.TopLeft;
            labelLoading.Location = new Point(161, 335);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(284, 56);
            labelLoading.TabIndex = 4;
            labelLoading.Text = "loading...";
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            ClientSize = new Size(600, 800);
            Controls.Add(labelLoading);
            Controls.Add(buttonPlay);
            Controls.Add(buttonResults);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonResults;
        private Button buttonPlay;
        private Label labelLoading;
    }
}