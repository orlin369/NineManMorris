using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robotis
{
    /// <summary>
    /// Alarm shutdown bits.
    /// </summary>
    public class Allarms
    {
        public const int ERRBIT_VOLTAGE     = 1;
        public const int ERRBIT_ANGLE       = 2;
        public const int ERRBIT_OVERHEAT    = 4;
        public const int ERRBIT_RANGE       = 8;
        public const int ERRBIT_CHECKSUM    = 16;
        public const int ERRBIT_OVERLOAD    = 32;
        public const int ERRBIT_INSTRUCTION = 64;
    }
}
