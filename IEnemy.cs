using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    internal interface IEnemy
    {
        static int X;
        static int Y;
        static bool alive;
        void Move();
        void Attack();
    }
}
