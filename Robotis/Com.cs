using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robotis
{
    class Com
    {
        /// <summary>
        /// Sucsessfull transmition.
        /// </summary>
        public const int TXSUCCESS = 0;

        /// <summary>
        /// Sucsessfull reception.
        /// </summary>
        public const int RXSUCCESS = 1;

        /// <summary>
        /// Unsuxsefull transmition.
        /// </summary>
        public const int TXFAIL = 2;

        /// <summary>
        /// Unsucefull reception.
        /// </summary>
        public const int RXFAIL = 3;

        /// <summary>
        /// Transmition error.
        /// </summary>
        public const int TXERROR = 4;

        /// <summary>
        /// Transmition waiting.
        /// </summary>
        public const int RXWAITING = 5;

        /// <summary>
        /// Reception timeout.
        /// </summary>
        public const int RXTIMEOUT = 6;

        /// <summary>
        /// Corupted reception.
        /// </summary>
        public const int RXCORRUPT = 7;
    }
}
