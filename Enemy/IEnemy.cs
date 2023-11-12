using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    internal interface IEnemy
    {
        static Vector2 position;
        static bool alive;
        void Move();
        void Attack();
    }
}
