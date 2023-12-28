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
            SuspendLayout();
            // 
            // labelLoading
            // 
            labelLoading.AutoSize = true;
            labelLoading.BackColor = Color.Transparent;
            labelLoading.Font = new Font("Consolas", 36F, FontStyle.Bold, GraphicsUnit.Point);
            labelLoading.ForeColor = SystemColors.ButtonHighlight;
            labelLoading.ImageAlign = ContentAlignment.TopLeft;
            labelLoading.Location = new Point(163, 362);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(284, 56);
            labelLoading.TabIndex = 1;
            labelLoading.Text = "loading...";
            // 
            // LeaderboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            ClientSize = new Size(600, 800);
            Controls.Add(labelLoading);
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
    }
}