using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard
{
    public partial class Move : UserControl
    {
        public Move()
        {
            InitializeComponent();
            moveFunction = MoveFunctionList.through;
            this.BackgroundImage = moveFunctionImageList.Images[(int)moveFunction];
        }

        public enum MoveFunctionList:int
        {
            through = 0,
            forward = 1,
            back = 2,
            cw = 3,
            ccw = 4,
            stop = 5
        }

        private MoveFunctionList moveFunction;

        public MoveFunctionList MoveFunction
        {
            get
            {
                return moveFunction;
            }
            set
            {
                moveFunction = value;
                this.BackgroundImage = moveFunctionImageList.Images[(int)moveFunction];
            }
        }

        private void changeMoveFunctionToThrough_Click(object sender, EventArgs e)
        {
            MoveFunction = MoveFunctionList.through;
        }

        private void changeMoveFunctionToForward_Click(object sender, EventArgs e)
        {
            MoveFunction = MoveFunctionList.forward;
        }

        private void changeMoveFunctionToBack_Click(object sender, EventArgs e)
        {
            MoveFunction = MoveFunctionList.back;
        }

        private void changeMoveFunctionToCW_Click(object sender, EventArgs e)
        {
            MoveFunction = MoveFunctionList.cw;
        }

        private void changeMoveFunctionToCCW_Click(object sender, EventArgs e)
        {
            MoveFunction = MoveFunctionList.ccw;
        }

        private void changeMoveFunctionToStop_Click(object sender, EventArgs e)
        {
            MoveFunction = MoveFunctionList.stop;
        }

        private void Move_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                changeMoveFunctionMenu.Show(this, new Point(e.X, e.Y));
            }
        }
    }
}