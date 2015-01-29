using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

// Robotis Dynamixel.
using Robotis;

// Loging system
using Loging;
using System.Threading;
using Robot.CorrdinateSystems;
using Robot.Kinematics;

namespace Robot.RobotArm
{
    public class DynamixelArm : IRobotArm, IDisposable
    {
        #region Constants

        /// <summary>
        /// Scale from servo position fo degree.
        /// </summary>
        private const double SCALE = 3.78d;

        /// <summary>
        /// The full servo size is 1024 so the half offset will be 510 after scaling.
        /// </summary>
        private const double OFFSET = 510.5d;

        #endregion

        #region Variables

        /// <summary>
        /// Robot controller.
        /// </summary>
        private DynamixelController controller;

        /// <summary>
        /// 
        /// </summary>
        private Servo baseServo;
        
        /// <summary>
        /// 
        /// </summary>
        private Servo shoulderServo;
        
        /// <summary>
        /// 
        /// </summary>
        private Servo elbowServo;
        
        /// <summary>
        /// 
        /// </summary>
        private Servo wristServo;
        
        /// <summary>
        /// 
        /// </summary>
        private Servo gripperServo;

        /// <summary>
        /// Joint IDs.
        /// </summary>
        private Joints jointIDs;

        #endregion

        #region Property

        /// <summary>
        /// If return true then the robot is on position.
        /// </summary>
        public bool isOnPosition
        {
            get
            {
                if (this.baseServo.PresentPosition == this.baseServo.GoalPosition)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<StateEvent> ComState;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ErrorEvent> ServoState;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> OnPosition;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DynamixelArm(DynamixelController controller, Joints jointIDs)
        {
            this.jointIDs = jointIDs;
            this.controller = controller;

            List<Servo> servos = this.controller.FindServos(jointIDs.ToArray().Min(), jointIDs.ToArray().Max());

            this.baseServo = servos.Where(x => x.ID == this.jointIDs.Base).First();
            this.shoulderServo = servos.Where(x => x.ID == this.jointIDs.Shoulder).First();
            this.elbowServo = servos.Where(x => x.ID == this.jointIDs.Elbow).First();
            this.wristServo = servos.Where(x => x.ID == this.jointIDs.Wrist).First();
            this.gripperServo = servos.Where(x => x.ID == this.jointIDs.Gripper).First();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~DynamixelArm()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose, call by the destructor.
        /// </summary>
        public void Dispose()
        {

        }

        #endregion

        #region Private

        /// <summary>
        /// Convert angles to servo units.
        /// </summary>
        /// <param name="Angle">Angle</param>
        /// <returns>Servo units.</returns>
        private int ToServoPosition(double angle)
        {
            return Convert.ToInt32((angle * SCALE) + OFFSET);
        }

        /// <summary>
        /// Convert servo position in angle.
        /// </summary>
        /// <param name="Position">Servo units.</param>
        /// <returns>Angle position</returns>
        private double FromServoPosition(int position)
        {
            return Convert.ToDouble((position - OFFSET) / SCALE);
        }

        #endregion

        #region Public

        /// <summary>
        /// Set position of the robot by decart coordinates.
        /// </summary>
        /// <param name="Position"></param>
        public void SetDPosition(Decart Position) 
        {
            // Calculate
            Joint configuration = KinematicsStatic.IverseKinematicTask(Position);

            this.SetJPosition(configuration);
        }

        /// <summary>
        /// Set position of the robot by joint coordinates.
        /// </summary>
        /// <param name="configuration"></param>
        public void SetJPosition(Joint configuration)
        {
            this.baseServo.GoalPosition = this.ToServoPosition(configuration.Base);
            this.shoulderServo.GoalPosition = this.ToServoPosition(configuration.Shoulder);
            this.elbowServo.GoalPosition = this.ToServoPosition(configuration.Elbow);
            this.wristServo.GoalPosition = this.ToServoPosition(configuration.Wrist);
            this.gripperServo.GoalPosition = this.ToServoPosition(configuration.Gripper);
        }

        /// <summary>
        /// Get position of the robot by decart coordinates
        /// </summary>
        /// <returns>Decart coordinate.</returns>
        public Decart GetDPosition()
        {
            // Current robot pos.
            Joint curJointPos = this.GetJPosition();

            // Current robot in XYZPR coordinates.
            Decart curDecPos = KinematicsStatic.RightsKinematicTask(curJointPos);

            return curDecPos;
        }
        
        /// <summary>
        /// Get position of the robot by joint coordinates
        /// </summary>
        /// <returns>Joint coordinates.</returns>
        public Joint GetJPosition()
        {
            double baseAngle = this.FromServoPosition(this.baseServo.GoalPosition);
            double shoulderAngle = this.FromServoPosition(this.shoulderServo.GoalPosition);
            double elbowAngle = this.FromServoPosition(this.elbowServo.GoalPosition);
            double wristAngle = this.FromServoPosition(this.wristServo.GoalPosition);
            double gripperAngle = this.FromServoPosition(this.gripperServo.GoalPosition);

            // Current robot pos.
            return new Joint(baseAngle, shoulderAngle, elbowAngle, wristAngle, gripperAngle);
        }

        /// <summary>
        /// Move the robot hand to the [0:0:0] position.
        /// </summary>
        public void GoToHome()
        {
            this.SetJPosition(new Joint(0.0d, 0.0d, 0.0d, 0.0d, 0.0d));
        }

        /// <summary>
        /// Set the gripper position.
        /// </summary>
        /// <param name="position">Value</param>
        public void SetGripper(double position)
        {
            this.gripperServo.GoalPosition = this.ToServoPosition(position);
        }

        /// <summary>
        /// Get the gripper position.
        /// </summary>
        /// <returns>Value</returns>
        public double GetGripper()
        {
            return FromServoPosition(this.gripperServo.GoalPosition);
        }

        /// <summary>
        /// Linear joiint interpolation.
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="step">Step of the integration.</param>
        /// <param name="delay">Delay between steps.</param>
        public void LJInterpolation(Joint position, double step, int delay = 1)
        {
            // Current robot pos.
            Joint currentPosition = this.GetJPosition();
            
            // Delta steps for each joint.
            double deltaBase = position.Base / step;
            double deltaShoulder = position.Shoulder / step;
            double deltaElbow = position.Elbow / step;
            double deltaWrist = position.Wrist / step;
            
            // Current positions.
            double curBase = currentPosition.Base;
            double curShouler = currentPosition.Shoulder;
            double curElbow = currentPosition.Elbow;
            double curWrist = currentPosition.Wrist;

            for (double i = 0; i < step; i++)
            {
                curBase += deltaBase;
                curShouler -= deltaShoulder;
                curElbow += deltaElbow;
                curWrist += deltaWrist;

                Joint newPos = new Joint(curBase, curShouler, curElbow, curWrist, currentPosition.Gripper);

                this.SetJPosition(newPos);
                Console.WriteLine("{0}", newPos.ToString());
                System.Threading.Thread.Sleep(delay);
            }
        }

        #endregion
    }
}
