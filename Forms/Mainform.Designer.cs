
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
            groupBox1 = new GroupBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // addgame
            // 
            addgame.BackColor = Color.Gray;
            addgame.FlatAppearance.BorderSize = 0;
            addgame.FlatStyle = FlatStyle.Flat;
            addgame.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addgame.ForeColor = Color.SeaShell;
            addgame.Location = new Point(96, 31);
            addgame.Name = "addgame";
            addgame.Size = new Size(133, 23);
            addgame.TabIndex = 0;
            addgame.Text = "Add titile";
            addgame.UseVisualStyleBackColor = false;
            addgame.MouseDown += addtitle_MouseDown;
            // 
            // operationLabel
            // 
            operationLabel.AutoSize = true;
            operationLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            operationLabel.ForeColor = Color.SeaShell;
            operationLabel.Location = new Point(96, 54);
            operationLabel.Name = "operationLabel";
            operationLabel.Size = new Size(57, 20);
            operationLabel.TabIndex = 1;
            operationLabel.Text = "status:";
            // 
            // namebox
            // 
            namebox.BackColor = Color.FromArgb(42, 42, 42);
            namebox.BorderStyle = BorderStyle.FixedSingle;
            namebox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            namebox.ForeColor = Color.SeaShell;
            namebox.Location = new Point(237, 31);
            namebox.Name = "namebox";
            namebox.Size = new Size(276, 23);
            namebox.TabIndex = 2;
            // 
            // statusbox
            // 
            statusbox.BackColor = Color.FromArgb(42, 42, 42);
            statusbox.DropDownStyle = ComboBoxStyle.DropDownList;
            statusbox.FlatStyle = FlatStyle.Flat;
            statusbox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            statusbox.ForeColor = Color.SeaShell;
            statusbox.FormattingEnabled = true;
            statusbox.Items.AddRange(new object[] { "COMPLETED", "BACKLOG", "RETIRED", "IN PROGRESS" });
            statusbox.Location = new Point(516, 31);
            statusbox.Name = "statusbox";
            statusbox.Size = new Size(121, 24);
            statusbox.TabIndex = 6;
            // 
            // YearSortBox
            // 
            YearSortBox.BackColor = Color.FromArgb(42, 42, 42);
            YearSortBox.DropDownStyle = ComboBoxStyle.DropDownList;
            YearSortBox.FlatStyle = FlatStyle.Flat;
            YearSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            YearSortBox.ForeColor = Color.SeaShell;
            YearSortBox.FormattingEnabled = true;
            YearSortBox.Location = new Point(96, 110);
            YearSortBox.Name = "YearSortBox";
            YearSortBox.Size = new Size(135, 24);
            YearSortBox.TabIndex = 8;
            YearSortBox.SelectionChangeCommitted += YearSortBox_SelectionChangeCommitted;
            // 
            // ByYearButton
            // 
            ByYearButton.BackColor = Color.Gray;
            ByYearButton.FlatAppearance.BorderSize = 0;
            ByYearButton.FlatStyle = FlatStyle.Flat;
            ByYearButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByYearButton.ForeColor = Color.SeaShell;
            ByYearButton.Location = new Point(96, 77);
            ByYearButton.Name = "ByYearButton";
            ByYearButton.Size = new Size(135, 27);
            ByYearButton.TabIndex = 10;
            ByYearButton.Tag = "0";
            ByYearButton.Text = "By Year";
            ByYearButton.UseVisualStyleBackColor = false;
            ByYearButton.Click += FilterButton_Click;
            // 
            // ByScoreButton
            // 
            ByScoreButton.BackColor = Color.Gray;
            ByScoreButton.FlatAppearance.BorderSize = 0;
            ByScoreButton.FlatStyle = FlatStyle.Flat;
            ByScoreButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByScoreButton.ForeColor = Color.SeaShell;
            ByScoreButton.Location = new Point(237, 77);
            ByScoreButton.Name = "ByScoreButton";
            ByScoreButton.Size = new Size(135, 27);
            ByScoreButton.TabIndex = 11;
            ByScoreButton.Tag = "1";
            ByScoreButton.Text = "By Score";
            ByScoreButton.UseVisualStyleBackColor = false;
            ByScoreButton.Click += FilterButton_Click;
            // 
            // statisticsLabel
            // 
            statisticsLabel.Anchor = AnchorStyles.Bottom;
            statisticsLabel.AutoSize = true;
            statisticsLabel.ForeColor = Color.SeaShell;
            statisticsLabel.Location = new Point(735, 700);
            statisticsLabel.Name = "statisticsLabel";
            statisticsLabel.Size = new Size(35, 13);
            statisticsLabel.TabIndex = 16;
            statisticsLabel.Text = "label2";
            // 
            // ByStatusButton
            // 
            ByStatusButton.BackColor = Color.Gray;
            ByStatusButton.FlatAppearance.BorderSize = 0;
            ByStatusButton.FlatStyle = FlatStyle.Flat;
            ByStatusButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByStatusButton.ForeColor = Color.SeaShell;
            ByStatusButton.Location = new Point(378, 76);
            ByStatusButton.Name = "ByStatusButton";
            ByStatusButton.Size = new Size(121, 27);
            ByStatusButton.TabIndex = 17;
            ByStatusButton.Tag = "2";
            ByStatusButton.Text = "By Status";
            ByStatusButton.UseVisualStyleBackColor = false;
            ByStatusButton.Click += FilterButton_Click;
            // 
            // scorebox
            // 
            scorebox.BackColor = Color.FromArgb(42, 42, 42);
            scorebox.DropDownStyle = ComboBoxStyle.DropDownList;
            scorebox.FlatStyle = FlatStyle.Flat;
            scorebox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            scorebox.ForeColor = Color.SeaShell;
            scorebox.FormattingEnabled = true;
            scorebox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            scorebox.Location = new Point(646, 30);
            scorebox.Name = "scorebox";
            scorebox.Size = new Size(68, 24);
            scorebox.TabIndex = 18;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(64, 64, 64);
            label3.ForeColor = Color.SeaShell;
            label3.Location = new Point(643, 14);
            label3.Name = "label3";
            label3.Size = new Size(35, 13);
            label3.TabIndex = 19;
            label3.Text = "Score";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(64, 64, 64);
            label5.ForeColor = Color.SeaShell;
            label5.Location = new Point(516, 14);
            label5.Name = "label5";
            label5.Size = new Size(37, 13);
            label5.TabIndex = 21;
            label5.Text = "Status";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(64, 64, 64);
            label6.ForeColor = Color.SeaShell;
            label6.Location = new Point(234, 14);
            label6.Name = "label6";
            label6.Size = new Size(35, 13);
            label6.TabIndex = 22;
            label6.Text = "Name";
            // 
            // ByGenreButton
            // 
            ByGenreButton.BackColor = Color.Gray;
            ByGenreButton.FlatAppearance.BorderSize = 0;
            ByGenreButton.FlatStyle = FlatStyle.Flat;
            ByGenreButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByGenreButton.ForeColor = Color.SeaShell;
            ByGenreButton.Location = new Point(646, 77);
            ByGenreButton.Name = "ByGenreButton";
            ByGenreButton.Size = new Size(135, 27);
            ByGenreButton.TabIndex = 25;
            ByGenreButton.Tag = "4";
            ByGenreButton.Text = "By Genre";
            ByGenreButton.UseVisualStyleBackColor = false;
            ByGenreButton.Visible = false;
            ByGenreButton.Click += FilterButton_Click;
            // 
            // ScoreSortBox
            // 
            ScoreSortBox.BackColor = Color.FromArgb(42, 42, 42);
            ScoreSortBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ScoreSortBox.FlatStyle = FlatStyle.Flat;
            ScoreSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ScoreSortBox.ForeColor = Color.SeaShell;
            ScoreSortBox.FormattingEnabled = true;
            ScoreSortBox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            ScoreSortBox.Location = new Point(237, 110);
            ScoreSortBox.Name = "ScoreSortBox";
            ScoreSortBox.Size = new Size(135, 24);
            ScoreSortBox.TabIndex = 26;
            ScoreSortBox.Visible = false;
            ScoreSortBox.SelectionChangeCommitted += YearSortBox_SelectionChangeCommitted;
            // 
            // GenreSortBox
            // 
            GenreSortBox.BackColor = Color.FromArgb(42, 42, 42);
            GenreSortBox.DropDownStyle = ComboBoxStyle.DropDownList;
            GenreSortBox.FlatStyle = FlatStyle.Flat;
            GenreSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            GenreSortBox.ForeColor = Color.SeaShell;
            GenreSortBox.FormattingEnabled = true;
            GenreSortBox.Location = new Point(646, 109);
            GenreSortBox.Name = "GenreSortBox";
            GenreSortBox.Size = new Size(135, 24);
            GenreSortBox.TabIndex = 27;
            GenreSortBox.Visible = false;
            GenreSortBox.SelectionChangeCommitted += YearSortBox_SelectionChangeCommitted;
            // 
            // StatusSortBox
            // 
            StatusSortBox.BackColor = Color.FromArgb(42, 42, 42);
            StatusSortBox.DropDownStyle = ComboBoxStyle.DropDownList;
            StatusSortBox.FlatStyle = FlatStyle.Flat;
            StatusSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            StatusSortBox.ForeColor = Color.SeaShell;
            StatusSortBox.FormattingEnabled = true;
            StatusSortBox.Items.AddRange(new object[] { "Completed", "Backlog", "Retired", "In progress" });
            StatusSortBox.Location = new Point(378, 109);
            StatusSortBox.Name = "StatusSortBox";
            StatusSortBox.Size = new Size(121, 24);
            StatusSortBox.TabIndex = 28;
            StatusSortBox.Visible = false;
            StatusSortBox.SelectionChangeCommitted += YearSortBox_SelectionChangeCommitted;
            // 
            // currentTitlePanel
            // 
            currentTitlePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            currentTitlePanel.AutoSize = true;
            currentTitlePanel.ForeColor = Color.SeaShell;
            currentTitlePanel.Location = new Point(1000, 0);
            currentTitlePanel.Name = "currentTitlePanel";
            currentTitlePanel.Size = new Size(400, 800);
            currentTitlePanel.TabIndex = 29;
            // 
            // ByNameButton
            // 
            ByNameButton.BackColor = Color.Gray;
            ByNameButton.FlatAppearance.BorderSize = 0;
            ByNameButton.FlatStyle = FlatStyle.Flat;
            ByNameButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByNameButton.ForeColor = Color.SeaShell;
            ByNameButton.Location = new Point(505, 77);
            ByNameButton.Name = "ByNameButton";
            ByNameButton.Size = new Size(135, 27);
            ByNameButton.TabIndex = 30;
            ByNameButton.Tag = "3";
            ByNameButton.Text = "By Name";
            ByNameButton.UseVisualStyleBackColor = false;
            ByNameButton.Click += FilterButton_Click;
            // 
            // NameSortBox
            // 
            NameSortBox.BackColor = Color.FromArgb(42, 42, 42);
            NameSortBox.DropDownStyle = ComboBoxStyle.DropDownList;
            NameSortBox.FlatStyle = FlatStyle.Flat;
            NameSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            NameSortBox.ForeColor = Color.SeaShell;
            NameSortBox.FormattingEnabled = true;
            NameSortBox.Items.AddRange(new object[] { "Increasing", "Descending" });
            NameSortBox.Location = new Point(505, 110);
            NameSortBox.Name = "NameSortBox";
            NameSortBox.Size = new Size(135, 24);
            NameSortBox.TabIndex = 31;
            NameSortBox.Visible = false;
            NameSortBox.SelectionChangeCommitted += YearSortBox_SelectionChangeCommitted;
            // 
            // refreshButton
            // 
            refreshButton.BackgroundImage = Properties.Resources.refresh_icon;
            refreshButton.BackgroundImageLayout = ImageLayout.Stretch;
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.ForeColor = Color.SeaShell;
            refreshButton.Location = new Point(853, 17);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(86, 57);
            refreshButton.TabIndex = 32;
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // displayLinesButton
            // 
            displayLinesButton.BackColor = Color.Gray;
            displayLinesButton.FlatAppearance.BorderSize = 0;
            displayLinesButton.FlatStyle = FlatStyle.Flat;
            displayLinesButton.ForeColor = Color.SeaShell;
            displayLinesButton.Location = new Point(181, 141);
            displayLinesButton.Name = "displayLinesButton";
            displayLinesButton.Size = new Size(75, 23);
            displayLinesButton.TabIndex = 33;
            displayLinesButton.Tag = "1";
            displayLinesButton.Text = "lines";
            displayLinesButton.UseVisualStyleBackColor = false;
            displayLinesButton.Click += displayOptionsButton_Click;
            // 
            // displayImagesButton
            // 
            displayImagesButton.BackColor = Color.Gray;
            displayImagesButton.FlatAppearance.BorderSize = 0;
            displayImagesButton.FlatStyle = FlatStyle.Flat;
            displayImagesButton.ForeColor = Color.SeaShell;
            displayImagesButton.Location = new Point(262, 141);
            displayImagesButton.Name = "displayImagesButton";
            displayImagesButton.Size = new Size(75, 23);
            displayImagesButton.TabIndex = 34;
            displayImagesButton.Tag = "2";
            displayImagesButton.Text = "greed";
            displayImagesButton.UseVisualStyleBackColor = false;
            displayImagesButton.Click += displayOptionsButton_Click;
            // 
            // displayButtonsButton
            // 
            displayButtonsButton.BackColor = Color.Gray;
            displayButtonsButton.FlatAppearance.BorderSize = 0;
            displayButtonsButton.FlatStyle = FlatStyle.Flat;
            displayButtonsButton.ForeColor = Color.SeaShell;
            displayButtonsButton.Location = new Point(100, 141);
            displayButtonsButton.Name = "displayButtonsButton";
            displayButtonsButton.Size = new Size(75, 23);
            displayButtonsButton.TabIndex = 35;
            displayButtonsButton.Tag = "0";
            displayButtonsButton.Text = "button1";
            displayButtonsButton.UseVisualStyleBackColor = false;
            displayButtonsButton.Click += displayOptionsButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Dock = DockStyle.Left;
            groupBox1.ForeColor = Color.SeaShell;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(78, 761);
            groupBox1.TabIndex = 36;
            groupBox1.TabStop = false;
            groupBox1.Text = "Mode";
            // 
            // button3
            // 
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.SeaShell;
            button3.Location = new Point(6, 159);
            button3.Name = "button3";
            button3.Size = new Size(64, 64);
            button3.TabIndex = 2;
            button3.Tag = mode.TVSERIES;
            button3.TextImageRelation = TextImageRelation.ImageAboveText;
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.BackgroundImage = Properties.Resources.film_icon;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.SeaShell;
            button2.Location = new Point(6, 89);
            button2.Name = "button2";
            button2.Size = new Size(64, 64);
            button2.TabIndex = 1;
            button2.Tag = mode.FILMS;
            button2.TextImageRelation = TextImageRelation.ImageAboveText;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button1_Click;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.game_icon;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.SeaShell;
            button1.Location = new Point(6, 19);
            button1.Name = "button1";
            button1.Size = new Size(64, 64);
            button1.TabIndex = 0;
            button1.Tag = mode.GAMES;
            button1.TextImageRelation = TextImageRelation.ImageAboveText;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Mainform
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1400, 761);
            Controls.Add(groupBox1);
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
            ForeColor = Color.SeaShell;
            Name = "Mainform";
            Text = "MyList";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
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
        private GroupBox groupBox1;
        private Button button1;
        private Button button3;
        private Button button2;
    }
}

