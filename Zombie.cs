using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Zombie : Enemy
    {
        public Zombie(Field field) : base(field)
        {
            X = field.ZombieStartPos.Item1;
            Y = field.ZombieStartPos.Item2;
        }

    }
}
