namespace Ex05.WinUI
{
    public partial class GameSettings
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
            this.BoardSizeButton = new System.Windows.Forms.Button();
            this.PlayAgainstCompButton = new System.Windows.Forms.Button();
            this.PlayAgainstFriendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BoardSizeButton
            // 
            this.BoardSizeButton.Location = new System.Drawing.Point(28, 31);
            this.BoardSizeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BoardSizeButton.Name = "BoardSizeButton";
            this.BoardSizeButton.Size = new System.Drawing.Size(454, 86);
            this.BoardSizeButton.TabIndex = 0;
            this.BoardSizeButton.Text = "Board Size: 6X6  (click to increase)";
            this.BoardSizeButton.UseVisualStyleBackColor = true;
            this.BoardSizeButton.Click += new System.EventHandler(this.BoardSizeButton_Click);
            // 
            // PlayAgainstCompButton
            // 
            this.PlayAgainstCompButton.Location = new System.Drawing.Point(32, 156);
            this.PlayAgainstCompButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PlayAgainstCompButton.Name = "PlayAgainstCompButton";
            this.PlayAgainstCompButton.Size = new System.Drawing.Size(214, 59);
            this.PlayAgainstCompButton.TabIndex = 1;
            this.PlayAgainstCompButton.Text = "Play against the computer";
            this.PlayAgainstCompButton.UseVisualStyleBackColor = true;
            this.PlayAgainstCompButton.Click += new System.EventHandler(this.PlayAgainstCompButton_Click);
            // 
            // PlayAgainstFriendButton
            // 
            this.PlayAgainstFriendButton.Location = new System.Drawing.Point(264, 156);
            this.PlayAgainstFriendButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PlayAgainstFriendButton.Name = "PlayAgainstFriendButton";
            this.PlayAgainstFriendButton.Size = new System.Drawing.Size(218, 59);
            this.PlayAgainstFriendButton.TabIndex = 2;
            this.PlayAgainstFriendButton.Text = "Play against your friend";
            this.PlayAgainstFriendButton.UseVisualStyleBackColor = true;
            this.PlayAgainstFriendButton.Click += new System.EventHandler(this.PlayAgainstFriendButton_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 256);
            this.Controls.Add(this.PlayAgainstFriendButton);
            this.Controls.Add(this.PlayAgainstCompButton);
            this.Controls.Add(this.BoardSizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.Text = "Othelo - GameSettings";
            this.Load += new System.EventHandler(this.GameSettings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BoardSizeButton;
        private System.Windows.Forms.Button PlayAgainstCompButton;
        private System.Windows.Forms.Button PlayAgainstFriendButton;
    }
}