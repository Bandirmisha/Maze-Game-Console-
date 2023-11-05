using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Player
    {
        public int X, Y;
        public int Health;
        public Field Map;
        public bool isKeyPicked;
        public bool isFinished;
        public Zombie zombie;
        public Skeleton skeleton;
        public Player(Field field)
        {
            Health = 100;
            X = 1;
            Y = 1;
            Map = field;
            isKeyPicked = false;
            isFinished = false;
        }

        public void Move((int, int) direction)
        {
            int x = X + direction.Item1;
            int y = Y + direction.Item2;

            if (x == zombie.Y && y == zombie.X || x == skeleton.Y && y == skeleton.X)
            {
                return;
            }

            if (Map.field[y, x] == 5)
            {
                X += direction.Item1;
                Y += direction.Item2;
            }

            if (Map.key == (Y, X))
            {
                isKeyPicked = true;
            }

            if (Map.exit == (Y, X))
            {
                isFinished = true;
            }

        }

        public void Attack()
        {
            if (zombie.X == Y - 1 && zombie.Y == X ||
                zombie.X == Y && zombie.Y == X - 1 ||
                zombie.X == Y + 1 && zombie.Y == X ||
                zombie.X == Y && zombie.Y == X + 1)
            {
                zombie.alive = false;
            }
            else if (skeleton.X == Y - 1 && skeleton.Y == X ||
                skeleton.X == Y && skeleton.Y == X - 1 ||
                skeleton.X == Y + 1 && skeleton.Y == X ||
                skeleton.X == Y && skeleton.Y == X + 1)
            {
                skeleton.alive = false;
            }
        }
    }
}
