using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.RobotArm
{
    public class Joints
    {
        public byte Base { get; private set; }
        public byte Shoulder { get; private set; }
        public byte Elbow { get; private set; }
        public byte Wrist { get; private set; }
        public byte Gripper { get; private set; }

        public Joints(byte baseID, byte shoulderID, byte elbowID, byte wristID, byte gripperID)
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
        public byte[] ToArray()
        {
            return new byte[] { this.Base, this.Shoulder, this.Elbow, this.Wrist, this.Gripper };
        }
    }
}
