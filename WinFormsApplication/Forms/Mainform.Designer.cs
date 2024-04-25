
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
            components = new System.ComponentModel.Container();
            addgame = new Button();
            operationLabel = new Label();
            namebox = new TextBox();
            YearSortBox = new ComboBox();
            ByYearButton = new Button();
            ByScoreButton = new Button();
            statisticsLabel = new Label();
            ByStatusButton = new Button();
            ScoreSortBox = new ComboBox();
            StatusSortBox = new ComboBox();
            currentTitlePanel = new Panel();
            ByNameButton = new Button();
            NameSortBox = new ComboBox();
            groupBox1 = new GroupBox();
            tvseriesModeButton = new Button();
            filmsModeButton = new Button();
            gamesModeButton = new Button();
            displayModeGroupBox = new GroupBox();
            displayImagesButton = new Button();
            displayRowsButton = new Button();
            displayButtonsButton = new Button();
            refreshButton = new Forms.RoundedButton(components);
            groupBox1.SuspendLayout();
            displayModeGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // addgame
            // 
            addgame.BackColor = Color.Gray;
            addgame.BackgroundImage = Properties.Resources.search_icon;
            addgame.BackgroundImageLayout = ImageLayout.Zoom;
            addgame.FlatAppearance.BorderSize = 0;
            addgame.FlatStyle = FlatStyle.Flat;
            addgame.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            addgame.ForeColor = Color.SeaShell;
            addgame.Location = new Point(100, 22);
            addgame.Name = "addgame";
            addgame.Size = new Size(38, 29);
            addgame.TabIndex = 0;
            addgame.UseVisualStyleBackColor = false;
            addgame.MouseDown += addtitle_MouseDown;
            // 
            // operationLabel
            // 
            operationLabel.AutoSize = true;
            operationLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            operationLabel.ForeColor = Color.SeaShell;
            operationLabel.Location = new Point(505, 27);
            operationLabel.Name = "operationLabel";
            operationLabel.Size = new Size(57, 20);
            operationLabel.TabIndex = 1;
            operationLabel.Text = "status:";
            // 
            // namebox
            // 
            namebox.BackColor = Color.FromArgb(42, 42, 42);
            namebox.BorderStyle = BorderStyle.FixedSingle;
            namebox.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            namebox.ForeColor = Color.SeaShell;
            namebox.Location = new Point(144, 22);
            namebox.Name = "namebox";
            namebox.PlaceholderText = "Enter title";
            namebox.Size = new Size(355, 29);
            namebox.TabIndex = 2;
            // 
            // YearSortBox
            // 
            YearSortBox.BackColor = Color.FromArgb(42, 42, 42);
            YearSortBox.DropDownStyle = ComboBoxStyle.DropDownList;
            YearSortBox.FlatStyle = FlatStyle.Flat;
            YearSortBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            YearSortBox.ForeColor = Color.SeaShell;
            YearSortBox.FormattingEnabled = true;
            YearSortBox.Location = new Point(100, 90);
            YearSortBox.Name = "YearSortBox";
            YearSortBox.Size = new Size(135, 24);
            YearSortBox.TabIndex = 8;
            YearSortBox.SelectionChangeCommitted += ByYearButton_Click;
            // 
            // ByYearButton
            // 
            ByYearButton.BackColor = Color.Gray;
            ByYearButton.FlatAppearance.BorderSize = 0;
            ByYearButton.FlatStyle = FlatStyle.Flat;
            ByYearButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByYearButton.ForeColor = Color.SeaShell;
            ByYearButton.Location = new Point(100, 57);
            ByYearButton.Name = "ByYearButton";
            ByYearButton.Size = new Size(135, 27);
            ByYearButton.TabIndex = 10;
            ByYearButton.Tag = "0";
            ByYearButton.Text = "By Year";
            ByYearButton.UseVisualStyleBackColor = false;
            ByYearButton.Click += ByYearButton_Click;
            // 
            // ByScoreButton
            // 
            ByScoreButton.BackColor = Color.Gray;
            ByScoreButton.FlatAppearance.BorderSize = 0;
            ByScoreButton.FlatStyle = FlatStyle.Flat;
            ByScoreButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByScoreButton.ForeColor = Color.SeaShell;
            ByScoreButton.Location = new Point(241, 57);
            ByScoreButton.Name = "ByScoreButton";
            ByScoreButton.Size = new Size(135, 27);
            ByScoreButton.TabIndex = 11;
            ByScoreButton.Tag = "1";
            ByScoreButton.Text = "By Score";
            ByScoreButton.UseVisualStyleBackColor = false;
            ByScoreButton.Click += ByScoreButton_Click;
            // 
            // statisticsLabel
            // 
            statisticsLabel.Anchor = AnchorStyles.Bottom;
            statisticsLabel.AutoSize = true;
            statisticsLabel.ForeColor = Color.SeaShell;
            statisticsLabel.Location = new Point(735, 744);
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
            ByStatusButton.Location = new Point(382, 56);
            ByStatusButton.Name = "ByStatusButton";
            ByStatusButton.Size = new Size(121, 27);
            ByStatusButton.TabIndex = 17;
            ByStatusButton.Tag = "2";
            ByStatusButton.Text = "By Status";
            ByStatusButton.UseVisualStyleBackColor = false;
            ByStatusButton.Click += ByStatusButton_Click;
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
            ScoreSortBox.Location = new Point(241, 90);
            ScoreSortBox.Name = "ScoreSortBox";
            ScoreSortBox.Size = new Size(135, 24);
            ScoreSortBox.TabIndex = 26;
            ScoreSortBox.SelectionChangeCommitted += ByScoreButton_Click;
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
            StatusSortBox.Location = new Point(382, 89);
            StatusSortBox.Name = "StatusSortBox";
            StatusSortBox.Size = new Size(121, 24);
            StatusSortBox.TabIndex = 28;
            StatusSortBox.SelectionChangeCommitted += ByStatusButton_Click;
            // 
            // currentTitlePanel
            // 
            currentTitlePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            currentTitlePanel.ForeColor = Color.SeaShell;
            currentTitlePanel.Location = new Point(938, 5);
            currentTitlePanel.Name = "currentTitlePanel";
            currentTitlePanel.Size = new Size(460, 800);
            currentTitlePanel.TabIndex = 29;
            // 
            // ByNameButton
            // 
            ByNameButton.BackColor = Color.Gray;
            ByNameButton.FlatAppearance.BorderSize = 0;
            ByNameButton.FlatStyle = FlatStyle.Flat;
            ByNameButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ByNameButton.ForeColor = Color.SeaShell;
            ByNameButton.Location = new Point(509, 57);
            ByNameButton.Name = "ByNameButton";
            ByNameButton.Size = new Size(135, 27);
            ByNameButton.TabIndex = 30;
            ByNameButton.Tag = "3";
            ByNameButton.Text = "By Name";
            ByNameButton.UseVisualStyleBackColor = false;
            ByNameButton.Click += ByNameButton_Click;
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
            NameSortBox.Location = new Point(509, 90);
            NameSortBox.Name = "NameSortBox";
            NameSortBox.Size = new Size(135, 24);
            NameSortBox.TabIndex = 31;
            NameSortBox.SelectionChangeCommitted += ByNameButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tvseriesModeButton);
            groupBox1.Controls.Add(filmsModeButton);
            groupBox1.Controls.Add(gamesModeButton);
            groupBox1.Dock = DockStyle.Left;
            groupBox1.ForeColor = Color.SeaShell;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(78, 805);
            groupBox1.TabIndex = 36;
            groupBox1.TabStop = false;
            groupBox1.Text = "Mode";
            // 
            // tvseriesModeButton
            // 
            tvseriesModeButton.BackgroundImage = Properties.Resources.tvseries_icon;
            tvseriesModeButton.FlatAppearance.BorderSize = 2;
            tvseriesModeButton.FlatStyle = FlatStyle.Popup;
            tvseriesModeButton.ForeColor = Color.SeaShell;
            tvseriesModeButton.Location = new Point(6, 159);
            tvseriesModeButton.Name = "tvseriesModeButton";
            tvseriesModeButton.Size = new Size(64, 64);
            tvseriesModeButton.TabIndex = 2;
            tvseriesModeButton.Tag = "TVSeries";
            tvseriesModeButton.TextImageRelation = TextImageRelation.ImageAboveText;
            tvseriesModeButton.UseVisualStyleBackColor = true;
            // 
            // filmsModeButton
            // 
            filmsModeButton.BackgroundImage = Properties.Resources.film_icon;
            filmsModeButton.FlatAppearance.BorderSize = 2;
            filmsModeButton.FlatStyle = FlatStyle.Popup;
            filmsModeButton.ForeColor = Color.SeaShell;
            filmsModeButton.Location = new Point(6, 89);
            filmsModeButton.Name = "filmsModeButton";
            filmsModeButton.Size = new Size(64, 64);
            filmsModeButton.TabIndex = 1;
            filmsModeButton.Tag = "Film";
            filmsModeButton.TextImageRelation = TextImageRelation.ImageAboveText;
            filmsModeButton.UseVisualStyleBackColor = true;
            filmsModeButton.Click += button1_Click;
            // 
            // gamesModeButton
            // 
            gamesModeButton.BackgroundImage = Properties.Resources.game_icon;
            gamesModeButton.FlatAppearance.BorderSize = 2;
            gamesModeButton.FlatStyle = FlatStyle.Popup;
            gamesModeButton.ForeColor = Color.SeaShell;
            gamesModeButton.Location = new Point(6, 19);
            gamesModeButton.Name = "gamesModeButton";
            gamesModeButton.Size = new Size(64, 64);
            gamesModeButton.TabIndex = 0;
            gamesModeButton.Tag = "Game";
            gamesModeButton.TextImageRelation = TextImageRelation.ImageAboveText;
            gamesModeButton.UseVisualStyleBackColor = true;
            gamesModeButton.Click += button1_Click;
            // 
            // displayModeGroupBox
            // 
            displayModeGroupBox.Controls.Add(displayImagesButton);
            displayModeGroupBox.Controls.Add(displayRowsButton);
            displayModeGroupBox.Controls.Add(displayButtonsButton);
            displayModeGroupBox.ForeColor = Color.SeaShell;
            displayModeGroupBox.Location = new Point(104, 120);
            displayModeGroupBox.Name = "displayModeGroupBox";
            displayModeGroupBox.Size = new Size(399, 52);
            displayModeGroupBox.TabIndex = 37;
            displayModeGroupBox.TabStop = false;
            displayModeGroupBox.Text = "Display mode";
            // 
            // displayImagesButton
            // 
            displayImagesButton.BackgroundImage = Properties.Resources.images_icon;
            displayImagesButton.BackgroundImageLayout = ImageLayout.Zoom;
            displayImagesButton.FlatAppearance.BorderSize = 2;
            displayImagesButton.FlatStyle = FlatStyle.Popup;
            displayImagesButton.ForeColor = Color.SeaShell;
            displayImagesButton.Location = new Point(86, 13);
            displayImagesButton.Name = "displayImagesButton";
            displayImagesButton.Size = new Size(32, 32);
            displayImagesButton.TabIndex = 2;
            displayImagesButton.TextImageRelation = TextImageRelation.ImageAboveText;
            displayImagesButton.UseVisualStyleBackColor = true;
            displayImagesButton.Click += displayImagesButton_Click;
            // 
            // displayRowsButton
            // 
            displayRowsButton.BackgroundImage = Properties.Resources.rows_icon;
            displayRowsButton.BackgroundImageLayout = ImageLayout.Zoom;
            displayRowsButton.FlatAppearance.BorderSize = 0;
            displayRowsButton.FlatStyle = FlatStyle.Popup;
            displayRowsButton.ForeColor = Color.SeaShell;
            displayRowsButton.Location = new Point(44, 13);
            displayRowsButton.Name = "displayRowsButton";
            displayRowsButton.Size = new Size(32, 32);
            displayRowsButton.TabIndex = 1;
            displayRowsButton.TextImageRelation = TextImageRelation.ImageAboveText;
            displayRowsButton.UseVisualStyleBackColor = true;
            displayRowsButton.Click += displayRowsButton_Click;
            // 
            // displayButtonsButton
            // 
            displayButtonsButton.BackgroundImage = Properties.Resources.buttons_icon;
            displayButtonsButton.BackgroundImageLayout = ImageLayout.Zoom;
            displayButtonsButton.FlatAppearance.BorderSize = 2;
            displayButtonsButton.FlatStyle = FlatStyle.Popup;
            displayButtonsButton.ForeColor = Color.SeaShell;
            displayButtonsButton.Location = new Point(6, 13);
            displayButtonsButton.Name = "displayButtonsButton";
            displayButtonsButton.Size = new Size(32, 32);
            displayButtonsButton.TabIndex = 0;
            displayButtonsButton.TextImageRelation = TextImageRelation.ImageAboveText;
            displayButtonsButton.UseVisualStyleBackColor = true;
            displayButtonsButton.Click += displayOptionsButton_Click;
            // 
            // refreshButton
            // 
            refreshButton.BackgroundImage = Properties.Resources.refresh_icon;
            refreshButton.BackgroundImageLayout = ImageLayout.Stretch;
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.FlatStyle = FlatStyle.Popup;
            refreshButton.Font = new Font("Arial", 8F, FontStyle.Bold, GraphicsUnit.Point);
            refreshButton.ForeColor = Color.Black;
            refreshButton.Location = new Point(878, 5);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(54, 33);
            refreshButton.TabIndex = 38;
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // Mainform
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1400, 805);
            Controls.Add(refreshButton);
            Controls.Add(displayModeGroupBox);
            Controls.Add(groupBox1);
            Controls.Add(currentTitlePanel);
            Controls.Add(NameSortBox);
            Controls.Add(ByNameButton);
            Controls.Add(StatusSortBox);
            Controls.Add(ScoreSortBox);
            Controls.Add(ByStatusButton);
            Controls.Add(statisticsLabel);
            Controls.Add(ByScoreButton);
            Controls.Add(ByYearButton);
            Controls.Add(YearSortBox);
            Controls.Add(namebox);
            Controls.Add(operationLabel);
            Controls.Add(addgame);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.SeaShell;
            Name = "Mainform";
            Text = "MyList";
            FormClosing += Mainform_FormClosing;
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            displayModeGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addgame;
        private Label operationLabel;
        private TextBox namebox;
        private ComboBox YearSortBox;
        private Button ByYearButton;
        private Button ByScoreButton;
        private Label statisticsLabel;
        private Button ByStatusButton;
        private ComboBox ScoreSortBox;
        private ComboBox StatusSortBox;
        private Panel currentTitlePanel;
        private Button ByNameButton;
        private ComboBox NameSortBox;
        private Button displayRowsButton;
        private Button displayImagesButton;
        private Button displayButtonsButton;
        private GroupBox groupBox1;
        private Button gamesModeButton;
        private Button tvseriesModeButton;
        private Button filmsModeButton;
        private GroupBox displayModeGroupBox;
        private Forms.RoundedButton refreshButton;
    }
}

