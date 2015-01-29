using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robotis
{
    /// <summary>
    ///  Controll table of AX12.
    /// </summary>
    class ControlTable
    {
        #region EEPROM

        /// <summary>
        /// (0X00) Model Number(L) RD 12(0x0C)
        /// </summary>
        public const int ModelNumberL = 0;

        /// <summary>
        /// (0X01) Model Number(H) RD 0(0x00)
        /// </summary>
        public const int ModelNumberH = 1;
        /// <summary>
        /// (0X02) Version of Firmware RD ?
        /// </summary>
        public const int VersionOfFirmware = 2;

        /// <summary>
        /// (0X03) ID RD,WR 1(0x01)
        /// </summary>
        public const int ID = 3; 

        /// <summary>
        /// (0X04) Baud Rate RD,WR
        /// </summary>
        public const int BaudRate = 4; 
        
        /// <summary>
        /// (0X05) Return Delay Time RD,WR 250(0xFA)
        /// </summary>
        public const int ReturnDelayTime = 5;
 
        /// <summary>
        /// (0X06) CW Angle Limit(L) RD,WR 0(0x00) 
        /// </summary>
        public const int CWAngleLimitL = 6;

        /// <summary>
        /// (0X07) CW Angle Limit(H) RD,WR 0(0x00)
        /// </summary>
        public const int CWAngleLimitH = 7; 
        
        /// <summary>
        /// (0X08) CCW Angle Limit(L) RD,WR 255(0xFF)
        /// </summary>
        public const int CCWAngleLimitL = 8;
 
        /// <summary>
        /// (0X09) CCW Angle Limit(H) RD,WR 3(0x03)
        /// </summary>
        public const int CCWAngleLimitH = 9;

        //public const int Reserved = 10; //(0x0A) (Reserved) - 0(0x00) 
        
        /// <summary>
        /// (0X0B) the Highest Limit Temperature RD,WR
        /// </summary>
        public const int HighestLimitTemperature = 11;
 
        /// <summary>
        /// (0x55) 12(0X0C) the Lowest Limit Voltage RD,WR
        /// </summary>
        public const int LowestLimitVoltage = 85;

        /// <summary>
        /// (0X3C) 13(0X0D) the Highest Limit Voltage RD,WR
        /// </summary>
        public const int HighestLimitVoltage = 60;
 
        /// <summary>
        /// (0xBE) 14(0X0E) Max Torque(L) RD,WR 255(0XFF)
        /// </summary>
        public const int MaxTorqueL = 190;
 
        /// <summary>
        /// (0X0F) Max Torque(H) RD,WR 3(0x03)
        /// </summary>
        public const int MaxTorqueH = 15; 

        /// <summary>
        /// (0X10) Status Return Level RD,WR
        /// </summary>
        public const int StatusReturnLevel = 16;

        /// <summary>
        /// (0X11) Alarm LED RD,WR
        /// </summary>
        public const int AlarmLED = 17; 

        /// <summary>
        /// (0X12) Alarm Shutdown RD,WR
        /// </summary>
        public const int AlarmShutdown  = 18;

        //public const int Reserved = 19; //(0X13) (Reserved) RD,WR 
        
        /// <summary>
        /// (0X14) Down Calibration(L) RD ?
        /// </summary>
        public const int DownCalibrationL = 20;

        /// <summary>
        /// (0X15) Down Calibration(H) RD ?
        /// </summary>
        public const int DownCalibrationH = 21;

        /// <summary>
        /// (0X16) Up Calibration(L) RD ?
        /// </summary>
        public const int UpCalibrationL = 22;

        /// <summary>
        /// (0X17) Up Calibration(H) RD ?
        /// </summary>
        public const int UpCalibrationH = 23;

        #endregion

        #region RAM

        /// <summary>
        /// (0X18) Torque Enable RD,WR
        /// </summary>
        public const int TorqueEnable = 24;

        /// <summary>
        /// (0X19) LED RD,WR
        /// </summary>
        public const int LED = 25;

        /// <summary>
        /// (0X1A) CW Compliance Margin RD,WR
        /// </summary>
        public const int CWComplianceMargin = 26;

        /// <summary>
        /// (0X1B) CCW Compliance Margin RD,WR
        /// </summary>
        public const int CCWComplianceMargin = 27;

        /// <summary>
        /// (0X1C) CW Compliance Slope RD,WR
        /// </summary>
        public const int CWComplianceSlope = 28;

        /// <summary>
        /// (0X1D) CCW Compliance Slope RD,WR
        /// </summary>
        public const int CCWComplianceSlope = 29;

        /// <summary>
        /// (0X1E) Goal Position(L) RD,WR [Addr36]value
        /// </summary>
        public const int GoalPositionL = 30;

        /// <summary>
        /// (0X1F) Goal Position(H) RD,WR [Addr37]value
        /// </summary>
        public const int GoalPositionH = 31;

        /// <summary>
        /// (0X20) Moving Speed(L) RD,WR 0
        /// </summary>
        public const int MovingSpeedL = 32;

        /// <summary>
        /// (0X21) Moving Speed(H) RD,WR 0
        /// </summary>
        public const int MovingSpeedH = 33;

        /// <summary>
        /// (0X22) Torque Limit(L) RD,WR [Addr14] value
        /// </summary>
        public const int TorqueLimitL = 34;

        /// <summary>
        /// (0X23) Torque Limit(H) RD,WR [Addr15] value
        /// </summary>
        public const int TorqueLimitH = 35;

        /// <summary>
        /// (0X24) Present Position(L) RD ?
        /// </summary>
        public const int PresentPositionL = 36;

        /// <summary>
        /// (0X25) Present Position(H) RD ?
        /// </summary>
        public const int PresentPositionH = 37;

        /// <summary>
        /// (0X26) Present Speed(L) RD ?
        /// </summary>
        public const int PresentSpeedL = 38;

        /// <summary>
        /// (0X27) Present Speed(H) RD ?
        /// </summary>
        public const int PresentSpeedH = 39;

        /// <summary>
        /// (0X28) Present Load(L) RD ?
        /// </summary>
        public const int PresentLoadL = 40;

        /// <summary>
        /// (0X29) Present Load(H) RD ?
        /// </summary>
        public const int PresentLoadH = 41;

        /// <summary>
        /// (0X2A) Present Voltage RD ?
        /// </summary>
        public const int PresentVoltage = 42;

        /// <summary>
        /// (0X2B) Present Temperature RD ?
        /// </summary>
        public const int PresentTemperature = 43;

        /// <summary>
        /// (0X2C) Registered Instruction RD,WR 0(0x00)
        /// </summary>
        public const int RegisteredInstruction = 44;

        //public const int Reserved = 45; //(0X2D) (Reserved) - 0(0x00) 
        
        /// <summary>
        /// (0x2E) Moving RD 0(0x00)
        /// </summary>
        public const int Moving = 46;

        /// <summary>
        /// (0x2F) Lock RD,WR 0(0x00)
        /// </summary>
        public const int Lock = 47;

        /// <summary>
        /// (0x30) Punch(L) RD,WR 32(0x20)
        /// </summary>
        public const int PunchL = 48;

        /// <summary>
        /// (0x31) Punch(H) RD,WR 0(0x00)
        /// </summary>
        public const int PunchH = 49;

        #endregion
    }
}
