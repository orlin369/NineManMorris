using Robot.RobotArm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Robotis;
using Robot.CorrdinateSystems;

namespace Robot.Application
{
    public class NineMansMorrisPlayer : DynamixelArm
    {
        private FigureShop MyFigureShop;

        private FigureShop CaptureShop;

        private RobotBoardPosition BoardFigurePositions;
        /// <summary>
        /// Is the gripper is busy.
        /// </summary>
        public bool IsFigureInGripper
        {
            get;
            private set;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RobotPortNumber"></param>
        /// <param name="Boudrate"></param>
        /// <param name="JointID"></param>
        public NineMansMorrisPlayer(DynamixelController controler, Joints jointIDs)
            : base(controler, jointIDs)
        {
            //this.MyFigureShop = new FigureShop();
            //this.CaptureShop = new FigureShop();
            //this.BoardFigurePositions = new RobotBoardPosition();
        }

        /// <summary>
        /// 
        /// </summary>
        public void GetFigureFromShop()
        {
            Joint figurePoint = MyFigureShop.GetNextFigure();
            // TODO: motion for geting the figure based on this position.
            this.SetJPosition(figurePoint);
            this.IsFigureInGripper = true;
        }

        public void DropFigureOnBoard(int BoardIndexPosition)
        {
            if (this.IsFigureInGripper)
            {
                //JointConfiguration figurePosition = 
                //this.SetJointConfiguration()
                // TODO: Drop the figure on the board, based on end position index. (motion)
                Console.WriteLine("Drop the figure on the board, based on end position index.");
                this.IsFigureInGripper = false;
            }
        }

        public void GetFigureFromBoard(int BoardIndexPosition)
        {
            if (!this.IsFigureInGripper)
            {
                this.IsFigureInGripper = true;
                Console.WriteLine("Get the figure from the board.");
                // TODO: Get the figure from the board. (motion)
            }
        }

        public void DropFigureToTheCaptureShop()
        {
            if (this.IsFigureInGripper)
            {
                // TODO: Drop the figure to the shop. (motion)
                Console.WriteLine("Drop the figure to the shop.");
                this.IsFigureInGripper = false;
            }
        }
    }
}
