using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.RobotArm
{
    public class StateEvent : EventArgs
    {
        /// <summary>
        ///  State message.
        /// </summary>
        public string State
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="State">State message.</param>
        public StateEvent(string State)
        {
            this.State = State;
        }
    }
}
