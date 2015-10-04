using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Camera image source.
using ImageSource;

// Vision System
using VisionSystem;

// AI
using ArtifitialInteligence;

// Robot controller
using Robot;
using Robot.Application;
using Robot.RobotArm;
using Robotis;
using Robot.CorrdinateSystems;

namespace DiO_CS_NineMansMorris
{
    public partial class MainForm : Form
    {
        #region Variables

        #region Image source

        // Size 679 x 960 ... Comming from facebook.com
        //private string FbGoodImage = "https://fbcdn-sphotos-h-a.akamaihd.net/hphotos-ak-ash4/t1/397524_1819233898500_682258959_n.jpg";
        // Size 640 x 480 ... Comming from facebook.com
        //private string fbFromCamera = "https://fbcdn-sphotos-b-a.akamaihd.net/hphotos-ak-ash4/t1/397524_1819233858499_1893872488_n.jpg";
        // File
        //string ImagePath = @"C:\Users\Orlin\Pictures\WorkImages\testBoard5.JPG";

        /// <summary>
        /// IP address of the camera.
        /// </summary>
        private string ipCameraAddress = "http://192.168.1.1/cgi-bin/image.jpg";



        //src net 192.168.0.0 mask 255.255.255.0
        //http.request.full_uri
        private string ipMobileCamera = "http://192.168.0.104:8080/photoaf.jpg";
        //http://192.168.0.104:8080/enabletorch
        //http://192.168.0.104:8080/disabletorch

        #endregion

        #region Vision system

        /// <summary>
        /// Camera
        /// </summary>
        private ICaptureDevice camera;

        /// <summary>
        /// Search configuration for the vision engine.
        /// </summary>
        private VisionSystem.SearchConfigurator boardConfiguration;

        /// <summary>
        /// Play board
        /// </summary>
        private VisionSystem.Board playerVS;

        /// <summary>
        /// Board configuration of the players.
        /// </summary>
        private VisionSystem.BoardConfiguration currentBoardConfiguration;

        /// <summary>
        /// HEre will be safe image that is get from the camera.
        /// </summary>
        Bitmap imageFromCamera = null;

        /// <summary>
        /// Output image after recognition
        /// </summary>
        Bitmap outputImage = null;

        #endregion

        #region Artifitial Inteligence

        /// <summary>
        ///  AI to make naext move.
        /// </summary>
        private ArtifitialInteligence.GameController PlayerAI;
        /// <summary>
        /// How much time you will give to the AI to make desition.
        /// </summary>
        private int PlayerTimeout = 200;
        /// <summary>
        /// How deep the AI will get down to find the better solution.
        /// </summary>
        private byte PlayerDiffculty = 3;
        /// <summary>
        /// The color of the human player.
        /// 1 - Means RED.
        /// </summary>
        private int HumanIndex = 1;
        /// <summary>
        /// The color of the artifitial player.
        /// 2 - Means GRREN.
        /// </summary>
        private int ComputerIndex = 2;
        /// <summary>
        /// Next move of the artifitial inteligence.
        /// </summary>
        Move NewMove;

        #endregion

        #region Robot

        /// <summary>
        /// Robot that will make the next move.
        /// </summary>
        private NineMansMorrisPlayer playerRobot;

        /// <summary>
        /// Robot serial port.
        /// </summary>
        private string robotSerialPortName = "COM26";

        /// <summary>
        /// 
        /// </summary>
        private bool enableSending = false;

        /// <summary>
        /// 
        /// </summary>
        private Joints jointIDs = new Joints(1, 2, 3, 4, 5);

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            //this.camera = new IpCamera(new Uri(this.ipCameraAddress));
            this.camera = new IpCamera(new Uri(this.ipMobileCamera));
        }

        #endregion

        #region Private

        /// <summary>
        /// 
        /// </summary>
        private void FillThePlayers()
        {
            for (int index = 1; index < 5; index++)
            {
                cbHumanPlayerColor.Items.Add(index);
            }
        }

        /// <summary>
        /// Search for ports on the PC.
        /// </summary>
        private void SearchForPorts()
        {
            cmbPortNames.Items.Clear();

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();

            if (portNames.Length == 0)
            {
                cmbPortNames.Text = "No Ports";
                return;
            }

            foreach (string item in portNames)
            {
                //store the each retrieved available prot names into the Listbox...
                cmbPortNames.Items.Add(item);
            }

            // 
            cmbPortNames.Text = cmbPortNames.Items[0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Move"></param>
        private void WriteTheResultFromTheAI(Move Move)
        {
            string message = "";

            switch (Move.GetMoveType())
            {
                case MoveType.Drop:
                    message = String.Format("Робота, си взема фигура и я поставя на позиция {0} от игралното поле.", Move.GetEndPosition().ToString());
                    break;

                case MoveType.DropAndCapture:
                    message = String.Format("Робота, си взема фигура и я поставя на позиция {0} от игралното поле,\nи взема фигура на опонента си от {1}", Move.GetEndPosition().ToString(), Move.GetCapturePosition());
                    break;

                case MoveType.Move:
                    message = String.Format("Робота, си взема фигура от {0}, и я мести до позиция {1}.", Move.GetStartPosition().ToString(), Move.GetEndPosition().ToString());
                    break;

                case MoveType.MoveAndCapture:
                    message = String.Format("Робота, си взема фигура и я мести по дъската от позиция {0}.", Move.GetEndPosition().ToString());
                    break;
            }


            //
            Console.WriteLine("Type: {0}", Move.GetMoveType().ToString());
            Console.WriteLine("Start position: {0}", Move.GetStartPosition().ToString());
            Console.WriteLine("End Position: {0}", Move.GetEndPosition().ToString());
            Console.WriteLine("Capture Position: {0}", Move.GetCapturePosition());
            Console.WriteLine(message);

            // Apply the message to the UI.
            this.lblDesition.Text = message;
            //this.lblLong.Text = message;
        }

        /// <summary>
        /// Test program fr the robot.
        /// </summary>
        /// <param name="arm"></param>
        private void TestProgram(DynamixelArm arm)
        {
            
            //Steve Stavrev
            /*
            arm.LJInterpolation(new Joint(15.0d, 0.0d, 0.0d, 0.0d, 0.0d), 20);
            System.Threading.Thread.Sleep(1000);
            arm.LJInterpolation(new Joint(15.0d, 0.0d, 0.0d, 0.0d, 0.0d), 20);
            System.Threading.Thread.Sleep(1000);

            arm.LJInterpolation(new Joint(0.0d, 0.0d, 0.0d, 0.0d, 90.0d), 20);
            System.Threading.Thread.Sleep(1000);

            arm.LJInterpolation(new Joint(0.0d, 30.0d, 0.0d, 0.0d, 0.0d), 20);
            System.Threading.Thread.Sleep(1000);
            arm.LJInterpolation(new Joint(0.0d, 30.0d, 0.0d, 0.0d, 0.0d), 20);
            System.Threading.Thread.Sleep(1000);

            arm.LJInterpolation(new Joint(0.0d, 0.0d, 0.0d, 0.0d, 0.0d), 20);
            System.Threading.Thread.Sleep(1000);


            arm.LJInterpolation(new Joint(0.0d, 0.0d, 35.0d, 0.0d, 0.0d), 20);
            System.Threading.Thread.Sleep(1000);
            arm.LJInterpolation(new Joint(0.0d, 0.0d, 35.0d, 0.0d, 0.0d), 20);
            System.Threading.Thread.Sleep(1000);

            arm.LJInterpolation(new Joint(0.0d, 0.0d, 0.0d, 20.0d, 0.0d), 20);
            System.Threading.Thread.Sleep(1000);
            arm.LJInterpolation(new Joint(0.0d, 0.0d, 0.0d, 10.0d, 0.0d), 20);
            System.Threading.Thread.Sleep(1000);
            */

            /*
            //Dzhuvi Juniour
            arm.SetJPosition(new JointConfiguration(-20.0d, 0.0d, 0.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);
            arm.SetJPosition(new JointConfiguration(0.0d, 15.0d, 0.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);
            arm.SetJPosition(new JointConfiguration(0.0d, 15.0d, 0.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);

            arm.SetJPosition(new JointConfiguration(0.0d, 0.0d, 0.0d, 0.0d, 90.0d));
            System.Threading.Thread.Sleep(1000);
            arm.SetJPosition(new JointConfiguration(0.0d, 0.0d, 20.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);

            arm.SetJPosition(new JointConfiguration(0.0d, 0.0d, 0.0d, 0.0d, 45.0d));
            System.Threading.Thread.Sleep(1000);

            arm.SetJPosition(new JointConfiguration(0.0d, 0.0d, -20.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);
            arm.SetJPosition(new JointConfiguration(0.0d, -20.0d, 0.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);
            arm.SetJPosition(new JointConfiguration(0.0d, 0.0d, 0.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);
            //end Dzhuvi Juniour
            */
            /*arm.SetJPosition(new JointConfiguration(10.0d, 10.0d, 0.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);
            arm.SetJPosition(new JointConfiguration(-10.0d, 0.0d, 0.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);
            arm.SetJPosition(new JointConfiguration(0.0d, 0.0d, 0.0d, 0.0d, 0.0d));
            System.Threading.Thread.Sleep(1000);*/

            // Plamen Velikov

            //int i = -1;
            //while (true)
            //{
            //    const double maxAngle = 40.0d;
            //    const double step = 1.0d;

            //    for (double angle = 0; angle < maxAngle; angle += step)
            //    {
            //        arm.SetJPosition(new JointConfiguration(angle * i, angle, angle, angle, angle));
            //        //System.Threading.Thread.Sleep(1);
            //    }

            //    for (double angle = maxAngle; angle > 0; angle -= step)
            //    {
            //        arm.SetJPosition(new JointConfiguration(angle * i, angle, angle, angle, angle));
            //        //System.Threading.Thread.Sleep(1);
            //    }

            //    i = i > 0 ? -1 : 1;
            //}

            //for (int i = 0; i < length; i++)
            //{
            //    arm.SetJPosition(new JointConfiguration(0.0d, 0.0d, 0.0d, 0.0d, 40.0d));
            //    System.Threading.Thread.Sleep(1000);
            //    arm.SetJPosition(new JointConfiguration(0.0d, 0.0d, 0.0d, 0.0d, -40.0d));
            //    System.Threading.Thread.Sleep(1000);
            //}

            // Krasimir Yosifov: 29.11.2014
            /*
            for (int i = 0; i < 360; i+=5)
            {
                arm.SetJPosition(new JointConfiguration(Math.Sin(i * 3.14 / 180) * 30, 0, (double)Math.Cos(i * 3.14 / 180) * 30, 0.0d, 0.0d));
                System.Threading.Thread.Sleep(1);
            }
            */
            /*
             * //Krasimir ...
            arm.SetJPosition(new Joint(0, 0, 0, 0, 120));
            for (int i = 0; i < 36; i++)
            {
                arm.SetJPosition(new Joint(0, i, 0, i, 120));
                System.Threading.Thread.Sleep(1);
            }
            System.Threading.Thread.Sleep(500);
            arm.SetJPosition(new Joint(0, 36, 0, 36, 52));
            System.Threading.Thread.Sleep(500);
            for (int i = 36; i > 20; i--)
            {
                arm.SetJPosition(new Joint(0, i, 0, 36, 52));
                System.Threading.Thread.Sleep(1);
            }

            for (int i = 0; i < 12; i++)
            {
                arm.SetJPosition(new Joint(i*5, 20, 0, 36-i, 52));
                System.Threading.Thread.Sleep(10);
            }
            arm.SetJPosition(new Joint(55, 20, 0, 27, 120));
            System.Threading.Thread.Sleep(1000);
            */

            // Linda Massarwe ...
            /*
            //arm.LJInterpolation(new Joint(0, 0, 0, 5, 0), 20.0d);
            arm.LJInterpolation(new Joint(62.0d, -27.0d, 5, 15, 0.0d), 20.0d);
            System.Threading.Thread.Sleep(2000);

            arm.LJInterpolation(new Joint(50, 23, 0, 0, 0), 20.0d);
            System.Threading.Thread.Sleep(3000);

            arm.LJInterpolation(new Joint(0.0d, 0.0d, 0.0d, 0.0d, 0.0d), 20);
            */

            // Angel Dimov
            arm.LJInterpolation(new Joint(35.0d, 0.0d, 0.0d, 0.0d, 75.0d), 20);
            System.Threading.Thread.Sleep(3000);


            //arm.GoToHome();
            Decart pos = arm.GetDPosition();
            Console.WriteLine("XYZPR: {0}", pos.ToString());
        }

        #endregion

        #region Robot events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myRobot_RobotServoState(object sender, ErrorEvent e)
        {
            Console.WriteLine("Servo state: {0}", e.State);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myRobot_RobotComState(object sender, StateEvent e)
        {
            tsslblSerialStatus.Text = "";
            tsslblSerialStatus.Text = e.State;
        }

        #endregion

        #region UI

        #region Form

        /// <summary>
        /// Closing form event.
        /// </summary>
        /// <param name="sender">Sender class</param>
        /// <param name="e">Sender arguments</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Loging.Log.CreateRecord("DiO_CS_NineMansMorris.MainForm.MainForm_FormClosing", "Application is closing.", Loging.LogMessageTypes.Info, true);
        }

        /// <summary>
        /// Runs when form load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SearchForPorts();

            this.FillThePlayers();
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Proces the image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcessImage_Click(object sender, EventArgs e)
        {
            try
            {
                // Get image from image source.
                this.imageFromCamera = this.camera.Capture();

                // Create search engine configuration.
                this.boardConfiguration = new SearchConfigurator();

                // Create processor.
                this.playerVS = new VisionSystem.Board(this.boardConfiguration);

                // Process the image and get the board configuration.
                this.currentBoardConfiguration = playerVS.FindBoard(this.imageFromCamera, out this.outputImage);
                this.currentBoardConfiguration.Black = this.ComputerIndex;
                this.currentBoardConfiguration.White = this.HumanIndex;

                // Apply the processed image.
                pbMain.Image = new Bitmap(this.imageFromCamera, pbMain.Size);

                // Apply the processed image.
                pbProcessed.Image = this.outputImage;

                // Print configuration ti the console.
                Console.WriteLine(this.currentBoardConfiguration.GetBoardAsString());

                // Upadate UI
                tsslblStatusProces.Text = "Last action: search for the board.";
            }
            catch (Exception exception)
            {
                tsslblStatusProces.Text = String.Format("Exception: {0}", exception.Message);
                Loging.Log.CreateRecord("MainForm.btnProcessImage_Click", String.Format("Exception: {0}", exception.Message), Loging.LogMessageTypes.Error);
            }

        }

        /// <summary>
        /// Calculate the next move via AI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculateNextMove_Click(object sender, EventArgs e)
        {
            if (cbHumanPlayerColor.SelectedIndex < 0)
            {
                MessageBox.Show("First select the human color.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cbComputerPlayerColor.SelectedIndex < 0)
            {
                MessageBox.Show("Now select the computer color.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                //
                this.PlayerDiffculty = Convert.ToByte(txtPlayerDiffculty.Text);

                // Update UI
                tsslblPlayerDiffculty.Text = this.PlayerDiffculty.ToString();

                //
                this.PlayerAI = new GameController(this.PlayerTimeout, this.PlayerDiffculty);

                // Calculate move.
                //this.NewMove = PlayerAI.PassBoard(this.CurrentBoardConfiguration.Figure, Convert.ToInt32(cbHumanPlayerColor.SelectedIndex + 1), Convert.ToInt32(cbComputerPlayerColor.SelectedIndex + 1)); // Convert.ToInt32(cbHumanPlayer.SelectedValue), Convert.ToInt32(cbComputerPlayer.SelectedValue));
                this.NewMove = PlayerAI.PassBoard(this.currentBoardConfiguration.Figure, Convert.ToInt32(cbHumanPlayerColor.Text), Convert.ToInt32(cbComputerPlayerColor.Text));

                // Deside that there is no good move.
                if (this.NewMove == null)
                {
                    tsslblStatusProces.Text = "No move";
                    Loging.Log.CreateRecord("Program.MainForm.btnCalculateNextMove_Click", "No move ...", Loging.LogMessageTypes.Warning);
                    return;
                }

                this.WriteTheResultFromTheAI(this.NewMove);

                // Upadate UI
                tsslblStatusProces.Text = "Last action: calculating best move.";
            }
            catch (Exception exception)
            {
                tsslblStatusProces.Text = String.Format("Exception: {0}", exception.Message);
                Loging.Log.CreateRecord("MainForm.btnMakeMove_Click", String.Format("Exception: {0}", exception.Message), Loging.LogMessageTypes.Error);
            }
        }

        /// <summary>
        /// Make the robot to do the move.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMakeMove_Click(object sender, EventArgs e)
        {
            try
            {
                int comIndex = Convert.ToInt32(this.robotSerialPortName.Replace("COM", ""));
                Robotis.DynamixelController controlr = new Robotis.DynamixelController(comIndex, 1);
                this.playerRobot = new NineMansMorrisPlayer(controlr, this.jointIDs);

                ArtifitialInteligence.MoveType type = this.NewMove.GetMoveType();
                ArtifitialInteligence.BoardIndex startPosition = this.NewMove.GetStartPosition();
                ArtifitialInteligence.BoardIndex endPosition = this.NewMove.GetEndPosition();
                ArtifitialInteligence.BoardIndex capturePosition = this.NewMove.GetCapturePosition();

                switch (type)
                {
                    case ArtifitialInteligence.MoveType.Drop:
                        // TODO: Get figure from the shop and drop it on to EndPosition of the move object.

                        // Update UI.
                        lblDesition.Text = String.Format("Робота взема фигура и я поставя на позиция {0}.", endPosition.ToString());

                        // Commands for the robot.
                        if (enableSending)
                        {
                            playerRobot.GetFigureFromShop();
                            playerRobot.DropFigureOnBoard((int)endPosition);
                        }
                        // END
                        break;

                    case ArtifitialInteligence.MoveType.DropAndCapture:
                        // TODO: See the coment below (Drop and capture)
                        /*
                         * If the end position == drop position then
                         *  Remove the figure from the board and throw it to thecaptured figures shop.
                         * Get figure from the shop and drop it on to EndPosition of the move objec.
                         */

                        // Update UI.
                        lblDesition.Text = String.Format("Робота взема фигура и я поставя на позиция {0} от игралното поле\nи взема фигура на опонента си от {1}", endPosition.ToString(), capturePosition.ToString());

                        //Commands for the robot.
                        if (this.enableSending)
                        {
                            playerRobot.GetFigureFromShop();
                            playerRobot.DropFigureOnBoard((int)endPosition);
                            playerRobot.GetFigureFromBoard((int)capturePosition);
                            playerRobot.DropFigureToTheCaptureShop();
                        }
                        // END
                        break;

                    case ArtifitialInteligence.MoveType.Move:
                        // TODO: Get the figure of the start position of the move objet and drop it to the end position.

                        // Update UI.
                        lblDesition.Text = String.Format("Робота взема фигура и я мести по дъската от позиция {0}.", endPosition.ToString());

                        //Commands for the robot.
                        if (this.enableSending)
                        {

                            playerRobot.GetFigureFromBoard((int)startPosition);
                            playerRobot.DropFigureOnBoard((int)endPosition);
                        }
                        // END
                        break;

                    case ArtifitialInteligence.MoveType.MoveAndCapture:
                        // TODO: Get figure frm start position, move it to the end position. Get the figure from capture position and drop it to the capture shop.

                        // Update UI.
                        lblDesition.Text = String.Format("Робота взема фигура и я мести по дъската от позиция {0}.", endPosition.ToString());

                        //Commands for the robot.
                        if (this.enableSending)
                        {

                            playerRobot.GetFigureFromBoard((int)startPosition);
                            playerRobot.DropFigureOnBoard((int)endPosition);
                            playerRobot.GetFigureFromBoard((int)capturePosition);
                            playerRobot.DropFigureToTheCaptureShop();
                        }
                        break;
                }

                // Upadate UI
                tsslblStatusProces.Text = "Last action: generating command.";
            }
            catch (Exception exception)
            {
                tsslblStatusProces.Text = String.Format("Exception: {0}", exception.Message);
                Loging.Log.CreateRecord("MainForm.btnMakeMove_Click", String.Format("Exception: {0}", exception.Message), Loging.LogMessageTypes.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeeTheRobotView_Click(object sender, EventArgs e)
        {
            try
            {
                // Get image from image source.
                this.imageFromCamera = this.camera.Capture();
                Console.WriteLine("Size: {0}", this.imageFromCamera.Size);

                // Apply the processed image.
                pbMain.Image = new Bitmap(this.imageFromCamera, new Size(600, 450));

                // Upadate UI
                tsslblStatusProces.Text = "Last action: whatcing from camera.";

            }
            catch (Exception exception)
            {
                tsslblStatusProces.Text = String.Format("Exception: {0}", exception.Message);
                Loging.Log.CreateRecord("MainForm.btnSeeTheRobotView_Click", String.Format("Exception: {0}", exception.Message), Loging.LogMessageTypes.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchForPorts_Click(object sender, EventArgs e)
        {
            this.SearchForPorts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            if (!this.enableSending)
            {
                return;
            }

            int comIndex = Convert.ToInt32(this.robotSerialPortName.Replace("COM", ""));
            tsslblSerialStatus.Text = "";

            try
            {
                
                using (DynamixelController myDynamixel = new DynamixelController(comIndex, 1))
                {
                    DynamixelArm myArm = new DynamixelArm(myDynamixel, this.jointIDs);
                    myArm.ComState += new EventHandler<StateEvent>(myRobot_RobotComState);
                    myArm.ServoState += new EventHandler<ErrorEvent>(myRobot_RobotServoState);
                    /*
                    for (int iterator = 0; iterator < 10; iterator++)
                    {
                        myArm.LJInterpolation(new Joint(20.0d, -20.0d, 20.0d, 20.0d, 0.0d), 20.0d);
                        myArm.LJInterpolation(new Joint(-40.0d, 40.0d, -40.0d, -40.0d, 0.0d), 40.0d);
                        myArm.LJInterpolation(new Joint(20.0d, -20.0d, 20.0d, 20.0d, 0.0d), 20.0d);
                    }
                    */
                    myArm.GoToHome();

                    myArm.ComState -= new EventHandler<StateEvent>(myRobot_RobotComState);
                    myArm.ServoState -= new EventHandler<ErrorEvent>(myRobot_RobotServoState);

                    this.TestProgram(myArm);
                }
              
                Joint homeJ = new Joint(0.0d, 0.0d, 0.0d, 0.0d, 0.0d);
                Robot.Experimantal.ArmConfiguration myConfig = new Robot.Experimantal.ArmConfiguration(47, 47, 142, 110);
                Robot.Experimantal.ArmKinematics myKinematics = new Robot.Experimantal.ArmKinematics(myConfig);
                Decart homeD = myKinematics.Forward(homeJ);
                string values = homeD.ToString();
                Console.WriteLine("Values: {0}", values);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Robot: {0}", exception.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveRobotToHome_Click(object sender, EventArgs e)
        {
            if (!this.enableSending)
            {
                return;
            }

            int comIndex = Convert.ToInt32(this.robotSerialPortName.Replace("COM", ""));
            try
            {
                using (DynamixelController myDynamixel = new DynamixelController(comIndex, 1))
                {
                    DynamixelArm myArm = new DynamixelArm(myDynamixel, this.jointIDs);
                    //System.Threading.Thread.Sleep(2000);
                    myArm.GoToHome();
                    Decart pos = myArm.GetDPosition();
                    Console.WriteLine("XYZPR: {0}", pos.ToString());
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Robot: {0}", exception.Message);
            }
        }

        #endregion

        #region Check

        /// <summary>
        /// This event refill the second combo box with the posible choice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbHumanPlayerColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbComputerPlayerColor.Items.Clear();

            for (int index = 1; index < 5; index++)
            {
                cbComputerPlayerColor.Items.Add(index);
            }

            cbComputerPlayerColor.Items.RemoveAt(cbHumanPlayerColor.SelectedIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbEnableCmdSending_Click(object sender, EventArgs e)
        {
            this.enableSending = chbEnableCmdSending.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPortNames_SelectedValueChanged(object sender, EventArgs e)
        {
            this.robotSerialPortName = cmbPortNames.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbEnableCmdSending_CheckedChanged(object sender, EventArgs e)
        {
            this.enableSending = chbEnableCmdSending.Checked;
        }

        #endregion

        #endregion

        //Angel Dimov
        private void btnSetPosition_Click(object sender, EventArgs e)
        {
            double baseJoint = double.Parse(this.txtBase.Text);
            double shoulderJoint = double.Parse(this.txtShoulder.Text);
            double elbowJoint = double.Parse(this.txtElbow.Text);
            double wristJoint = double.Parse(this.txtWrist.Text);
            double gripperJoint = double.Parse(this.txtGripper.Text);

            this.trackBar1.Value = (int)baseJoint;
            this.trackBar2.Value = (int)shoulderJoint;
            this.trackBar3.Value = (int)elbowJoint;
            this.trackBar4.Value = (int)wristJoint;
            this.trackBar5.Value = (int)gripperJoint;

            if (!this.enableSending)
            {
                return;
            }

            int comIndex = Convert.ToInt32(this.robotSerialPortName.Replace("COM", ""));
            try
            {
                using (DynamixelController myDynamixel = new DynamixelController(comIndex, 1))
                {
                    DynamixelArm myArm = new DynamixelArm(myDynamixel, this.jointIDs);

                    //baseJoint -= tempBase;
                    //shoulderJoint -= tempShoulder;
                    //elbowJoint -= tempShoulder;
                    //wristJoint -= tempWrist;
                    //gripperJoint -= tempGripper;

                    myArm.LJInterpolation(new Joint(baseJoint, shoulderJoint, elbowJoint, wristJoint, gripperJoint), 20);

                    Decart pos = myArm.GetDPosition();
                    Console.WriteLine("XYZPR: {0}", pos.ToString());
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Robot: {0}", exception.Message);
            }

        }

        private void trbUpdatePosition_MouseUp(object sender, MouseEventArgs e)
        {
            double baseJoint = this.trackBar1.Value;
            double shoulderJoint = this.trackBar2.Value;
            double elbowJoint = this.trackBar3.Value;
            double wristJoint = this.trackBar4.Value;
            double gripperJoint = this.trackBar5.Value;

            if (!this.enableSending)
            {
                return;
            }

            int comIndex = Convert.ToInt32(this.robotSerialPortName.Replace("COM", ""));
            try
            {
                using (DynamixelController myDynamixel = new DynamixelController(comIndex, 1))
                {
                    DynamixelArm myArm = new DynamixelArm(myDynamixel, this.jointIDs);

                    //baseJoint -= tempBase;
                    //shoulderJoint -= tempShoulder;
                    //elbowJoint -= tempShoulder;
                    //wristJoint -= tempWrist;
                    //gripperJoint -= tempGripper;

                    myArm.LJInterpolation(new Joint(baseJoint, shoulderJoint, elbowJoint, wristJoint, gripperJoint), 20);

                    Decart pos = myArm.GetDPosition();
                    Console.WriteLine("XYZPR: {0}", pos.ToString());
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Robot: {0}", exception.Message);
            }
        }
    }
}
