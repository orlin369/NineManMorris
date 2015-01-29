using Robot.CorrdinateSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Kinematics
{
    interface IKinematics
    {
        Joint IverseKinematicTask(Decart position);

        Decart RightsKinematicTask(Joint Configuration);
    }
}
