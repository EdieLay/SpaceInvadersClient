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
            labelLoading.Location = new Point(166, 343);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(284, 56);
            labelLoading.TabIndex = 0;
            labelLoading.Text = "loading...";
            // 
            // gameOverText
            // 
            gameOverText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gameOverText.BackColor = SystemColors.InfoText;
            gameOverText.Font = new Font("Consolas", 20F, FontStyle.Bold, GraphicsUnit.Point);
            gameOverText.ForeColor = SystemColors.HighlightText;
            gameOverText.Location = new Point(97, 330);
            gameOverText.Margin = new Padding(3, 2, 3, 2);
            gameOverText.Multiline = true;
            gameOverText.Name = "gameOverText";
            gameOverText.ReadOnly = true;
            gameOverText.ShortcutsEnabled = false;
            gameOverText.Size = new Size(421, 201);
            gameOverText.TabIndex = 1;
            gameOverText.TextAlign = HorizontalAlignment.Center;
            gameOverText.KeyDown += gameOverText_KeyDown;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(600, 800);
            Controls.Add(labelLoading);
            Controls.Add(gameOverText);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
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