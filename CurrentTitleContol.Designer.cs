
namespace hltb
{
    partial class CurrentTitleContol
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.titlePicture = new System.Windows.Forms.PictureBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.copyButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.score_c = new System.Windows.Forms.ComboBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.status_c = new System.Windows.Forms.ComboBox();
            this.deleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.titlePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // titlePicture
            // 
            this.titlePicture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.titlePicture.Location = new System.Drawing.Point(77, 3);
            this.titlePicture.Name = "titlePicture";
            this.titlePicture.Size = new System.Drawing.Size(240, 360);
            this.titlePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.titlePicture.TabIndex = 0;
            this.titlePicture.TabStop = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(22, 381);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(70, 27);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(322, 381);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(75, 23);
            this.copyButton.TabIndex = 2;
            this.copyButton.Text = "Copy";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(22, 408);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(72, 27);
            this.timeLabel.TabIndex = 3;
            this.timeLabel.Text = "Time: ";
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearLabel.Location = new System.Drawing.Point(22, 435);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(67, 27);
            this.yearLabel.TabIndex = 4;
            this.yearLabel.Text = "Year: ";
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.Location = new System.Drawing.Point(22, 462);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(78, 27);
            this.scoreLabel.TabIndex = 5;
            this.scoreLabel.Text = "Score: ";
            // 
            // score_c
            // 
            this.score_c.FormattingEnabled = true;
            this.score_c.Location = new System.Drawing.Point(107, 462);
            this.score_c.Name = "score_c";
            this.score_c.Size = new System.Drawing.Size(121, 21);
            this.score_c.TabIndex = 6;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(22, 489);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(85, 27);
            this.statusLabel.TabIndex = 7;
            this.statusLabel.Text = "Status: ";
            // 
            // status_c
            // 
            this.status_c.FormattingEnabled = true;
            this.status_c.Location = new System.Drawing.Point(106, 489);
            this.status_c.Name = "status_c";
            this.status_c.Size = new System.Drawing.Size(121, 21);
            this.status_c.TabIndex = 8;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(77, 628);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(240, 30);
            this.deleteButton.TabIndex = 9;
            this.deleteButton.Text = "Delete title";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // CurrentTitleContol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.status_c);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.score_c);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.yearLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.titlePicture);
            this.Name = "CurrentTitleContol";
            this.Size = new System.Drawing.Size(400, 700);
            ((System.ComponentModel.ISupportInitialize)(this.titlePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox titlePicture;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.ComboBox score_c;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox status_c;
        private System.Windows.Forms.Button deleteButton;
    }
}
