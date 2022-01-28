
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.addgame = new System.Windows.Forms.Button();
            this.operationLabel = new System.Windows.Forms.Label();
            this.namebox = new System.Windows.Forms.TextBox();
            this.statusbox = new System.Windows.Forms.ComboBox();
            this.YearSortBox = new System.Windows.Forms.ComboBox();
            this.ByYearButton = new System.Windows.Forms.Button();
            this.ByScoreButton = new System.Windows.Forms.Button();
            this.statisticsLabel = new System.Windows.Forms.Label();
            this.ByStatusButton = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
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
            this.ByNameButton = new System.Windows.Forms.Button();
            this.NameSortBox = new System.Windows.Forms.ComboBox();
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
            this.statisticsLabel.Location = new System.Drawing.Point(736, 647);
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
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "A Plague Tale Innocence.jpg");
            this.imageList1.Images.SetKeyName(1, "Alan Wake.jpg");
            this.imageList1.Images.SetKeyName(2, "Antichamber.jpg");
            this.imageList1.Images.SetKeyName(3, "Arcanum Of Steamworks and Magick Obscura.jpg");
            this.imageList1.Images.SetKeyName(4, "Army of Two The Devil\'s Cartel.jpg");
            this.imageList1.Images.SetKeyName(5, "Arx Fatalis.jpg");
            this.imageList1.Images.SetKeyName(6, "Assassin\'s Creed Brotherhood.jpg");
            this.imageList1.Images.SetKeyName(7, "Assassin\'s Creed III Liberation.jpg");
            this.imageList1.Images.SetKeyName(8, "Assassin\'s Creed III.jpg");
            this.imageList1.Images.SetKeyName(9, "Assassin\'s Creed IV Black Flag.jpg");
            this.imageList1.Images.SetKeyName(10, "Assassin\'s Creed Origins.jpg");
            this.imageList1.Images.SetKeyName(11, "Baldur\'s Gate II Enhanced Edition.jpg");
            this.imageList1.Images.SetKeyName(12, "Batman Arkham Asylum.jpg");
            this.imageList1.Images.SetKeyName(13, "Batman Arkham City.jpg");
            this.imageList1.Images.SetKeyName(14, "Batman Arkham Knight.jpg");
            this.imageList1.Images.SetKeyName(15, "Batman Arkham Origins Blackgate.jpg");
            this.imageList1.Images.SetKeyName(16, "Batman Arkham Origins.jpg");
            this.imageList1.Images.SetKeyName(17, "BattleBlock Theater.jpg");
            this.imageList1.Images.SetKeyName(18, "Battlefield 1.jpg");
            this.imageList1.Images.SetKeyName(19, "Battlefield 3.jpg");
            this.imageList1.Images.SetKeyName(20, "Battlefield Bad Company 2.jpg");
            this.imageList1.Images.SetKeyName(21, "Beholder.jpg");
            this.imageList1.Images.SetKeyName(22, "BioShock 2.jpg");
            this.imageList1.Images.SetKeyName(23, "BioShock Infinite.jpg");
            this.imageList1.Images.SetKeyName(24, "Black Mesa.jpg");
            this.imageList1.Images.SetKeyName(25, "Bloodborne.jpg");
            this.imageList1.Images.SetKeyName(26, "BPM Bullets Per Minute.jpg");
            this.imageList1.Images.SetKeyName(27, "Braid.jpg");
            this.imageList1.Images.SetKeyName(28, "Call of Duty Black Ops 4.jpg");
            this.imageList1.Images.SetKeyName(29, "Call of Duty Black Ops Declassified.jpg");
            this.imageList1.Images.SetKeyName(30, "Call of Duty Black Ops III.jpg");
            this.imageList1.Images.SetKeyName(31, "Call of Duty Infinite Warfare.jpg");
            this.imageList1.Images.SetKeyName(32, "Call of Duty World at War.jpg");
            this.imageList1.Images.SetKeyName(33, "Catherine Full Body.jpg");
            this.imageList1.Images.SetKeyName(34, "Cave Story.jpg");
            this.imageList1.Images.SetKeyName(35, "Celeste.jpg");
            this.imageList1.Images.SetKeyName(36, "Chaos;Child.jpg");
            this.imageList1.Images.SetKeyName(37, "Cities Skylines.jpg");
            this.imageList1.Images.SetKeyName(38, "Control.jpg");
            this.imageList1.Images.SetKeyName(39, "Crysis 3.jpg");
            this.imageList1.Images.SetKeyName(40, "Crysis.jpg");
            this.imageList1.Images.SetKeyName(41, "Cube Escape Paradox.jpg");
            this.imageList1.Images.SetKeyName(42, "Cuphead.jpg");
            this.imageList1.Images.SetKeyName(43, "Cyberpunk 2077.jpg");
            this.imageList1.Images.SetKeyName(44, "Danganronpa 2 Goodbye Despair.jpg");
            this.imageList1.Images.SetKeyName(45, "Danganronpa Trigger Happy Havoc.jpg");
            this.imageList1.Images.SetKeyName(46, "Danganronpa V3 Killing Harmony.jpg");
            this.imageList1.Images.SetKeyName(47, "Dark Messiah of Might and Magic.jpg");
            this.imageList1.Images.SetKeyName(48, "Dark Souls III.jpg");
            this.imageList1.Images.SetKeyName(49, "Dark Souls.jpg");
            this.imageList1.Images.SetKeyName(50, "Darksiders.jpg");
            this.imageList1.Images.SetKeyName(51, "Dead Space 2.jpg");
            this.imageList1.Images.SetKeyName(52, "Death Stranding.jpg");
            this.imageList1.Images.SetKeyName(53, "Deponia Doomsday.jpg");
            this.imageList1.Images.SetKeyName(54, "Deponia.jpg");
            this.imageList1.Images.SetKeyName(55, "Deus Ex Human Revolution.jpg");
            this.imageList1.Images.SetKeyName(56, "Deus Ex Mankind Divided.jpg");
            this.imageList1.Images.SetKeyName(57, "Deus Ex.jpg");
            this.imageList1.Images.SetKeyName(58, "Deus.jpg");
            this.imageList1.Images.SetKeyName(59, "Devil May Cry 5.jpg");
            this.imageList1.Images.SetKeyName(60, "Diablo III.jpg");
            this.imageList1.Images.SetKeyName(61, "Disciples II Gallean\'s Return.jpg");
            this.imageList1.Images.SetKeyName(62, "Disco Elysium.jpg");
            this.imageList1.Images.SetKeyName(63, "Dishonored 2.jpg");
            this.imageList1.Images.SetKeyName(64, "Divinity Original Sin 2.jpg");
            this.imageList1.Images.SetKeyName(65, "Doom Eternal.jpg");
            this.imageList1.Images.SetKeyName(66, "Dragon Age Origins.jpg");
            this.imageList1.Images.SetKeyName(67, "Enter the Gungeon.jpg");
            this.imageList1.Images.SetKeyName(68, "Europa Universalis.jpg");
            this.imageList1.Images.SetKeyName(69, "Ever 17 The Out of Infinity.jpg");
            this.imageList1.Images.SetKeyName(70, "Everlasting Summer.jpg");
            this.imageList1.Images.SetKeyName(71, "Far Cry 3 Blood Dragon.jpg");
            this.imageList1.Images.SetKeyName(72, "Fatestay night.jpg");
            this.imageList1.Images.SetKeyName(73, "FIFA 13.jpg");
            this.imageList1.Images.SetKeyName(74, "Final Fantasy IX.jpg");
            this.imageList1.Images.SetKeyName(75, "Final Fantasy VII Remake.jpg");
            this.imageList1.Images.SetKeyName(76, "Final Fantasy XIII.jpg");
            this.imageList1.Images.SetKeyName(77, "Firewatch.jpg");
            this.imageList1.Images.SetKeyName(78, "Forza Horizon 4.jpg");
            this.imageList1.Images.SetKeyName(79, "Game Dev Tycoon.jpg");
            this.imageList1.Images.SetKeyName(80, "Gears 5.jpg");
            this.imageList1.Images.SetKeyName(81, "Gears of War 2.jpg");
            this.imageList1.Images.SetKeyName(82, "Gears of War 3.jpg");
            this.imageList1.Images.SetKeyName(83, "Gears of War 4.jpg");
            this.imageList1.Images.SetKeyName(84, "Ghostrunner.jpg");
            this.imageList1.Images.SetKeyName(85, "God of War III.jpg");
            this.imageList1.Images.SetKeyName(86, "Goodbye Deponia.jpg");
            this.imageList1.Images.SetKeyName(87, "Grand Theft Auto IV.jpg");
            this.imageList1.Images.SetKeyName(88, "Grim Fandango Remastered.jpg");
            this.imageList1.Images.SetKeyName(89, "Half-Life 2 Episode One.jpg");
            this.imageList1.Images.SetKeyName(90, "Half-Life 2 Episode Two.jpg");
            this.imageList1.Images.SetKeyName(91, "Half-Life 2.jpg");
            this.imageList1.Images.SetKeyName(92, "Half-Life Blue Shift.jpg");
            this.imageList1.Images.SetKeyName(93, "Halo 2.jpg");
            this.imageList1.Images.SetKeyName(94, "Halo 3 ODST.jpg");
            this.imageList1.Images.SetKeyName(95, "Halo 3.jpg");
            this.imageList1.Images.SetKeyName(96, "Halo 4.jpg");
            this.imageList1.Images.SetKeyName(97, "Halo 5 Guardians.jpg");
            this.imageList1.Images.SetKeyName(98, "Halo Combat Evolved.jpg");
            this.imageList1.Images.SetKeyName(99, "Heavy Rain.jpg");
            this.imageList1.Images.SetKeyName(100, "Hellblade Senua\'s Sacrifice.jpg");
            this.imageList1.Images.SetKeyName(101, "Her Story.jpg");
            this.imageList1.Images.SetKeyName(102, "Hitman Blood Money.jpg");
            this.imageList1.Images.SetKeyName(103, "Hotline Miami 2 Wrong Number.jpg");
            this.imageList1.Images.SetKeyName(104, "Hotline Miami.jpg");
            this.imageList1.Images.SetKeyName(105, "Injustice Gods Among Us.jpg");
            this.imageList1.Images.SetKeyName(106, "Iwaihime.jpg");
            this.imageList1.Images.SetKeyName(107, "Just Cause 2.jpg");
            this.imageList1.Images.SetKeyName(108, "Katamari Damacy.jpg");
            this.imageList1.Images.SetKeyName(109, "Kentucky Route Zero.jpg");
            this.imageList1.Images.SetKeyName(110, "Killzone Mercenary.jpg");
            this.imageList1.Images.SetKeyName(111, "L.A. Noire.jpg");
            this.imageList1.Images.SetKeyName(112, "Lara Croft and the Guardian of Light.jpg");
            this.imageList1.Images.SetKeyName(113, "Late Shift.jpg");
            this.imageList1.Images.SetKeyName(114, "Layers of Fear 2.jpg");
            this.imageList1.Images.SetKeyName(115, "Limbo.jpg");
            this.imageList1.Images.SetKeyName(116, "LittleBigPlanet 2.jpg");
            this.imageList1.Images.SetKeyName(117, "LittleBigPlanet 3.jpg");
            this.imageList1.Images.SetKeyName(118, "LittleBigPlanet PS Vita.jpg");
            this.imageList1.Images.SetKeyName(119, "Machinarium.jpg");
            this.imageList1.Images.SetKeyName(120, "Mafia III Definitive Edition.jpg");
            this.imageList1.Images.SetKeyName(121, "Mass Effect 2.jpg");
            this.imageList1.Images.SetKeyName(122, "Mass Effect 3.jpg");
            this.imageList1.Images.SetKeyName(123, "Max Payne.jpg");
            this.imageList1.Images.SetKeyName(124, "Metal Gear Solid 2 Sons of Liberty.jpg");
            this.imageList1.Images.SetKeyName(125, "Metal Gear Solid 4 Guns of the Patriots.jpg");
            this.imageList1.Images.SetKeyName(126, "Metal Gear Solid Peace Walker.jpg");
            this.imageList1.Images.SetKeyName(127, "Metal Gear Solid V The Phantom Pain.jpg");
            this.imageList1.Images.SetKeyName(128, "Metro Exodus.jpg");
            this.imageList1.Images.SetKeyName(129, "Metroid Prime Trilogy.jpg");
            this.imageList1.Images.SetKeyName(130, "Middle-Earth Shadow of Mordor.jpg");
            this.imageList1.Images.SetKeyName(131, "Minecraft.jpg");
            this.imageList1.Images.SetKeyName(132, "My Friend Pedro.jpg");
            this.imageList1.Images.SetKeyName(133, "N.jpg");
            this.imageList1.Images.SetKeyName(134, "Nier Automata.jpg");
            this.imageList1.Images.SetKeyName(135, "Nier.jpg");
            this.imageList1.Images.SetKeyName(136, "None.jpg");
            this.imageList1.Images.SetKeyName(137, "Observation.jpg");
            this.imageList1.Images.SetKeyName(138, "Observer.jpg");
            this.imageList1.Images.SetKeyName(139, "Ori and the Will of the Wisps.jpg");
            this.imageList1.Images.SetKeyName(140, "Outer Wilds.jpg");
            this.imageList1.Images.SetKeyName(141, "Outlast.jpg");
            this.imageList1.Images.SetKeyName(142, "Papers, Please.jpg");
            this.imageList1.Images.SetKeyName(143, "Pathologic 2.jpg");
            this.imageList1.Images.SetKeyName(144, "Persona 4 Golden.jpg");
            this.imageList1.Images.SetKeyName(145, "Persona 5.jpg");
            this.imageList1.Images.SetKeyName(146, "Phoenix Wright Ace Attorney - Spirit of Justice.jpg");
            this.imageList1.Images.SetKeyName(147, "Planescape Torment.jpg");
            this.imageList1.Images.SetKeyName(148, "Plants vs. Zombies.jpg");
            this.imageList1.Images.SetKeyName(149, "Portal 2.jpg");
            this.imageList1.Images.SetKeyName(150, "Portal.jpg");
            this.imageList1.Images.SetKeyName(151, "Quantum Break.jpg");
            this.imageList1.Images.SetKeyName(152, "Red Dead Redemption 2.jpg");
            this.imageList1.Images.SetKeyName(153, "Resident Evil 3 Remake.jpg");
            this.imageList1.Images.SetKeyName(154, "Resident Evil 4.jpg");
            this.imageList1.Images.SetKeyName(155, "Resident Evil HD Remaster.jpg");
            this.imageList1.Images.SetKeyName(156, "Rocket League.jpg");
            this.imageList1.Images.SetKeyName(157, "S.T.A.L.K.E.R. Call of Pripyat.jpg");
            this.imageList1.Images.SetKeyName(158, "Scratches.jpg");
            this.imageList1.Images.SetKeyName(159, "Sekiro Shadows Die Twice.jpg");
            this.imageList1.Images.SetKeyName(160, "Sid Meier\'s Civilization III.jpg");
            this.imageList1.Images.SetKeyName(161, "Silent Hill 2.jpg");
            this.imageList1.Images.SetKeyName(162, "Silent Hill Shattered Memories.jpg");
            this.imageList1.Images.SetKeyName(163, "SiN Episodes Emergence.jpg");
            this.imageList1.Images.SetKeyName(164, "SiN.jpg");
            this.imageList1.Images.SetKeyName(165, "Singularity.jpg");
            this.imageList1.Images.SetKeyName(166, "Spec Ops The Line.jpg");
            this.imageList1.Images.SetKeyName(167, "Spelunky.jpg");
            this.imageList1.Images.SetKeyName(168, "StarCraft II Legacy of the Void.jpg");
            this.imageList1.Images.SetKeyName(169, "StarCraft II Wings of Liberty.jpg");
            this.imageList1.Images.SetKeyName(170, "Stephen\'s Sausage Roll.jpg");
            this.imageList1.Images.SetKeyName(171, "Super Mario Odyssey.jpg");
            this.imageList1.Images.SetKeyName(172, "Super Meat Boy.jpg");
            this.imageList1.Images.SetKeyName(173, "SUPERHOT.jpg");
            this.imageList1.Images.SetKeyName(174, "Superliminal.jpg");
            this.imageList1.Images.SetKeyName(175, "System Shock 2.jpg");
            this.imageList1.Images.SetKeyName(176, "Tearaway.jpg");
            this.imageList1.Images.SetKeyName(177, "Telling Lies.jpg");
            this.imageList1.Images.SetKeyName(178, "The Bunker.jpg");
            this.imageList1.Images.SetKeyName(179, "The Curse of Monkey Island.jpg");
            this.imageList1.Images.SetKeyName(180, "The Elder Scrolls V Skyrim.jpg");
            this.imageList1.Images.SetKeyName(181, "The Last Guardian.jpg");
            this.imageList1.Images.SetKeyName(182, "The Last of Us.jpg");
            this.imageList1.Images.SetKeyName(183, "The Legend of Zelda Breath of the Wild.jpg");
            this.imageList1.Images.SetKeyName(184, "The Legend of Zelda Majora\'s Mask 3D.jpg");
            this.imageList1.Images.SetKeyName(185, "The Legend of Zelda Twilight Princess HD.jpg");
            this.imageList1.Images.SetKeyName(186, "The Outer Worlds.jpg");
            this.imageList1.Images.SetKeyName(187, "The Room Three.jpg");
            this.imageList1.Images.SetKeyName(188, "The Suicide of Rachel Foster.jpg");
            this.imageList1.Images.SetKeyName(189, "The Witcher 2 Assassins of Kings.jpg");
            this.imageList1.Images.SetKeyName(190, "The Witcher 3 Wild Hunt.jpg");
            this.imageList1.Images.SetKeyName(191, "The Witness.jpg");
            this.imageList1.Images.SetKeyName(192, "The Wolf Among Us.jpg");
            this.imageList1.Images.SetKeyName(193, "Titanfall 2.jpg");
            this.imageList1.Images.SetKeyName(194, "Trine Enchanted Edition.jpg");
            this.imageList1.Images.SetKeyName(195, "Uncharted 2 Among Thieves.jpg");
            this.imageList1.Images.SetKeyName(196, "Uncharted 3 Drake\'s Deception.jpg");
            this.imageList1.Images.SetKeyName(197, "Uncharted 4 A Thief\'s End.jpg");
            this.imageList1.Images.SetKeyName(198, "Uncharted Drake\'s Fortune.jpg");
            this.imageList1.Images.SetKeyName(199, "Uncharted Golden Abyss.jpg");
            this.imageList1.Images.SetKeyName(200, "Undertale.jpg");
            this.imageList1.Images.SetKeyName(201, "Unravel.jpg");
            this.imageList1.Images.SetKeyName(202, "Vampire The Masquerade - Bloodlines 2.jpg");
            this.imageList1.Images.SetKeyName(203, "Visage.jpg");
            this.imageList1.Images.SetKeyName(204, "VVVVVV.jpg");
            this.imageList1.Images.SetKeyName(205, "Wolfenstein II The New Colossus.jpg");
            this.imageList1.Images.SetKeyName(206, "Wolfenstein The New Order.jpg");
            this.imageList1.Images.SetKeyName(207, "World of Goo.jpg");
            this.imageList1.Images.SetKeyName(208, "World of Warcraft.jpg");
            this.imageList1.Images.SetKeyName(209, "XCOM Enemy Unknown.jpg");
            this.imageList1.Images.SetKeyName(210, "Zero Escape Zero Time Dilemma.jpg");
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
            this.ModeBox.Location = new System.Drawing.Point(12, 90);
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
            this.currentTitlePanel.Location = new System.Drawing.Point(1000, 25);
            this.currentTitlePanel.Name = "currentTitlePanel";
            this.currentTitlePanel.Size = new System.Drawing.Size(300, 600);
            this.currentTitlePanel.TabIndex = 29;
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
            this.ClientSize = new System.Drawing.Size(1384, 561);
            this.Controls.Add(this.NameSortBox);
            this.Controls.Add(this.ByNameButton);
            this.Controls.Add(this.currentTitlePanel);
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
        private System.Windows.Forms.ImageList imageList1;
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
    }
}

