using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.RobotArm
{
    public class Joints
    {
        public int Base { get; private set; }
        public int Shoulder { get; private set; }
        public int Elbow { get; private set; }
        public int Wrist { get; private set; }
        public int Gripper { get; private set; }

        public Joints(int baseID, int shoulderID, int elbowID, int wristID, int gripperID)
        {
            this.Base = baseID;
            this.Shoulder = shoulderID;
            this.Elbow = elbowID;
            this.Wrist = wristID;
            this.Gripper = gripperID;
        }

        /// <summary>
        /// Convert the ID to array.
        /// </summary>
        /// <returns></returns>
        public int[] ToArray()
        {
            return new int[] { this.Base, this.Shoulder, this.Elbow, this.Wrist, this.Gripper };
        }
    }
}
