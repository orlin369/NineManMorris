using Robot.CorrdinateSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.RobotArm
{
    public interface IRobotArm
    {
        void SetJPosition(Joint configuration);

        void SetDPosition(Decart configuration);

        Joint GetJPosition();

        Decart GetDPosition();

        void SetGripper(double position);

        double GetGripper();
    }
}
