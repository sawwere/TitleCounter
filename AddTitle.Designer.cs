
namespace hltb
{
    partial class AddContent
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
            nameLabel = new Label();
            titlePicture = new PictureBox();
            addButton = new Button();
            button2 = new Button();
            statusLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)titlePicture).BeginInit();
            SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.Anchor = AnchorStyles.Left;
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            nameLabel.Location = new Point(5, 452);
            nameLabel.Margin = new Padding(4, 0, 4, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(70, 27);
            nameLabel.TabIndex = 11;
            nameLabel.Text = "Name";
            // 
            // titlePicture
            // 
            titlePicture.Location = new Point(68, 14);
            titlePicture.Margin = new Padding(4, 3, 4, 3);
            titlePicture.Name = "titlePicture";
            titlePicture.Size = new Size(280, 415);
            titlePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            titlePicture.TabIndex = 10;
            titlePicture.TabStop = false;
            // 
            // addButton
            // 
            addButton.DialogResult = DialogResult.OK;
            addButton.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            addButton.Location = new Point(68, 538);
            addButton.Margin = new Padding(4, 3, 4, 3);
            addButton.Name = "addButton";
            addButton.Size = new Size(124, 37);
            addButton.TabIndex = 20;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.Cancel;
            button2.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(198, 538);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(142, 37);
            button2.TabIndex = 21;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            statusLabel.Location = new Point(5, 503);
            statusLabel.Margin = new Padding(4, 0, 4, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(73, 27);
            statusLabel.TabIndex = 22;
            statusLabel.Text = "Status";
            // 
            // AddContent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(430, 606);
            Controls.Add(statusLabel);
            Controls.Add(button2);
            Controls.Add(addButton);
            Controls.Add(nameLabel);
            Controls.Add(titlePicture);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddContent";
            Text = "AddTitle";
            VisibleChanged += AddTitle_VisibleChanged;
            ((System.ComponentModel.ISupportInitialize)titlePicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label nameLabel;
        private PictureBox titlePicture;
        private Button addButton;
        private Button button2;
        private Label statusLabel;
    }
}