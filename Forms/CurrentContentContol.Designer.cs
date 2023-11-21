
namespace hltb
{
    partial class CurrentContentContol
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
            titlePicture = new PictureBox();
            nameLabel = new Label();
            copyButton = new Button();
            timeLabel = new Label();
            releaseLabel = new Label();
            scoreLabel = new Label();
            score_c = new ComboBox();
            statusLabel = new Label();
            status_c = new ComboBox();
            deleteButton = new Button();
            timeHourLabel = new Label();
            timeMinuteLabel = new Label();
            timeHour = new TextBox();
            timeMinute = new TextBox();
            completitionLabel = new Label();
            competitionDay = new ComboBox();
            competitionMonth = new ComboBox();
            competitionYear = new TextBox();
            saveButton = new Button();
            noteTextBox = new TextBox();
            competiotionButtonToday = new Button();
            ((System.ComponentModel.ISupportInitialize)titlePicture).BeginInit();
            SuspendLayout();
            // 
            // titlePicture
            // 
            titlePicture.Anchor = AnchorStyles.Top;
            titlePicture.Location = new Point(92, 3);
            titlePicture.Margin = new Padding(4, 3, 4, 3);
            titlePicture.Name = "titlePicture";
            titlePicture.Size = new Size(280, 415);
            titlePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            titlePicture.TabIndex = 0;
            titlePicture.TabStop = false;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            nameLabel.ForeColor = Color.Black;
            nameLabel.Location = new Point(26, 440);
            nameLabel.Margin = new Padding(4, 0, 4, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(70, 27);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "Name";
            nameLabel.Click += nameLabel_Click;
            // 
            // copyButton
            // 
            copyButton.Location = new Point(376, 440);
            copyButton.Margin = new Padding(4, 3, 4, 3);
            copyButton.Name = "copyButton";
            copyButton.Size = new Size(88, 27);
            copyButton.TabIndex = 2;
            copyButton.Text = "Copy";
            copyButton.UseVisualStyleBackColor = true;
            copyButton.Click += copyButton_Click;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            timeLabel.ForeColor = Color.Black;
            timeLabel.Location = new Point(26, 471);
            timeLabel.Margin = new Padding(4, 0, 4, 0);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(56, 21);
            timeLabel.TabIndex = 3;
            timeLabel.Text = "Time: ";
            // 
            // releaseLabel
            // 
            releaseLabel.AutoSize = true;
            releaseLabel.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            releaseLabel.ForeColor = Color.Black;
            releaseLabel.Location = new Point(26, 502);
            releaseLabel.Margin = new Padding(4, 0, 4, 0);
            releaseLabel.Name = "releaseLabel";
            releaseLabel.Size = new Size(76, 21);
            releaseLabel.TabIndex = 4;
            releaseLabel.Text = "Release: ";
            // 
            // scoreLabel
            // 
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            scoreLabel.ForeColor = Color.Black;
            scoreLabel.Location = new Point(26, 533);
            scoreLabel.Margin = new Padding(4, 0, 4, 0);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(60, 21);
            scoreLabel.TabIndex = 5;
            scoreLabel.Text = "Score: ";
            // 
            // score_c
            // 
            score_c.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            score_c.FormattingEnabled = true;
            score_c.Location = new Point(125, 533);
            score_c.Margin = new Padding(4, 3, 4, 3);
            score_c.Name = "score_c";
            score_c.Size = new Size(140, 23);
            score_c.TabIndex = 6;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            statusLabel.ForeColor = Color.Black;
            statusLabel.Location = new Point(26, 564);
            statusLabel.Margin = new Padding(4, 0, 4, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(65, 21);
            statusLabel.TabIndex = 7;
            statusLabel.Text = "Status: ";
            // 
            // status_c
            // 
            status_c.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            status_c.FormattingEnabled = true;
            status_c.Location = new Point(125, 564);
            status_c.Margin = new Padding(4, 3, 4, 3);
            status_c.Name = "status_c";
            status_c.Size = new Size(140, 23);
            status_c.TabIndex = 8;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Top;
            deleteButton.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            deleteButton.Location = new Point(92, 760);
            deleteButton.Margin = new Padding(4, 3, 4, 3);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(280, 35);
            deleteButton.TabIndex = 9;
            deleteButton.Text = "Delete title";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // timeHourLabel
            // 
            timeHourLabel.AutoSize = true;
            timeHourLabel.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            timeHourLabel.Location = new Point(160, 471);
            timeHourLabel.Name = "timeHourLabel";
            timeHourLabel.Size = new Size(24, 21);
            timeHourLabel.TabIndex = 10;
            timeHourLabel.Text = "h.";
            // 
            // timeMinuteLabel
            // 
            timeMinuteLabel.AutoSize = true;
            timeMinuteLabel.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            timeMinuteLabel.Location = new Point(257, 471);
            timeMinuteLabel.Name = "timeMinuteLabel";
            timeMinuteLabel.Size = new Size(29, 21);
            timeMinuteLabel.TabIndex = 11;
            timeMinuteLabel.Text = "m.";
            // 
            // timeHour
            // 
            timeHour.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            timeHour.Location = new Point(106, 471);
            timeHour.Name = "timeHour";
            timeHour.Size = new Size(55, 21);
            timeHour.TabIndex = 12;
            timeHour.Text = "0";
            timeHour.TextChanged += TimeHourCTextChanged;
            timeHour.KeyPress += TimeHourCKeyPress;
            // 
            // timeMinute
            // 
            timeMinute.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            timeMinute.Location = new Point(196, 471);
            timeMinute.Name = "timeMinute";
            timeMinute.Size = new Size(55, 21);
            timeMinute.TabIndex = 13;
            timeMinute.Text = "0";
            timeMinute.TextChanged += TimeHourCTextChanged;
            timeMinute.KeyPress += TimeHourCKeyPress;
            // 
            // completitionLabel
            // 
            completitionLabel.AutoSize = true;
            completitionLabel.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            completitionLabel.ForeColor = Color.Black;
            completitionLabel.Location = new Point(26, 593);
            completitionLabel.Margin = new Padding(4, 0, 4, 0);
            completitionLabel.Name = "completitionLabel";
            completitionLabel.Size = new Size(119, 21);
            completitionLabel.TabIndex = 14;
            completitionLabel.Text = "Completition: ";
            // 
            // competitionDay
            // 
            competitionDay.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            competitionDay.FormattingEnabled = true;
            competitionDay.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" });
            competitionDay.Location = new Point(173, 593);
            competitionDay.Name = "competitionDay";
            competitionDay.Size = new Size(54, 23);
            competitionDay.TabIndex = 15;
            // 
            // competitionMonth
            // 
            competitionMonth.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            competitionMonth.FormattingEnabled = true;
            competitionMonth.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            competitionMonth.Location = new Point(240, 593);
            competitionMonth.Name = "competitionMonth";
            competitionMonth.Size = new Size(54, 23);
            competitionMonth.TabIndex = 16;
            // 
            // competitionYear
            // 
            competitionYear.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            competitionYear.Location = new Point(300, 593);
            competitionYear.Name = "competitionYear";
            competitionYear.Size = new Size(70, 21);
            competitionYear.TabIndex = 17;
            competitionYear.Text = "0";
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Top;
            saveButton.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            saveButton.Location = new Point(92, 719);
            saveButton.Margin = new Padding(4, 3, 4, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(280, 35);
            saveButton.TabIndex = 18;
            saveButton.Text = "Save changes";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // noteTextBox
            // 
            noteTextBox.Font = new Font("Microsoft Tai Le", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            noteTextBox.Location = new Point(26, 634);
            noteTextBox.MaxLength = 255;
            noteTextBox.Multiline = true;
            noteTextBox.Name = "noteTextBox";
            noteTextBox.PlaceholderText = "Enter your note";
            noteTextBox.Size = new Size(412, 79);
            noteTextBox.TabIndex = 19;
            // 
            // competiotionButtonToday
            // 
            competiotionButtonToday.Location = new Point(376, 591);
            competiotionButtonToday.Name = "competiotionButtonToday";
            competiotionButtonToday.Size = new Size(75, 23);
            competiotionButtonToday.TabIndex = 20;
            competiotionButtonToday.Text = "Today";
            competiotionButtonToday.UseVisualStyleBackColor = true;
            competiotionButtonToday.Click += competiotionButtonToday_Click;
            // 
            // CurrentContentContol
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            Controls.Add(competiotionButtonToday);
            Controls.Add(noteTextBox);
            Controls.Add(saveButton);
            Controls.Add(competitionYear);
            Controls.Add(competitionMonth);
            Controls.Add(competitionDay);
            Controls.Add(completitionLabel);
            Controls.Add(timeMinute);
            Controls.Add(timeHour);
            Controls.Add(timeMinuteLabel);
            Controls.Add(timeHourLabel);
            Controls.Add(deleteButton);
            Controls.Add(status_c);
            Controls.Add(statusLabel);
            Controls.Add(score_c);
            Controls.Add(scoreLabel);
            Controls.Add(releaseLabel);
            Controls.Add(timeLabel);
            Controls.Add(copyButton);
            Controls.Add(nameLabel);
            Controls.Add(titlePicture);
            Margin = new Padding(4, 3, 4, 3);
            Name = "CurrentContentContol";
            Size = new Size(470, 800);
            ((System.ComponentModel.ISupportInitialize)titlePicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox titlePicture;
        private Label nameLabel;
        private Button copyButton;
        private Label timeLabel;
        private Label releaseLabel;
        private Label scoreLabel;
        private ComboBox score_c;
        private Label statusLabel;
        private ComboBox status_c;
        private Button deleteButton;
        private Label timeHourLabel;
        private Label timeMinuteLabel;
        private TextBox timeHour;
        private TextBox timeMinute;
        private Label completitionLabel;
        private ComboBox competitionDay;
        private ComboBox competitionMonth;
        private TextBox competitionYear;
        private Button saveButton;
        private TextBox noteTextBox;
        private Button competiotionButtonToday;
    }
}
