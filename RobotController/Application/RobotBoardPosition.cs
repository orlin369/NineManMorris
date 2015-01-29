using Robot.CorrdinateSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Application
{
    class RobotBoardPosition
    {
        private Joint[] Positions;

        /// <summary>
        /// Constructor
        /// </summary>
        public RobotBoardPosition(Joint[] positions)
        {
            this.Positions = positions;
        }
    }
}
