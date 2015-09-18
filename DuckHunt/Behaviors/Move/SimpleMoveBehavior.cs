using DuckHunt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckHunt.Behaviors.Move
{
    class SimpleMoveBehavior : IMoveBehavior
    {
        private int _posX;
        public int PosX
        {
            get
            {
                return _posX;
            }
            set
            {
                _posX = value;
            }
        }

        private int _posY;
        public int PosY
        {
            get
            {
                return _posY;
            }

            set
            {
                _posY = value;
            }
        }

        private int _vX;
        public int VX
        {
            get
            {
                return _vX;
            }
            set
            {
                _vX = value;
            }
        }

        private int _vY;

        public int VY
        {
            get
            {
                return _vY;
            }
            set
            {
                _vY = value;
            }
        }

        public SimpleMoveBehavior()
        {
            VX = 5;
            VY = 10;

            PosX = 0;
            PosY = 0;
        }

        public void Move()
        {
            double maxX = 0;
            double maxY = 0;

            lock (Locks.ActionContainer)
            {
                maxX = ActionContainer.Instance.WindowWidth - 20;
                maxY = ActionContainer.Instance.WindowHeight - 20;
            }

            if ((PosX > maxX && VX > 0) ||
                (PosX < 0 && VX < 0))
            {
                VX = -VX;

                if (VX > 0)
                {
                    PosX = 0;
                }
                else
                {
                    PosX = (int)maxX;
                }
            }

            if ((PosY > maxY && VY > 0) ||
                (PosY < 0 && VY < 0))
            {
                VY = -VY;

                if (VY > 0)
                {
                    PosY = 0;
                }
                else
                {
                    PosY = (int)maxY;
                }
            }

            PosX += VX;
            PosY += VY;
        }
    }
}
