namespace DiO_CS_NineMansMorris
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.btnProcessImage = new System.Windows.Forms.Button();
            this.pbProcessed = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslblStatusProces = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblPlayerDiffculty = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblSerialStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDesition = new System.Windows.Forms.Label();
            this.btnSeeTheRobotView = new System.Windows.Forms.Button();
            this.gbRobotController = new System.Windows.Forms.GroupBox();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.btnMoveRobotToHome = new System.Windows.Forms.Button();
            this.btnSearchForPorts = new System.Windows.Forms.Button();
            this.lblSerialPortName = new System.Windows.Forms.Label();
            this.cmbPortNames = new System.Windows.Forms.ComboBox();
            this.chbEnableCmdSending = new System.Windows.Forms.CheckBox();
            this.btnMakeMove = new System.Windows.Forms.Button();
            this.gbArtifitialInteligence = new System.Windows.Forms.GroupBox();
            this.lblComputerPlayerColor = new System.Windows.Forms.Label();
            this.lblHumanPlayerColor = new System.Windows.Forms.Label();
            this.cbHumanPlayerColor = new System.Windows.Forms.ComboBox();
            this.cbComputerPlayerColor = new System.Windows.Forms.ComboBox();
            this.txtPlayerDiffculty = new System.Windows.Forms.TextBox();
            this.lblDiffculty = new System.Windows.Forms.Label();
            this.btnCalculateNextMove = new System.Windows.Forms.Button();
            this.txtBase = new System.Windows.Forms.TextBox();
            this.txtShoulder = new System.Windows.Forms.TextBox();
            this.txtElbow = new System.Windows.Forms.TextBox();
            this.txtWrist = new System.Windows.Forms.TextBox();
            this.txtGripper = new System.Windows.Forms.TextBox();
            this.btnSetPosition = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessed)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.gbRobotController.SuspendLayout();
            this.gbArtifitialInteligence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMain.Location = new System.Drawing.Point(12, 12);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(600, 433);
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            // 
            // btnProcessImage
            // 
            this.btnProcessImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnProcessImage.Location = new System.Drawing.Point(519, 468);
            this.btnProcessImage.Name = "btnProcessImage";
            this.btnProcessImage.Size = new System.Drawing.Size(93, 52);
            this.btnProcessImage.TabIndex = 1;
            this.btnProcessImage.Text = "Process Image";
            this.btnProcessImage.UseVisualStyleBackColor = true;
            this.btnProcessImage.Click += new System.EventHandler(this.btnProcessImage_Click);
            // 
            // pbProcessed
            // 
            this.pbProcessed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbProcessed.Location = new System.Drawing.Point(618, 12);
            this.pbProcessed.Name = "pbProcessed";
            this.pbProcessed.Size = new System.Drawing.Size(400, 400);
            this.pbProcessed.TabIndex = 2;
            this.pbProcessed.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblStatusProces,
            this.tsslblPlayerDiffculty,
            this.tsslblSerialStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 582);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1306, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslblStatusProces
            // 
            this.tsslblStatusProces.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsslblStatusProces.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tsslblStatusProces.Name = "tsslblStatusProces";
            this.tsslblStatusProces.Size = new System.Drawing.Size(28, 17);
            this.tsslblStatusProces.Text = "----";
            // 
            // tsslblPlayerDiffculty
            // 
            this.tsslblPlayerDiffculty.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsslblPlayerDiffculty.ForeColor = System.Drawing.Color.Red;
            this.tsslblPlayerDiffculty.Name = "tsslblPlayerDiffculty";
            this.tsslblPlayerDiffculty.Size = new System.Drawing.Size(28, 17);
            this.tsslblPlayerDiffculty.Text = "----";
            // 
            // tsslblSerialStatus
            // 
            this.tsslblSerialStatus.ForeColor = System.Drawing.Color.Red;
            this.tsslblSerialStatus.Name = "tsslblSerialStatus";
            this.tsslblSerialStatus.Size = new System.Drawing.Size(27, 17);
            this.tsslblSerialStatus.Text = "----";
            // 
            // lblDesition
            // 
            this.lblDesition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDesition.Location = new System.Drawing.Point(12, 526);
            this.lblDesition.Name = "lblDesition";
            this.lblDesition.Size = new System.Drawing.Size(600, 46);
            this.lblDesition.TabIndex = 8;
            this.lblDesition.Text = "Desition: ";
            // 
            // btnSeeTheRobotView
            // 
            this.btnSeeTheRobotView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSeeTheRobotView.Location = new System.Drawing.Point(420, 468);
            this.btnSeeTheRobotView.Name = "btnSeeTheRobotView";
            this.btnSeeTheRobotView.Size = new System.Drawing.Size(93, 52);
            this.btnSeeTheRobotView.TabIndex = 13;
            this.btnSeeTheRobotView.Text = "See the robot view";
            this.btnSeeTheRobotView.UseVisualStyleBackColor = true;
            this.btnSeeTheRobotView.Click += new System.EventHandler(this.btnSeeTheRobotView_Click);
            // 
            // gbRobotController
            // 
            this.gbRobotController.Controls.Add(this.btnSendCommand);
            this.gbRobotController.Controls.Add(this.btnMoveRobotToHome);
            this.gbRobotController.Controls.Add(this.btnSearchForPorts);
            this.gbRobotController.Controls.Add(this.lblSerialPortName);
            this.gbRobotController.Controls.Add(this.cmbPortNames);
            this.gbRobotController.Controls.Add(this.chbEnableCmdSending);
            this.gbRobotController.Controls.Add(this.btnMakeMove);
            this.gbRobotController.Location = new System.Drawing.Point(768, 418);
            this.gbRobotController.Name = "gbRobotController";
            this.gbRobotController.Size = new System.Drawing.Size(270, 160);
            this.gbRobotController.TabIndex = 18;
            this.gbRobotController.TabStop = false;
            this.gbRobotController.Text = "Robot Controller";
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSendCommand.Location = new System.Drawing.Point(165, 76);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(93, 52);
            this.btnSendCommand.TabIndex = 24;
            this.btnSendCommand.Text = "Test Robot Arm";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
            // 
            // btnMoveRobotToHome
            // 
            this.btnMoveRobotToHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMoveRobotToHome.Location = new System.Drawing.Point(165, 19);
            this.btnMoveRobotToHome.Name = "btnMoveRobotToHome";
            this.btnMoveRobotToHome.Size = new System.Drawing.Size(93, 52);
            this.btnMoveRobotToHome.TabIndex = 23;
            this.btnMoveRobotToHome.Text = "GOTO HOME";
            this.btnMoveRobotToHome.UseVisualStyleBackColor = true;
            this.btnMoveRobotToHome.Click += new System.EventHandler(this.btnMoveRobotToHome_Click);
            // 
            // btnSearchForPorts
            // 
            this.btnSearchForPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSearchForPorts.Location = new System.Drawing.Point(6, 128);
            this.btnSearchForPorts.Name = "btnSearchForPorts";
            this.btnSearchForPorts.Size = new System.Drawing.Size(155, 26);
            this.btnSearchForPorts.TabIndex = 22;
            this.btnSearchForPorts.Text = "Search";
            this.btnSearchForPorts.UseVisualStyleBackColor = true;
            this.btnSearchForPorts.Click += new System.EventHandler(this.btnSearchForPorts_Click);
            // 
            // lblSerialPortName
            // 
            this.lblSerialPortName.AutoSize = true;
            this.lblSerialPortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSerialPortName.Location = new System.Drawing.Point(6, 104);
            this.lblSerialPortName.Name = "lblSerialPortName";
            this.lblSerialPortName.Size = new System.Drawing.Size(84, 16);
            this.lblSerialPortName.TabIndex = 21;
            this.lblSerialPortName.Text = "Serial port:";
            // 
            // cmbPortNames
            // 
            this.cmbPortNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbPortNames.FormattingEnabled = true;
            this.cmbPortNames.Location = new System.Drawing.Point(89, 103);
            this.cmbPortNames.Name = "cmbPortNames";
            this.cmbPortNames.Size = new System.Drawing.Size(72, 24);
            this.cmbPortNames.TabIndex = 20;
            this.cmbPortNames.SelectedValueChanged += new System.EventHandler(this.cmbPortNames_SelectedValueChanged);
            // 
            // chbEnableCmdSending
            // 
            this.chbEnableCmdSending.AutoSize = true;
            this.chbEnableCmdSending.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbEnableCmdSending.Location = new System.Drawing.Point(6, 77);
            this.chbEnableCmdSending.Name = "chbEnableCmdSending";
            this.chbEnableCmdSending.Size = new System.Drawing.Size(135, 20);
            this.chbEnableCmdSending.TabIndex = 19;
            this.chbEnableCmdSending.Text = "Enable sending";
            this.chbEnableCmdSending.UseVisualStyleBackColor = true;
            this.chbEnableCmdSending.CheckedChanged += new System.EventHandler(this.chbEnableCmdSending_CheckedChanged);
            // 
            // btnMakeMove
            // 
            this.btnMakeMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMakeMove.Location = new System.Drawing.Point(6, 18);
            this.btnMakeMove.Name = "btnMakeMove";
            this.btnMakeMove.Size = new System.Drawing.Size(155, 52);
            this.btnMakeMove.TabIndex = 18;
            this.btnMakeMove.Text = "Make move";
            this.btnMakeMove.UseVisualStyleBackColor = true;
            this.btnMakeMove.Click += new System.EventHandler(this.btnMakeMove_Click);
            // 
            // gbArtifitialInteligence
            // 
            this.gbArtifitialInteligence.Controls.Add(this.lblComputerPlayerColor);
            this.gbArtifitialInteligence.Controls.Add(this.lblHumanPlayerColor);
            this.gbArtifitialInteligence.Controls.Add(this.cbHumanPlayerColor);
            this.gbArtifitialInteligence.Controls.Add(this.cbComputerPlayerColor);
            this.gbArtifitialInteligence.Controls.Add(this.txtPlayerDiffculty);
            this.gbArtifitialInteligence.Controls.Add(this.lblDiffculty);
            this.gbArtifitialInteligence.Controls.Add(this.btnCalculateNextMove);
            this.gbArtifitialInteligence.Location = new System.Drawing.Point(618, 418);
            this.gbArtifitialInteligence.Name = "gbArtifitialInteligence";
            this.gbArtifitialInteligence.Size = new System.Drawing.Size(144, 160);
            this.gbArtifitialInteligence.TabIndex = 20;
            this.gbArtifitialInteligence.TabStop = false;
            this.gbArtifitialInteligence.Text = "AI";
            // 
            // lblComputerPlayerColor
            // 
            this.lblComputerPlayerColor.AutoSize = true;
            this.lblComputerPlayerColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblComputerPlayerColor.Location = new System.Drawing.Point(6, 131);
            this.lblComputerPlayerColor.Name = "lblComputerPlayerColor";
            this.lblComputerPlayerColor.Size = new System.Drawing.Size(78, 16);
            this.lblComputerPlayerColor.TabIndex = 19;
            this.lblComputerPlayerColor.Text = "Computer:";
            // 
            // lblHumanPlayerColor
            // 
            this.lblHumanPlayerColor.AutoSize = true;
            this.lblHumanPlayerColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHumanPlayerColor.Location = new System.Drawing.Point(6, 104);
            this.lblHumanPlayerColor.Name = "lblHumanPlayerColor";
            this.lblHumanPlayerColor.Size = new System.Drawing.Size(60, 16);
            this.lblHumanPlayerColor.TabIndex = 18;
            this.lblHumanPlayerColor.Text = "Human:";
            // 
            // cbHumanPlayerColor
            // 
            this.cbHumanPlayerColor.FormattingEnabled = true;
            this.cbHumanPlayerColor.Location = new System.Drawing.Point(90, 103);
            this.cbHumanPlayerColor.Name = "cbHumanPlayerColor";
            this.cbHumanPlayerColor.Size = new System.Drawing.Size(43, 21);
            this.cbHumanPlayerColor.TabIndex = 17;
            this.cbHumanPlayerColor.SelectedIndexChanged += new System.EventHandler(this.cbHumanPlayerColor_SelectedIndexChanged);
            // 
            // cbComputerPlayerColor
            // 
            this.cbComputerPlayerColor.FormattingEnabled = true;
            this.cbComputerPlayerColor.Location = new System.Drawing.Point(90, 131);
            this.cbComputerPlayerColor.Name = "cbComputerPlayerColor";
            this.cbComputerPlayerColor.Size = new System.Drawing.Size(43, 21);
            this.cbComputerPlayerColor.TabIndex = 16;
            // 
            // txtPlayerDiffculty
            // 
            this.txtPlayerDiffculty.Location = new System.Drawing.Point(90, 77);
            this.txtPlayerDiffculty.Name = "txtPlayerDiffculty";
            this.txtPlayerDiffculty.Size = new System.Drawing.Size(43, 20);
            this.txtPlayerDiffculty.TabIndex = 14;
            this.txtPlayerDiffculty.Text = "3";
            // 
            // lblDiffculty
            // 
            this.lblDiffculty.AutoSize = true;
            this.lblDiffculty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDiffculty.Location = new System.Drawing.Point(6, 78);
            this.lblDiffculty.Name = "lblDiffculty";
            this.lblDiffculty.Size = new System.Drawing.Size(53, 15);
            this.lblDiffculty.TabIndex = 15;
            this.lblDiffculty.Text = "Depth: ";
            // 
            // btnCalculateNextMove
            // 
            this.btnCalculateNextMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCalculateNextMove.Location = new System.Drawing.Point(9, 18);
            this.btnCalculateNextMove.Name = "btnCalculateNextMove";
            this.btnCalculateNextMove.Size = new System.Drawing.Size(124, 52);
            this.btnCalculateNextMove.TabIndex = 13;
            this.btnCalculateNextMove.Text = "Calculate next move";
            this.btnCalculateNextMove.UseVisualStyleBackColor = true;
            this.btnCalculateNextMove.Click += new System.EventHandler(this.btnCalculateNextMove_Click);
            // 
            // txtBase
            // 
            this.txtBase.Location = new System.Drawing.Point(1160, 318);
            this.txtBase.Name = "txtBase";
            this.txtBase.Size = new System.Drawing.Size(76, 20);
            this.txtBase.TabIndex = 21;
            this.txtBase.Text = "0";
            // 
            // txtShoulder
            // 
            this.txtShoulder.Location = new System.Drawing.Point(1160, 344);
            this.txtShoulder.Name = "txtShoulder";
            this.txtShoulder.Size = new System.Drawing.Size(76, 20);
            this.txtShoulder.TabIndex = 22;
            this.txtShoulder.Text = "0";
            // 
            // txtElbow
            // 
            this.txtElbow.Location = new System.Drawing.Point(1160, 370);
            this.txtElbow.Name = "txtElbow";
            this.txtElbow.Size = new System.Drawing.Size(76, 20);
            this.txtElbow.TabIndex = 23;
            this.txtElbow.Text = "0";
            // 
            // txtWrist
            // 
            this.txtWrist.Location = new System.Drawing.Point(1160, 396);
            this.txtWrist.Name = "txtWrist";
            this.txtWrist.Size = new System.Drawing.Size(76, 20);
            this.txtWrist.TabIndex = 24;
            this.txtWrist.Text = "0";
            // 
            // txtGripper
            // 
            this.txtGripper.Location = new System.Drawing.Point(1160, 422);
            this.txtGripper.Name = "txtGripper";
            this.txtGripper.Size = new System.Drawing.Size(76, 20);
            this.txtGripper.TabIndex = 25;
            this.txtGripper.Text = "0";
            // 
            // btnSetPosition
            // 
            this.btnSetPosition.Location = new System.Drawing.Point(1160, 450);
            this.btnSetPosition.Name = "btnSetPosition";
            this.btnSetPosition.Size = new System.Drawing.Size(76, 27);
            this.btnSetPosition.TabIndex = 26;
            this.btnSetPosition.Text = "Manual";
            this.btnSetPosition.UseVisualStyleBackColor = true;
            this.btnSetPosition.Click += new System.EventHandler(this.btnSetPosition_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(1046, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "Base";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(1046, 344);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 28;
            this.label2.Text = "Shoulder";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(1046, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "Elbow";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(1046, 396);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 30;
            this.label4.Text = "Wrist";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(1046, 422);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "Gripper";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(1096, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 16);
            this.label6.TabIndex = 32;
            this.label6.Text = "Jog Control";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(1039, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "Base";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(1146, 50);
            this.trackBar1.Maximum = 90;
            this.trackBar1.Minimum = -90;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 34;
            this.trackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbUpdatePosition_MouseUp);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(1146, 102);
            this.trackBar2.Maximum = 90;
            this.trackBar2.Minimum = -90;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 35;
            this.trackBar2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbUpdatePosition_MouseUp);
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(1146, 153);
            this.trackBar3.Maximum = 90;
            this.trackBar3.Minimum = -90;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(104, 45);
            this.trackBar3.TabIndex = 36;
            this.trackBar3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbUpdatePosition_MouseUp);
            // 
            // trackBar4
            // 
            this.trackBar4.Location = new System.Drawing.Point(1146, 204);
            this.trackBar4.Maximum = 90;
            this.trackBar4.Minimum = -90;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(104, 45);
            this.trackBar4.TabIndex = 37;
            this.trackBar4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbUpdatePosition_MouseUp);
            // 
            // trackBar5
            // 
            this.trackBar5.Location = new System.Drawing.Point(1146, 255);
            this.trackBar5.Maximum = 90;
            this.trackBar5.Minimum = -90;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(104, 45);
            this.trackBar5.TabIndex = 38;
            this.trackBar5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbUpdatePosition_MouseUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(1039, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 15);
            this.label8.TabIndex = 39;
            this.label8.Text = "Shoulder";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(1039, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 15);
            this.label9.TabIndex = 40;
            this.label9.Text = "Elbow";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(1039, 204);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 15);
            this.label10.TabIndex = 41;
            this.label10.Text = "Wrist";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(1039, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 15);
            this.label11.TabIndex = 42;
            this.label11.Text = "Gripper";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1118, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "-90";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1118, 104);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "-90";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1118, 153);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 13);
            this.label14.TabIndex = 45;
            this.label14.Text = "-90";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1118, 204);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 13);
            this.label15.TabIndex = 46;
            this.label15.Text = "-90";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1118, 257);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 13);
            this.label16.TabIndex = 47;
            this.label16.Text = "-90";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1256, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 13);
            this.label17.TabIndex = 48;
            this.label17.Text = "90";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1256, 104);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(19, 13);
            this.label18.TabIndex = 49;
            this.label18.Text = "90";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1256, 153);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(19, 13);
            this.label19.TabIndex = 50;
            this.label19.Text = "90";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(1256, 206);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 13);
            this.label20.TabIndex = 51;
            this.label20.Text = "90";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(1256, 257);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(19, 13);
            this.label21.TabIndex = 52;
            this.label21.Text = "90";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 604);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.trackBar5);
            this.Controls.Add(this.trackBar4);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSetPosition);
            this.Controls.Add(this.txtGripper);
            this.Controls.Add(this.txtWrist);
            this.Controls.Add(this.txtElbow);
            this.Controls.Add(this.txtShoulder);
            this.Controls.Add(this.txtBase);
            this.Controls.Add(this.gbArtifitialInteligence);
            this.Controls.Add(this.gbRobotController);
            this.Controls.Add(this.btnSeeTheRobotView);
            this.Controls.Add(this.lblDesition);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pbProcessed);
            this.Controls.Add(this.btnProcessImage);
            this.Controls.Add(this.pbMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Nine Man\'s Morris";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessed)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbRobotController.ResumeLayout(false);
            this.gbRobotController.PerformLayout();
            this.gbArtifitialInteligence.ResumeLayout(false);
            this.gbArtifitialInteligence.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Button btnProcessImage;
        private System.Windows.Forms.PictureBox pbProcessed;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslblStatusProces;
        private System.Windows.Forms.ToolStripStatusLabel tsslblPlayerDiffculty;
        private System.Windows.Forms.Label lblDesition;
        private System.Windows.Forms.Button btnSeeTheRobotView;
        private System.Windows.Forms.GroupBox gbRobotController;
        private System.Windows.Forms.Button btnSearchForPorts;
        private System.Windows.Forms.Label lblSerialPortName;
        private System.Windows.Forms.ComboBox cmbPortNames;
        private System.Windows.Forms.CheckBox chbEnableCmdSending;
        private System.Windows.Forms.Button btnMakeMove;
        private System.Windows.Forms.GroupBox gbArtifitialInteligence;
        private System.Windows.Forms.Label lblComputerPlayerColor;
        private System.Windows.Forms.Label lblHumanPlayerColor;
        private System.Windows.Forms.ComboBox cbHumanPlayerColor;
        private System.Windows.Forms.ComboBox cbComputerPlayerColor;
        private System.Windows.Forms.TextBox txtPlayerDiffculty;
        private System.Windows.Forms.Label lblDiffculty;
        private System.Windows.Forms.Button btnCalculateNextMove;
        private System.Windows.Forms.ToolStripStatusLabel tsslblSerialStatus;
        private System.Windows.Forms.Button btnSendCommand;
        private System.Windows.Forms.Button btnMoveRobotToHome;
        private System.Windows.Forms.TextBox txtBase;
        private System.Windows.Forms.TextBox txtShoulder;
        private System.Windows.Forms.TextBox txtElbow;
        private System.Windows.Forms.TextBox txtWrist;
        private System.Windows.Forms.TextBox txtGripper;
        private System.Windows.Forms.Button btnSetPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
    }
}

