namespace hltb.Forms
{
    partial class EntryCreationListElement
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
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            roundedButton1 = new RoundedButton(components);
            label2 = new Label();
            yearLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(108, 129);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(117, 3);
            label1.Name = "label1";
            label1.Size = new Size(69, 60);
            label1.TabIndex = 1;
            label1.Text = "Name\r\n2";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = SystemColors.ControlDarkDark;
            roundedButton1.BackgroundImageLayout = ImageLayout.Stretch;
            roundedButton1.DialogResult = DialogResult.OK;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            roundedButton1.ForeColor = Color.SeaShell;
            roundedButton1.Location = new Point(364, 93);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(88, 38);
            roundedButton1.TabIndex = 2;
            roundedButton1.Text = "Add";
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(117, 93);
            label2.Name = "label2";
            label2.Size = new Size(67, 25);
            label2.TabIndex = 3;
            label2.Text = "Status";
            // 
            // yearLabel
            // 
            yearLabel.AutoSize = true;
            yearLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            yearLabel.Location = new Point(117, 63);
            yearLabel.Name = "yearLabel";
            yearLabel.Size = new Size(52, 30);
            yearLabel.TabIndex = 4;
            yearLabel.Text = "Year";
            // 
            // EntryCreationListElement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(yearLabel);
            Controls.Add(label2);
            Controls.Add(roundedButton1);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "EntryCreationListElement";
            Size = new Size(455, 134);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private RoundedButton roundedButton1;
        private Label label2;
        private Label yearLabel;
    }
}
