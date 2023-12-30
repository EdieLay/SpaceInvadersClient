namespace SpaceInvadersClient
{
    partial class GameForm
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
            components = new System.ComponentModel.Container();
            labelLoading = new Label();
            gameOverText = new TextBox();
            gameTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // labelLoading
            // 
            labelLoading.AutoSize = true;
            labelLoading.BackColor = Color.Transparent;
            labelLoading.Font = new Font("Consolas", 36F, FontStyle.Bold, GraphicsUnit.Point);
            labelLoading.ForeColor = SystemColors.ButtonHighlight;
            labelLoading.ImageAlign = ContentAlignment.TopLeft;
            labelLoading.Location = new Point(190, 457);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(360, 70);
            labelLoading.TabIndex = 0;
            labelLoading.Text = "loading...";
            // 
            // gameOverText
            // 
            gameOverText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gameOverText.BackColor = SystemColors.InfoText;
            gameOverText.Font = new Font("Consolas", 20F, FontStyle.Bold, GraphicsUnit.Point);
            gameOverText.ForeColor = SystemColors.HighlightText;
            gameOverText.Location = new Point(111, 440);
            gameOverText.Multiline = true;
            gameOverText.Name = "gameOverText";
            gameOverText.ReadOnly = true;
            gameOverText.ShortcutsEnabled = false;
            gameOverText.Size = new Size(395, 0);
            gameOverText.TabIndex = 1;
            gameOverText.TextAlign = HorizontalAlignment.Center;
            gameOverText.KeyDown += gameOverText_KeyDown;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(600, 800);
            Controls.Add(labelLoading);
            Controls.Add(gameOverText);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GameForm";
            Paint += OnPaint;
            KeyDown += GameForm_KeyDown;
            KeyUp += GameForm_KeyUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelLoading;
        private TextBox gameOverText;
        private System.Windows.Forms.Timer gameTimer;
    }
}