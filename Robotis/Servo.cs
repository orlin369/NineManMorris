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

        private byte id = 0;

        #endregion

        #region Properties

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

        public byte ID
        {
            get
            {
                this.id = (byte)this.controler.ReadWord(this.id, ControlTable.ID);
                return this.id;
            }
            set
            {
                this.id = value;
                //this.controler.WriteWord(this.id, ControlTable.ID, this.id);
            }
        } 
        
        public byte BaudRate
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.BaudRate);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.BaudRate, value);
            }
        }
        
        public byte ReturnDelayTime
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.ReturnDelayTime);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.ReturnDelayTime, value);
            }
        }
        
        public ushort CWAngleLimit
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.CWAngleLimitH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.CWAngleLimitL);
                
                return (ushort)((high << 8) | low);
            }
            set
            {
                byte high = (byte)(value >> 8);
                byte low = (byte)value;

                this.controler.WriteByte(this.id, ControlTable.CWAngleLimitL, low);
                this.controler.WriteByte(this.id, ControlTable.CWAngleLimitH, high);
            }
        }
        
        public ushort CCWAngleLimit
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.CCWAngleLimitH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.CCWAngleLimitL);

                return (ushort)((high << 8) | low);
            }
            set
            {
                byte high = (byte)(value >> 8);
                byte low = (byte)value;

                this.controler.WriteByte(this.id, ControlTable.CCWAngleLimitL, low);
                this.controler.WriteByte(this.id, ControlTable.CCWAngleLimitH, high);
            }
        }
        
        public ushort HighestLimitTemperature
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.HighestLimitTemperature);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.HighestLimitTemperature, value);
            }
        }
        
        public byte LowestLimitVoltage
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.LowestLimitVoltage);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.LowestLimitVoltage, value);
            }
        }
        
        public byte HighestLimitVoltage
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.HighestLimitVoltage);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.HighestLimitVoltage, value);
            }
        }
        
        public ushort MaxTorque
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.MaxTorqueH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.MaxTorqueL);

                return (ushort)((high << 8) | low);
            }
            set
            {
                byte high = (byte)(value >> 8);
                byte low = (byte)value;

                this.controler.WriteByte(this.id, ControlTable.MaxTorqueL, low);
                this.controler.WriteByte(this.id, ControlTable.MaxTorqueH, high);
            }
        }
        
        public byte StatusReturnLevel
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.StatusReturnLevel);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.StatusReturnLevel, value);
            }
        }
        
        public byte AlarmLED
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.AlarmLED);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.AlarmLED, value);
            }
        }
        
        public byte AlarmShutdown
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.AlarmShutdown);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.AlarmShutdown, value);
            }
        }
        
        public ushort DownCalibration
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.DownCalibrationH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.DownCalibrationL);

                return (ushort)((high << 8) | low);
            }
        }
        
        public int UpCalibration
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.UpCalibrationH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.UpCalibrationL);

                return (ushort)((high << 8) | low);
            }
        }
        
        public int TorqueEnable
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.TorqueEnable);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.TorqueEnable, value);
            }
        }
        
        public byte LED
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.LED);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.LED, value);
            }
        }
        
        public byte CWComplianceMargin
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.CWComplianceMargin);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.CWComplianceMargin, value);
            }
        }
        
        public byte CCWComplianceMargin
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.CCWComplianceMargin);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.CCWComplianceMargin, value);
            }
        }
        
        public byte CWComplianceSlope
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.CWComplianceSlope);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.CWComplianceSlope, value);
            }
        }
        
        public byte CCWComplianceSlope
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.CCWComplianceSlope);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.CCWComplianceSlope, value);
            }
        }
        
        public int GoalPosition
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.GoalPositionH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.GoalPositionL);

                return (ushort)((high << 8) | low);
            }
            set
            {
                byte high = (byte)(value >> 8);
                byte low = (byte)value;

                this.controler.WriteByte(this.id, ControlTable.GoalPositionL, low);
                this.controler.WriteByte(this.id, ControlTable.GoalPositionH, high);
            }
        }
        
        public ushort MovingSpeed
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.MovingSpeedH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.MovingSpeedL);

                return (ushort)((high << 8) | low);
            }
            set
            {
                byte high = (byte)(value >> 8);
                byte low = (byte)value;

                this.controler.WriteByte(this.id, ControlTable.MovingSpeedL, low);
                this.controler.WriteByte(this.id, ControlTable.MovingSpeedH, high);
            }
        }
        
        public ushort TorqueLimit
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.MovingSpeedH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.MovingSpeedL);

                return (ushort)((high << 8) | low);
            }
            set
            {
                byte high = (byte)(value >> 8);
                byte low = (byte)value;

                this.controler.WriteByte(this.id, ControlTable.MovingSpeedL, low);
                this.controler.WriteByte(this.id, ControlTable.MovingSpeedH, high);
            }
        }
        
        public int PresentPosition
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.PresentPositionH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.PresentPositionL);

                return (ushort)((high << 8) | low);
            }
        }
        
        public int PresentSpeed
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.PresentSpeedH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.PresentSpeedL);

                return (ushort)((high << 8) | low);
            }
        }
        
        public int PresentLoad
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.PresentLoadH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.PresentLoadL);

                return (ushort)((high << 8) | low);
            }
        }
        
        public byte PresentVoltage
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.PresentVoltage);
            }
        }
        
        public byte PresentTemperature
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.PresentTemperature);
            }
        }
        
        public byte RegisteredInstruction
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.RegisteredInstruction);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.RegisteredInstruction, value);
            }
        }
        
        public byte Moving
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.Moving);
            }
        }
        
        public byte Lock
        {
            get
            {
                return (byte)this.controler.ReadByte(this.id, ControlTable.Lock);
            }
            set
            {
                this.controler.WriteByte(this.id, ControlTable.Lock, value);
            }
        }
        
        public ushort Punch
        {
            get
            {
                ushort high = (ushort)this.controler.ReadByte(this.id, ControlTable.PunchH);
                byte low = (byte)this.controler.ReadByte(this.id, ControlTable.PunchL);

                return (ushort)((high << 8) | low);
            }
            set
            {
                byte high = (byte)(value >> 8);
                byte low = (byte)value;

                this.controler.WriteByte(this.id, ControlTable.PunchL, low);
                this.controler.WriteByte(this.id, ControlTable.PunchH, high);
            }
        }

        #endregion

        #region Constructor / Destructor

        public Servo(DynamixelController controler, byte id)
        {
            // Create controller.
            this.controler = controler;
            // Current ID.
            this.id = id;
        }

        #endregion

    }
}
