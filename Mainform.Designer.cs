
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
            addgame = new Button();
            operationLabel = new Label();
            namebox = new TextBox();
            statusbox = new ComboBox();
            YearSortBox = new ComboBox();
            ByYearButton = new Button();
            ByScoreButton = new Button();
            statisticsLabel = new Label();
            ByStatusButton = new Button();
            scorebox = new ComboBox();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            ModeBox = new ListBox();
            ByGenreButton = new Button();
            ScoreSortBox = new ComboBox();
            GenreSortBox = new ComboBox();
            StatusSortBox = new ComboBox();
            currentTitlePanel = new Panel();
            ByNameButton = new Button();
            NameSortBox = new ComboBox();
            refreshButton = new Button();
            displayLinesButton = new Button();
            displayImagesButton = new Button();
            displayButtonsButton = new Button();
            SuspendLayout();
            // 
            // addgame
            // 
            addgame.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addgame.Location = new Point(84, 26);
            addgame.Name = "addgame";
            addgame.Size = new Size(133, 23);
            addgame.TabIndex = 0;
            addgame.Text = "Add titile";
            addgame.UseVisualStyleBackColor = true;
            addgame.MouseDown += addtitle_MouseDown;
            // 
            // operationLabel
            // 
            operationLabel.AutoSize = true;
            operationLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            operationLabel.Location = new Point(84, 49);
            operationLabel.Name = "operationLabel";
            operationLabel.Size = new Size(57, 20);
            operationLabel.TabIndex = 1;
            operationLabel.Text = "status:";
            // 
            // namebox
            // 
            namebox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            namebox.Location = new Point(225, 26);
            namebox.Name = "namebox";
            namebox.Size = new Size(276, 23);
            namebox.TabIndex = 2;
            // 
            // statusbox
            // 
            statusbox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            statusbox.FormattingEnabled = true;
            statusbox.Items.AddRange(new object[] { "COMPLETED", "BACKLOG", "RETIRED" });
            statusbox.Location = new Point(507, 25);
            statusbox.Name = "statusbox";
            statusbox.Size = new Size(121, 24);
            statusbox.TabIndex = 6;
            statusbox.KeyPress += statusbox_KeyPress;
            // 
            // YearSortBox
            // 
            YearSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            YearSortBox.FormattingEnabled = true;
            YearSortBox.Location = new Point(84, 105);
            YearSortBox.Name = "YearSortBox";
            YearSortBox.Size = new Size(135, 24);
            YearSortBox.TabIndex = 8;
            YearSortBox.Text = "Year";
            YearSortBox.SelectedValueChanged += YearSortBox_SelectedValueChanged;
            // 
            // ByYearButton
            // 
            ByYearButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByYearButton.Location = new Point(84, 72);
            ByYearButton.Name = "ByYearButton";
            ByYearButton.Size = new Size(135, 27);
            ByYearButton.TabIndex = 10;
            ByYearButton.Text = "By Year";
            ByYearButton.UseVisualStyleBackColor = true;
            ByYearButton.Click += ByYearButton_Click;
            // 
            // ByScoreButton
            // 
            ByScoreButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByScoreButton.Location = new Point(225, 72);
            ByScoreButton.Name = "ByScoreButton";
            ByScoreButton.Size = new Size(135, 27);
            ByScoreButton.TabIndex = 11;
            ByScoreButton.Text = "By Score";
            ByScoreButton.UseVisualStyleBackColor = true;
            ByScoreButton.Click += ByScoreButton_Click;
            // 
            // statisticsLabel
            // 
            statisticsLabel.Anchor = AnchorStyles.Bottom;
            statisticsLabel.AutoSize = true;
            statisticsLabel.Location = new Point(735, 740);
            statisticsLabel.Name = "statisticsLabel";
            statisticsLabel.Size = new Size(35, 13);
            statisticsLabel.TabIndex = 16;
            statisticsLabel.Text = "label2";
            // 
            // ByStatusButton
            // 
            ByStatusButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByStatusButton.Location = new Point(507, 72);
            ByStatusButton.Name = "ByStatusButton";
            ByStatusButton.Size = new Size(121, 27);
            ByStatusButton.TabIndex = 17;
            ByStatusButton.Text = "By Status";
            ByStatusButton.UseVisualStyleBackColor = true;
            ByStatusButton.Click += ByStatusButton_Click;
            // 
            // scorebox
            // 
            scorebox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            scorebox.FormattingEnabled = true;
            scorebox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            scorebox.Location = new Point(634, 25);
            scorebox.Name = "scorebox";
            scorebox.Size = new Size(68, 24);
            scorebox.TabIndex = 18;
            scorebox.KeyPress += scorebox_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(631, 9);
            label3.Name = "label3";
            label3.Size = new Size(35, 13);
            label3.TabIndex = 19;
            label3.Text = "Score";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(504, 9);
            label5.Name = "label5";
            label5.Size = new Size(37, 13);
            label5.TabIndex = 21;
            label5.Text = "Status";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(222, 9);
            label6.Name = "label6";
            label6.Size = new Size(35, 13);
            label6.TabIndex = 22;
            label6.Text = "Name";
            // 
            // ModeBox
            // 
            ModeBox.Anchor = AnchorStyles.Left;
            ModeBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ModeBox.FormattingEnabled = true;
            ModeBox.ItemHeight = 16;
            ModeBox.Items.AddRange(new object[] { "Games", "Films", "TvSeries" });
            ModeBox.Location = new Point(12, 242);
            ModeBox.Name = "ModeBox";
            ModeBox.Size = new Size(66, 52);
            ModeBox.TabIndex = 23;
            ModeBox.SelectedValueChanged += ModeBox_SelectedValueChanged;
            // 
            // ByGenreButton
            // 
            ByGenreButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByGenreButton.Location = new Point(634, 72);
            ByGenreButton.Name = "ByGenreButton";
            ByGenreButton.Size = new Size(135, 27);
            ByGenreButton.TabIndex = 25;
            ByGenreButton.Text = "By Genre";
            ByGenreButton.UseVisualStyleBackColor = true;
            ByGenreButton.Visible = false;
            ByGenreButton.Click += ByGenreButton_Click;
            // 
            // ScoreSortBox
            // 
            ScoreSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ScoreSortBox.FormattingEnabled = true;
            ScoreSortBox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            ScoreSortBox.Location = new Point(225, 105);
            ScoreSortBox.Name = "ScoreSortBox";
            ScoreSortBox.Size = new Size(135, 24);
            ScoreSortBox.TabIndex = 26;
            ScoreSortBox.Text = "Score";
            ScoreSortBox.Visible = false;
            ScoreSortBox.SelectedValueChanged += YearSortBox_SelectedValueChanged;
            // 
            // GenreSortBox
            // 
            GenreSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            GenreSortBox.FormattingEnabled = true;
            GenreSortBox.Location = new Point(634, 104);
            GenreSortBox.Name = "GenreSortBox";
            GenreSortBox.Size = new Size(135, 24);
            GenreSortBox.TabIndex = 27;
            GenreSortBox.Text = "Genre";
            GenreSortBox.Visible = false;
            GenreSortBox.SelectedValueChanged += YearSortBox_SelectedValueChanged;
            // 
            // StatusSortBox
            // 
            StatusSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            StatusSortBox.FormattingEnabled = true;
            StatusSortBox.Items.AddRange(new object[] { "Backlog", "Completed", "Retired", "In progress" });
            StatusSortBox.Location = new Point(507, 104);
            StatusSortBox.Name = "StatusSortBox";
            StatusSortBox.Size = new Size(121, 24);
            StatusSortBox.TabIndex = 28;
            StatusSortBox.Text = "Status";
            StatusSortBox.Visible = false;
            StatusSortBox.SelectedValueChanged += YearSortBox_SelectedValueChanged;
            // 
            // currentTitlePanel
            // 
            currentTitlePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            currentTitlePanel.AutoSize = true;
            currentTitlePanel.Location = new Point(1000, 0);
            currentTitlePanel.Name = "currentTitlePanel";
            currentTitlePanel.Size = new Size(400, 800);
            currentTitlePanel.TabIndex = 29;
            // 
            // ByNameButton
            // 
            ByNameButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByNameButton.Location = new Point(366, 72);
            ByNameButton.Name = "ByNameButton";
            ByNameButton.Size = new Size(135, 27);
            ByNameButton.TabIndex = 30;
            ByNameButton.Text = "By Name";
            ByNameButton.UseVisualStyleBackColor = true;
            ByNameButton.Click += ByNameButton_Click;
            // 
            // NameSortBox
            // 
            NameSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            NameSortBox.FormattingEnabled = true;
            NameSortBox.Items.AddRange(new object[] { "Increasing", "Descending" });
            NameSortBox.Location = new Point(366, 104);
            NameSortBox.Name = "NameSortBox";
            NameSortBox.Size = new Size(135, 24);
            NameSortBox.TabIndex = 31;
            NameSortBox.Text = "Name starts with";
            NameSortBox.Visible = false;
            NameSortBox.SelectedIndexChanged += YearSortBox_SelectedValueChanged;
            // 
            // refreshButton
            // 
            refreshButton.BackgroundImage = Properties.Resources.refresh_icon;
            refreshButton.BackgroundImageLayout = ImageLayout.Stretch;
            refreshButton.Location = new Point(841, 12);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(86, 57);
            refreshButton.TabIndex = 32;
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // displayLinesButton
            // 
            displayLinesButton.Location = new Point(169, 136);
            displayLinesButton.Name = "displayLinesButton";
            displayLinesButton.Size = new Size(75, 23);
            displayLinesButton.TabIndex = 33;
            displayLinesButton.Text = "lines";
            displayLinesButton.UseVisualStyleBackColor = true;
            displayLinesButton.Click += displayLinesButton_Click;
            // 
            // displayImagesButton
            // 
            displayImagesButton.Location = new Point(250, 136);
            displayImagesButton.Name = "displayImagesButton";
            displayImagesButton.Size = new Size(75, 23);
            displayImagesButton.TabIndex = 34;
            displayImagesButton.Text = "greed";
            displayImagesButton.UseVisualStyleBackColor = true;
            displayImagesButton.Click += displayImagesButton_Click;
            // 
            // displayButtonsButton
            // 
            displayButtonsButton.Location = new Point(88, 136);
            displayButtonsButton.Name = "displayButtonsButton";
            displayButtonsButton.Size = new Size(75, 23);
            displayButtonsButton.TabIndex = 35;
            displayButtonsButton.Text = "button1";
            displayButtonsButton.UseVisualStyleBackColor = true;
            displayButtonsButton.Click += displayButtonsButton_Click;
            // 
            // Mainform
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1400, 801);
            Controls.Add(displayButtonsButton);
            Controls.Add(displayImagesButton);
            Controls.Add(displayLinesButton);
            Controls.Add(refreshButton);
            Controls.Add(currentTitlePanel);
            Controls.Add(NameSortBox);
            Controls.Add(ByNameButton);
            Controls.Add(StatusSortBox);
            Controls.Add(GenreSortBox);
            Controls.Add(ScoreSortBox);
            Controls.Add(ByGenreButton);
            Controls.Add(ModeBox);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(scorebox);
            Controls.Add(ByStatusButton);
            Controls.Add(statisticsLabel);
            Controls.Add(ByScoreButton);
            Controls.Add(ByYearButton);
            Controls.Add(YearSortBox);
            Controls.Add(statusbox);
            Controls.Add(namebox);
            Controls.Add(operationLabel);
            Controls.Add(addgame);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.DarkSlateGray;
            Name = "Mainform";
            Text = "MyList";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addgame;
        private Label operationLabel;
        private TextBox namebox;
        private ComboBox statusbox;
        private ComboBox YearSortBox;
        private Button ByYearButton;
        private Button ByScoreButton;
        private Label statisticsLabel;
        private Button ByStatusButton;
        private ComboBox scorebox;
        private Label label3;
        private Label label5;
        private Label label6;
        private ListBox ModeBox;
        private Button ByGenreButton;
        private ComboBox ScoreSortBox;
        private ComboBox GenreSortBox;
        private ComboBox StatusSortBox;
        private Panel currentTitlePanel;
        private Button ByNameButton;
        private ComboBox NameSortBox;
        private Button refreshButton;
        private Button displayLinesButton;
        private Button displayImagesButton;
        private Button displayButtonsButton;
    }
}

