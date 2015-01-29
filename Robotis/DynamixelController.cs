using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Robotis
{
    /// <summary>
    /// Dynamixel comunication wrapper.
    /// </summary>
    public class DynamixelController : IDisposable
    {
        #region Constants

        private const int MAXNUM_TXPARAM = 150;
        private const int MAXNUM_RXPARAM = 60;

        private const int BROADCAST_ID = 254;

        private const int INST_PING = 1;
        private const int INST_READ = 2;
        private const int INST_WRITE = 3;
        private const int INST_REG_WRITE = 4;
        private const int INST_ACTION = 5;
        private const int INST_RESET = 6;
        private const int INST_SYNC_WRITE = 131;

        /// <summary>
        /// Dynamixel DLL name.
        /// </summary>
        private const string DYNAMIXEL_DLL = "dynamixel.dll";

        #endregion

        #region Variables

        /// <summary>
        /// Baud rate index.
        /// </summary>
        private int boudrate = 0;

        /// <summary>
        /// Serial port number.
        /// </summary>
        private int comIndex = 1;

        #endregion

        #region DLL Imports

        #region Init / Terminate

        /// <summary>
        /// Initialise the comunication.
        /// </summary>
        /// <param name="comIndex">Serial port name.</param>
        /// <param name="baudnum">Serial boudrate.</param>
        /// <returns>Serial port state.</returns>
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_initialize(int comIndex, int baudnum);

        /// <summary>
        /// Terminate the comunication
        /// </summary>
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_terminate();

        #endregion

        /// <summary>
        /// Get the result after the request.
        /// </summary>
        /// <returns>State of result.</returns>
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_result();

        #region Package

        /// <summary>
        /// Transmit the request.
        /// </summary>
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_tx_packet();

        /// <summary>
        /// Recept the responce.
        /// </summary>
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_rx_packet();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_txrx_packet();

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_set_txpacket_id(int id);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_set_txpacket_instruction(int instruction);


        #region Package lenght

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_set_txpacket_length(int length);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_rxpacket_length();

        #endregion

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_rxpacket_error(int errbit);

        #region Package parameter

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_rxpacket_parameter(int index);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_set_txpacket_parameter(int index, int value);

        #endregion

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_makeword(int lowbyte, int highbyte);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_lowbyte(int word);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_highbyte(int word);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_ping(int id);

        #region RW Byte

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_read_byte(int id, int address);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_write_byte(int id, int address, int value);

        #endregion

        #region RW Word

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_read_word(int id, int address);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_write_word(int id, int address, int value);

        #endregion

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="comIndex">Index of the serial port.</param>
        /// <param name="boudrate">Boudrate index.</param>
        public DynamixelController(int comIndex, int boudrate)
        {
            this.comIndex = comIndex;
            this.boudrate = boudrate;

            int status = dxl_initialize(this.comIndex, this.boudrate);

            if (status == 0)
            {
                throw new InvalidOperationException("Failed to open USB2Dynamixel!");
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~DynamixelController()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // Close device
            dxl_terminate();
        }
        
        #endregion

        #region Internal

        /// <summary>
        /// Read word.
        /// </summary>
        /// <param name="id">ID of the servo.</param>
        /// <param name="opCode">Operation code.</param>
        internal int ReadWord(int id, int opCode)
        {
            return DynamixelController.dxl_read_word(id, opCode);
        }

        /// <summary>
        /// Write word.
        /// </summary>
        /// <param name="id">ID of the servo.</param>
        /// <param name="opCode">Operation code.</param>
        /// <param name="data">Argument date.</param>
        internal void WriteWord(int id, int opCode, int data)
        {
            DynamixelController.dxl_write_word(id, opCode, data);
        }

        /// <summary>
        /// Read byte.
        /// </summary>
        /// <param name="id">ID of the servo.</param>
        /// <param name="opCode">Operation code.</param>
        internal int ReadByte(int id, int opCode)
        {
            return DynamixelController.dxl_read_byte(id, opCode);
        }

        /// <summary>
        /// Write byte.
        /// </summary>
        /// <param name="id">ID of the servo.</param>
        /// <param name="opCode">Operation code.</param>
        /// <param name="data">Argument date.</param>
        internal void WriteByte(int id, int opCode, int data)
        {
            DynamixelController.dxl_write_byte(id, opCode, data);
        }

        /// <summary>
        /// Ping servo.
        /// </summary>
        /// <param name="id">ID of the servo.</param>
        /// <returns>True if the servo is alive.</returns>
        internal bool Ping(int id)
        {
            dxl_ping(id);
            int result = dxl_get_result();

            if (result == INST_PING)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Public

        /// <summary>
        /// Find all servos in the network.
        /// </summary>
        /// <returns>Dictionary of servos in the network.</returns>
        public List<Servo> FindServos()
        {
            List<Servo> listOfServo = new List<Servo>();

            for (int index = 0; index < BROADCAST_ID; index++)
            {
                if (this.Ping(index))
                {
                    listOfServo.Add(new Servo(this, index));
                }
            }

            return listOfServo;
        }

        /// <summary>
        /// Find all servos in the network.
        /// </summary>
        /// <returns>Dictionary of servos in the network.</returns>
        public List<Servo> FindServos(int beginAddress = 0, int endAddress = BROADCAST_ID)
        {
            List<Servo> listOfServo = new List<Servo>();

            for (int index = beginAddress; index <= endAddress; index++)
            {
                if (this.Ping(index))
                {
                    listOfServo.Add(new Servo(this, index));
                }
            }

            return listOfServo;
        }

        #endregion
    }    
}