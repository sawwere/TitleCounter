
namespace hltb
{
    partial class Mainform
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.addgame = new System.Windows.Forms.Button();
            this.operationLabel = new System.Windows.Forms.Label();
            this.namebox = new System.Windows.Forms.TextBox();
            this.statusbox = new System.Windows.Forms.ComboBox();
            this.YearSortBox = new System.Windows.Forms.ComboBox();
            this.ByYearButton = new System.Windows.Forms.Button();
            this.ByScoreButton = new System.Windows.Forms.Button();
            this.statisticsLabel = new System.Windows.Forms.Label();
            this.ByStatusButton = new System.Windows.Forms.Button();
            this.scorebox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ModeBox = new System.Windows.Forms.ListBox();
            this.ByGenreButton = new System.Windows.Forms.Button();
            this.ScoreSortBox = new System.Windows.Forms.ComboBox();
            this.GenreSortBox = new System.Windows.Forms.ComboBox();
            this.StatusSortBox = new System.Windows.Forms.ComboBox();
            this.currentTitlePanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ByNameButton = new System.Windows.Forms.Button();
            this.NameSortBox = new System.Windows.Forms.ComboBox();
            this.currentTitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // addgame
            // 
            this.addgame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.addgame.Location = new System.Drawing.Point(84, 26);
            this.addgame.Name = "addgame";
            this.addgame.Size = new System.Drawing.Size(133, 23);
            this.addgame.TabIndex = 0;
            this.addgame.Text = "Add titile";
            this.addgame.UseVisualStyleBackColor = true;
            this.addgame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.addtitle_MouseDown);
            // 
            // operationLabel
            // 
            this.operationLabel.AutoSize = true;
            this.operationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.operationLabel.Location = new System.Drawing.Point(84, 49);
            this.operationLabel.Name = "operationLabel";
            this.operationLabel.Size = new System.Drawing.Size(57, 20);
            this.operationLabel.TabIndex = 1;
            this.operationLabel.Text = "status:";
            // 
            // namebox
            // 
            this.namebox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.namebox.Location = new System.Drawing.Point(225, 26);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(276, 23);
            this.namebox.TabIndex = 2;
            // 
            // statusbox
            // 
            this.statusbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.statusbox.FormattingEnabled = true;
            this.statusbox.Items.AddRange(new object[] {
            "completed",
            "backlog",
            "retired"});
            this.statusbox.Location = new System.Drawing.Point(507, 25);
            this.statusbox.Name = "statusbox";
            this.statusbox.Size = new System.Drawing.Size(121, 24);
            this.statusbox.TabIndex = 6;
            this.statusbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.statusbox_KeyPress);
            // 
            // YearSortBox
            // 
            this.YearSortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.YearSortBox.FormattingEnabled = true;
            this.YearSortBox.Location = new System.Drawing.Point(84, 105);
            this.YearSortBox.Name = "YearSortBox";
            this.YearSortBox.Size = new System.Drawing.Size(135, 24);
            this.YearSortBox.TabIndex = 8;
            this.YearSortBox.Text = "Year";
            this.YearSortBox.SelectedValueChanged += new System.EventHandler(this.YearSortBox_SelectedValueChanged);
            // 
            // ByYearButton
            // 
            this.ByYearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ByYearButton.Location = new System.Drawing.Point(84, 72);
            this.ByYearButton.Name = "ByYearButton";
            this.ByYearButton.Size = new System.Drawing.Size(135, 27);
            this.ByYearButton.TabIndex = 10;
            this.ByYearButton.Text = "By Year";
            this.ByYearButton.UseVisualStyleBackColor = true;
            this.ByYearButton.Click += new System.EventHandler(this.ByYearButton_Click);
            // 
            // ByScoreButton
            // 
            this.ByScoreButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ByScoreButton.Location = new System.Drawing.Point(225, 72);
            this.ByScoreButton.Name = "ByScoreButton";
            this.ByScoreButton.Size = new System.Drawing.Size(135, 27);
            this.ByScoreButton.TabIndex = 11;
            this.ByScoreButton.Text = "By Score";
            this.ByScoreButton.UseVisualStyleBackColor = true;
            this.ByScoreButton.Click += new System.EventHandler(this.ByScoreButton_Click);
            // 
            // statisticsLabel
            // 
            this.statisticsLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.statisticsLabel.AutoSize = true;
            this.statisticsLabel.Location = new System.Drawing.Point(727, 600);
            this.statisticsLabel.Name = "statisticsLabel";
            this.statisticsLabel.Size = new System.Drawing.Size(35, 13);
            this.statisticsLabel.TabIndex = 16;
            this.statisticsLabel.Text = "label2";
            // 
            // ByStatusButton
            // 
            this.ByStatusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ByStatusButton.Location = new System.Drawing.Point(507, 72);
            this.ByStatusButton.Name = "ByStatusButton";
            this.ByStatusButton.Size = new System.Drawing.Size(121, 27);
            this.ByStatusButton.TabIndex = 17;
            this.ByStatusButton.Text = "By Status";
            this.ByStatusButton.UseVisualStyleBackColor = true;
            this.ByStatusButton.Click += new System.EventHandler(this.ByStatusButton_Click);
            // 
            // scorebox
            // 
            this.scorebox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.scorebox.FormattingEnabled = true;
            this.scorebox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.scorebox.Location = new System.Drawing.Point(634, 25);
            this.scorebox.Name = "scorebox";
            this.scorebox.Size = new System.Drawing.Size(68, 24);
            this.scorebox.TabIndex = 18;
            this.scorebox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scorebox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(631, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Score";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(504, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Name";
            // 
            // ModeBox
            // 
            this.ModeBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ModeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ModeBox.FormattingEnabled = true;
            this.ModeBox.ItemHeight = 16;
            this.ModeBox.Items.AddRange(new object[] {
            "Games",
            "Films",
            "TvSeries"});
            this.ModeBox.Location = new System.Drawing.Point(12, 172);
            this.ModeBox.Name = "ModeBox";
            this.ModeBox.Size = new System.Drawing.Size(66, 52);
            this.ModeBox.TabIndex = 23;
            this.ModeBox.SelectedValueChanged += new System.EventHandler(this.ModeBox_SelectedValueChanged);
            // 
            // ByGenreButton
            // 
            this.ByGenreButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ByGenreButton.Location = new System.Drawing.Point(634, 72);
            this.ByGenreButton.Name = "ByGenreButton";
            this.ByGenreButton.Size = new System.Drawing.Size(135, 27);
            this.ByGenreButton.TabIndex = 25;
            this.ByGenreButton.Text = "By Genre";
            this.ByGenreButton.UseVisualStyleBackColor = true;
            this.ByGenreButton.Visible = false;
            this.ByGenreButton.Click += new System.EventHandler(this.ByGenreButton_Click);
            // 
            // ScoreSortBox
            // 
            this.ScoreSortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ScoreSortBox.FormattingEnabled = true;
            this.ScoreSortBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.ScoreSortBox.Location = new System.Drawing.Point(225, 105);
            this.ScoreSortBox.Name = "ScoreSortBox";
            this.ScoreSortBox.Size = new System.Drawing.Size(135, 24);
            this.ScoreSortBox.TabIndex = 26;
            this.ScoreSortBox.Text = "Score";
            this.ScoreSortBox.Visible = false;
            this.ScoreSortBox.SelectedValueChanged += new System.EventHandler(this.ScoreSortBox_SelectedValueChanged);
            // 
            // GenreSortBox
            // 
            this.GenreSortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.GenreSortBox.FormattingEnabled = true;
            this.GenreSortBox.Location = new System.Drawing.Point(634, 104);
            this.GenreSortBox.Name = "GenreSortBox";
            this.GenreSortBox.Size = new System.Drawing.Size(135, 24);
            this.GenreSortBox.TabIndex = 27;
            this.GenreSortBox.Text = "Genre";
            this.GenreSortBox.Visible = false;
            this.GenreSortBox.SelectedValueChanged += new System.EventHandler(this.GenreSortBox_SelectedValueChanged);
            // 
            // StatusSortBox
            // 
            this.StatusSortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.StatusSortBox.FormattingEnabled = true;
            this.StatusSortBox.Items.AddRange(new object[] {
            "Backlog",
            "Completed",
            "Retired"});
            this.StatusSortBox.Location = new System.Drawing.Point(507, 104);
            this.StatusSortBox.Name = "StatusSortBox";
            this.StatusSortBox.Size = new System.Drawing.Size(121, 24);
            this.StatusSortBox.TabIndex = 28;
            this.StatusSortBox.Text = "Status";
            this.StatusSortBox.Visible = false;
            this.StatusSortBox.SelectedValueChanged += new System.EventHandler(this.StatusSortBox_SelectedValueChanged);
            // 
            // currentTitlePanel
            // 
            this.currentTitlePanel.Controls.Add(this.pictureBox1);
            this.currentTitlePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.currentTitlePanel.Location = new System.Drawing.Point(984, 0);
            this.currentTitlePanel.Name = "currentTitlePanel";
            this.currentTitlePanel.Size = new System.Drawing.Size(400, 661);
            this.currentTitlePanel.TabIndex = 29;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(36, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ByNameButton
            // 
            this.ByNameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ByNameButton.Location = new System.Drawing.Point(366, 72);
            this.ByNameButton.Name = "ByNameButton";
            this.ByNameButton.Size = new System.Drawing.Size(135, 27);
            this.ByNameButton.TabIndex = 30;
            this.ByNameButton.Text = "By Name";
            this.ByNameButton.UseVisualStyleBackColor = true;
            // 
            // NameSortBox
            // 
            this.NameSortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.NameSortBox.FormattingEnabled = true;
            this.NameSortBox.Items.AddRange(new object[] {
            "Increasing",
            "Descending"});
            this.NameSortBox.Location = new System.Drawing.Point(366, 104);
            this.NameSortBox.Name = "NameSortBox";
            this.NameSortBox.Size = new System.Drawing.Size(135, 24);
            this.NameSortBox.TabIndex = 31;
            this.NameSortBox.Text = "Name starts with";
            this.NameSortBox.Visible = false;
            this.NameSortBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1384, 661);
            this.Controls.Add(this.currentTitlePanel);
            this.Controls.Add(this.NameSortBox);
            this.Controls.Add(this.ByNameButton);
            this.Controls.Add(this.StatusSortBox);
            this.Controls.Add(this.GenreSortBox);
            this.Controls.Add(this.ScoreSortBox);
            this.Controls.Add(this.ByGenreButton);
            this.Controls.Add(this.ModeBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.scorebox);
            this.Controls.Add(this.ByStatusButton);
            this.Controls.Add(this.statisticsLabel);
            this.Controls.Add(this.ByScoreButton);
            this.Controls.Add(this.ByYearButton);
            this.Controls.Add(this.YearSortBox);
            this.Controls.Add(this.statusbox);
            this.Controls.Add(this.namebox);
            this.Controls.Add(this.operationLabel);
            this.Controls.Add(this.addgame);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Name = "Mainform";
            this.Text = "MyList";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.currentTitlePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addgame;
        private System.Windows.Forms.Label operationLabel;
        private System.Windows.Forms.TextBox namebox;
        private System.Windows.Forms.ComboBox statusbox;
        private System.Windows.Forms.ComboBox YearSortBox;
        private System.Windows.Forms.Button ByYearButton;
        private System.Windows.Forms.Button ByScoreButton;
        private System.Windows.Forms.Label statisticsLabel;
        private System.Windows.Forms.Button ByStatusButton;
        private System.Windows.Forms.ComboBox scorebox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox ModeBox;
        private System.Windows.Forms.Button ByGenreButton;
        private System.Windows.Forms.ComboBox ScoreSortBox;
        private System.Windows.Forms.ComboBox GenreSortBox;
        private System.Windows.Forms.ComboBox StatusSortBox;
        private System.Windows.Forms.Panel currentTitlePanel;
        private System.Windows.Forms.Button ByNameButton;
        private System.Windows.Forms.ComboBox NameSortBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

