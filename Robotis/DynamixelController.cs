using System;
using System.Collections.Generic;
using System.IO.Ports;
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

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_initialize(int port_num, int baud_rate);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_change_baudrate(int baud_rate);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_terminate();

        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_comm_result();
        /// <summary>
        /// Get the result after the request.
        /// </summary>
        /// <returns>State of result.</returns>
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_result();

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_tx_packet();
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_rx_packet();
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_txrx_packet();

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_set_txpacket_id(int id);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_set_txpacket_instruction(int instruction);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_set_txpacket_parameter(int index, int value);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_set_txpacket_length(int length);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_rxpacket_error(int error);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_rxpacket_error_byte();
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_rxpacket_parameter(int index);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_get_rxpacket_length();

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_ping(int id);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_read_byte(int id, int address);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_write_byte(int id, int address, int value);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl_read_word(int id, int address);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl_write_word(int id, int address, int value);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_tx_packet();
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_rx_packet();
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_txrx_packet();


        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_set_txpacket_id(byte id);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_set_txpacket_instruction(byte instruction);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_set_txpacket_parameter(UInt16 index, byte value);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_set_txpacket_length(UInt16 length);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl2_get_rxpacket_error_byte();
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl2_get_rxpacket_parameter(int index);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl2_get_rxpacket_length();


        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_ping(byte id);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern int dxl2_get_ping_result(byte id, int info_num);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_broadcast_ping();

        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_reboot(byte id);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_factory_reset(byte id, int option);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern byte dxl2_read_byte(byte id, int address);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_write_byte(byte id, int address, byte value);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern UInt16 dxl2_read_word(byte id, int address);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_write_word(byte id, int address, UInt16 value);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern UInt32 dxl2_read_dword(byte id, int address);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern void dxl2_write_dword(byte id, int address, UInt32 value);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern byte dxl2_get_bulk_read_data_byte(byte id, UInt32 start_address);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern UInt16 dxl2_get_bulk_read_data_word(byte id, UInt32 start_address);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern UInt32 dxl2_get_bulk_read_data_dword(byte id, UInt32 start_address);

        [DllImport(DYNAMIXEL_DLL)]
        public static extern byte dxl2_get_sync_read_data_byte(byte id, UInt32 start_address);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern UInt16 dxl2_get_sync_read_data_word(byte id, UInt32 start_address);
        [DllImport(DYNAMIXEL_DLL)]
        public static extern UInt32 dxl2_get_sync_read_data_dword(byte id, UInt32 start_address);


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

            string[] porNames = SerialPort.GetPortNames();
            List<int> indexes = new List<int>();
            foreach (string name in porNames)
            {
                string n = name.Replace("COM", "");
                indexes.Add(int.Parse(n));
            }

            if (!indexes.Contains(comIndex))
            {
                throw new ArgumentException("Invalid port index.");
            }

            int status = dxl_initialize(this.comIndex, this.boudrate);

            if (status == 0)
            {
                throw new InvalidOperationException("Failed to open USB2Dynamixel! Status: " + status.ToString());
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

            for (byte index = 0; index < BROADCAST_ID; index++)
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
        public List<Servo> FindServos(byte beginAddress = 0, byte endAddress = BROADCAST_ID)
        {
            List<Servo> listOfServo = new List<Servo>();

            for (byte index = beginAddress; index <= endAddress; index++)
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