using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DuckHunt.Model
{
    class Chicken
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        private int VelocityX { get; set; }
        private int VelocityY { get; set; }

        public Ellipse Gfx { get; set; }

        public Chicken() : this(0, 0) { }

        public Chicken(int x, int y)
        {
            PosX = x;
            PosY = y;

            VelocityX = 10;
            VelocityY = 7;

            lock(ActionContainer.Instance)
            {
                ActionContainer.Instance.MovedObjects.Add(this);
            }
        }

        public void Move()
        {
            double maxX = 0;
            double maxY = 0;


            lock (ActionContainer.Instance)
            {
                maxX = ActionContainer.Instance.WindowWidth - 20;
                maxY = ActionContainer.Instance.WindowHeight - 20;
            }

            if ((PosX > maxX && VelocityX > 0) ||
                (PosX < 0 && VelocityX < 0))
            {
                VelocityX = -VelocityX;

                if (VelocityX > 0)
                {
                    PosX = 0;
                }
                else
                {
                    PosX = (int)maxX;
                }
            }

            if ((PosY > maxY && VelocityY > 0) ||
                (PosY < 0 && VelocityY < 0))
            {
                VelocityY = -VelocityY;

                if (VelocityY > 0)
                {
                    PosY = 0;
                }
                else
                {
                    PosY = (int)maxY;
                }
            }

            PosX += VelocityX;
            PosY += VelocityY;
        }
    }
}
