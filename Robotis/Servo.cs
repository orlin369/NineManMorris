using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robotis
{
    public class Servo
    {
        #region Variables

        private DynamixelController controler;

        private int id = 0;

        #endregion

        #region Property

        public int ModelNumber
        {
            get
            {
                return this.controler.ReadWord(this.id, ControlTable.ModelNumberL);
            }
        }
        public int Version
        {
            get
            {
                return this.controler.ReadWord(this.id, ControlTable.VersionOfFirmware);
            }
        }

        /// <summary>
        /// ID of the servo.
        /// </summary>
        public int ID
        {
            get
            {
                return this.id;
                //return this.controler.ReadWord(this.id, ControlTable.ID);
            }
            set
            {
                this.id = value;
                //this.controler.WriteWord(this.id, ControlTable.ID, this.id);
            }
        } 

        public byte BaudRate { get; set; }
        public byte ReturnDelayTime { get; set; }
        public ushort CWAngleLimit { get; set; }
        public ushort CCWAngleLimit { get; set; }
        public ushort HighestLimitTemperature { get; set; } 
        public byte LowestLimitVoltage { get; set; }
        public byte HighestLimitVoltage { get; set; }
        public ushort MaxTorque { get; set; }
        public byte StatusReturnLevel { get; set; }
        public byte AlarmLED { get; set; }
        public byte AlarmShutdown { get; set; }
        public int DownCalibration
        {
            get
            {
                return this.controler.ReadWord(this.id, ControlTable.DownCalibrationL);
            }
        }

        public int UpCalibration
        {
            get
            {
                return this.controler.ReadWord(this.id, ControlTable.UpCalibrationL);
            }
        }

        public byte TorqueEnable { get; set; } 
        public byte LED { get; set; } 
        public byte CWComplianceMargin { get; set; }
        public byte CCWComplianceMargin { get; set; }
        public byte CWComplianceSlope { get; set; }
        public byte CCWComplianceSlope { get; set; }
        public int GoalPosition
        {
            get
            {
                return this.controler.ReadWord(this.id, ControlTable.GoalPositionL);
            }
            
            set
            {
                this.controler.WriteWord(this.id, ControlTable.GoalPositionL, value);
            }
        }
        public ushort MovingSpeed { get; set; }
        public ushort TorqueLimit { get; set; }
        public int PresentPosition
        {
            get
            {
                return this.controler.ReadByte(this.id, ControlTable.PresentPositionL);
            }
        }

        public int PresentSpeed
        {
            get
            {
                return this.controler.ReadWord(this.id, ControlTable.PresentSpeedL);
            }
        }

        public int PresentLoad
        {
            get
            {
                return this.controler.ReadByte(this.id, ControlTable.PresentLoadL);
            }
        }

        public int PresentVoltage
        {
            get
            {
                return this.controler.ReadByte(this.id, ControlTable.PresentVoltage);
            }
        }
        public int PresentTemperature
        {
            get
            {
                return this.controler.ReadByte(this.id, ControlTable.PresentTemperature);
            }
        }
        public byte RegisteredInstruction { get; set; }
        public int Moving
        {
            get
            {
                return this.controler.ReadByte(this.id, ControlTable.Moving);
            }
        }
        public byte Lock { get; set; }
        public ushort Punch { get; set; }

        #endregion

        #region Constructor / Destructor

        public Servo(DynamixelController controler, int id)
        {
            // Create controller.
            this.controler = controler;
            // Current ID.
            this.id = id;
        }

        #endregion
    }
}
