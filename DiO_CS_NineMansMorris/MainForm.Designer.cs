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
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessed)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.gbRobotController.SuspendLayout();
            this.gbArtifitialInteligence.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMain.Location = new System.Drawing.Point(12, 12);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(600, 450);
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
            this.statusStrip1.Size = new System.Drawing.Size(1050, 22);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 604);
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
    }
}

