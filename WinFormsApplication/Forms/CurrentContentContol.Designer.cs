
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
            components = new System.ComponentModel.Container();
            titlePicture = new PictureBox();
            nameLabel = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            redirectToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
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
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // titlePicture
            // 
            titlePicture.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titlePicture.Location = new Point(56, 0);
            titlePicture.Margin = new Padding(4, 3, 4, 3);
            titlePicture.Name = "titlePicture";
            titlePicture.Size = new Size(280, 415);
            titlePicture.SizeMode = PictureBoxSizeMode.Zoom;
            titlePicture.TabIndex = 0;
            titlePicture.TabStop = false;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.BackColor = Color.DimGray;
            nameLabel.ContextMenuStrip = contextMenuStrip1;
            nameLabel.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            nameLabel.ForeColor = Color.SeaShell;
            nameLabel.Location = new Point(26, 440);
            nameLabel.Margin = new Padding(4, 0, 4, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(70, 27);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "Name";
            nameLabel.Click += nameLabel_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.BackColor = Color.FromArgb(64, 64, 64);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { redirectToolStripMenuItem, copyToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.ShowImageMargin = false;
            contextMenuStrip1.Size = new Size(93, 48);
            // 
            // redirectToolStripMenuItem
            // 
            redirectToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            redirectToolStripMenuItem.ForeColor = Color.SeaShell;
            redirectToolStripMenuItem.Name = "redirectToolStripMenuItem";
            redirectToolStripMenuItem.Size = new Size(92, 22);
            redirectToolStripMenuItem.Text = "Redirect";
            redirectToolStripMenuItem.Click += nameLabel_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            copyToolStripMenuItem.ForeColor = Color.SeaShell;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(92, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyButton_Click;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            timeLabel.ForeColor = Color.SeaShell;
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
            releaseLabel.ForeColor = Color.SeaShell;
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
            scoreLabel.ForeColor = Color.SeaShell;
            scoreLabel.Location = new Point(26, 533);
            scoreLabel.Margin = new Padding(4, 0, 4, 0);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(60, 21);
            scoreLabel.TabIndex = 5;
            scoreLabel.Text = "Score: ";
            // 
            // score_c
            // 
            score_c.BackColor = Color.FromArgb(42, 42, 42);
            score_c.DropDownStyle = ComboBoxStyle.DropDownList;
            score_c.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            score_c.ForeColor = Color.SeaShell;
            score_c.FormattingEnabled = true;
            score_c.Location = new Point(125, 533);
            score_c.Margin = new Padding(4, 3, 4, 3);
            score_c.Name = "score_c";
            score_c.Size = new Size(114, 23);
            score_c.TabIndex = 6;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Microsoft Tai Le", 12F, FontStyle.Bold, GraphicsUnit.Point);
            statusLabel.ForeColor = Color.SeaShell;
            statusLabel.Location = new Point(26, 564);
            statusLabel.Margin = new Padding(4, 0, 4, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(65, 21);
            statusLabel.TabIndex = 7;
            statusLabel.Text = "Status: ";
            // 
            // status_c
            // 
            status_c.BackColor = Color.FromArgb(42, 42, 42);
            status_c.DropDownStyle = ComboBoxStyle.DropDownList;
            status_c.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            status_c.ForeColor = Color.SeaShell;
            status_c.FormattingEnabled = true;
            status_c.Location = new Point(125, 564);
            status_c.Margin = new Padding(4, 3, 4, 3);
            status_c.Name = "status_c";
            status_c.Size = new Size(114, 23);
            status_c.TabIndex = 8;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            deleteButton.BackColor = Color.Gray;
            deleteButton.FlatStyle = FlatStyle.Popup;
            deleteButton.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            deleteButton.ForeColor = Color.SeaShell;
            deleteButton.Location = new Point(56, 754);
            deleteButton.Margin = new Padding(4, 3, 4, 3);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(280, 35);
            deleteButton.TabIndex = 9;
            deleteButton.Text = "Delete title";
            deleteButton.UseVisualStyleBackColor = false;
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
            timeHour.BackColor = Color.FromArgb(42, 42, 42);
            timeHour.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            timeHour.ForeColor = Color.SeaShell;
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
            timeMinute.BackColor = Color.FromArgb(42, 42, 42);
            timeMinute.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            timeMinute.ForeColor = Color.SeaShell;
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
            completitionLabel.ForeColor = Color.SeaShell;
            completitionLabel.Location = new Point(26, 593);
            completitionLabel.Margin = new Padding(4, 0, 4, 0);
            completitionLabel.Name = "completitionLabel";
            completitionLabel.Size = new Size(102, 21);
            completitionLabel.TabIndex = 14;
            completitionLabel.Text = "Completed: ";
            // 
            // competitionDay
            // 
            competitionDay.BackColor = Color.FromArgb(42, 42, 42);
            competitionDay.DropDownStyle = ComboBoxStyle.DropDownList;
            competitionDay.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            competitionDay.ForeColor = Color.SeaShell;
            competitionDay.FormattingEnabled = true;
            competitionDay.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" });
            competitionDay.Location = new Point(125, 593);
            competitionDay.Name = "competitionDay";
            competitionDay.Size = new Size(54, 23);
            competitionDay.TabIndex = 15;
            // 
            // competitionMonth
            // 
            competitionMonth.BackColor = Color.FromArgb(42, 42, 42);
            competitionMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            competitionMonth.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            competitionMonth.ForeColor = Color.SeaShell;
            competitionMonth.FormattingEnabled = true;
            competitionMonth.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            competitionMonth.Location = new Point(185, 593);
            competitionMonth.Name = "competitionMonth";
            competitionMonth.Size = new Size(54, 23);
            competitionMonth.TabIndex = 16;
            // 
            // competitionYear
            // 
            competitionYear.BackColor = Color.FromArgb(42, 42, 42);
            competitionYear.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            competitionYear.ForeColor = Color.SeaShell;
            competitionYear.Location = new Point(245, 595);
            competitionYear.Name = "competitionYear";
            competitionYear.Size = new Size(70, 21);
            competitionYear.TabIndex = 17;
            competitionYear.Text = "0";
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            saveButton.BackColor = Color.Gray;
            saveButton.FlatStyle = FlatStyle.Popup;
            saveButton.Font = new Font("Microsoft Tai Le", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            saveButton.ForeColor = Color.SeaShell;
            saveButton.Location = new Point(56, 713);
            saveButton.Margin = new Padding(4, 3, 4, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(280, 35);
            saveButton.TabIndex = 18;
            saveButton.Text = "Save changes";
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += saveButton_Click;
            // 
            // noteTextBox
            // 
            noteTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            noteTextBox.BackColor = Color.FromArgb(42, 42, 42);
            noteTextBox.Font = new Font("Microsoft Tai Le", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            noteTextBox.ForeColor = Color.SeaShell;
            noteTextBox.Location = new Point(12, 622);
            noteTextBox.MaxLength = 255;
            noteTextBox.Multiline = true;
            noteTextBox.Name = "noteTextBox";
            noteTextBox.PlaceholderText = "Enter your note";
            noteTextBox.Size = new Size(368, 85);
            noteTextBox.TabIndex = 19;
            // 
            // competiotionButtonToday
            // 
            competiotionButtonToday.BackColor = Color.Gray;
            competiotionButtonToday.FlatAppearance.BorderSize = 0;
            competiotionButtonToday.FlatStyle = FlatStyle.Flat;
            competiotionButtonToday.ForeColor = Color.SeaShell;
            competiotionButtonToday.Location = new Point(321, 593);
            competiotionButtonToday.Name = "competiotionButtonToday";
            competiotionButtonToday.Size = new Size(72, 23);
            competiotionButtonToday.TabIndex = 20;
            competiotionButtonToday.Text = "Today";
            competiotionButtonToday.UseVisualStyleBackColor = false;
            competiotionButtonToday.Click += competiotionButtonToday_Click;
            // 
            // CurrentContentContol
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.DimGray;
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
            Controls.Add(nameLabel);
            Controls.Add(titlePicture);
            ForeColor = Color.SeaShell;
            Margin = new Padding(4, 3, 4, 3);
            Name = "CurrentContentContol";
            Size = new Size(396, 792);
            ((System.ComponentModel.ISupportInitialize)titlePicture).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox titlePicture;
        private Label nameLabel;
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
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem redirectToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
    }
}
