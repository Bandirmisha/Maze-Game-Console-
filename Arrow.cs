using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Arrow
    {
        public int X, Y;
        public (int, int) direction;
        public Player player;
        private readonly Timer MoveTimer;
        private readonly Field field;

        public Arrow(int x, int y, (int, int) dir, Field f, Player pl)
        {
            direction = dir;
            X = x;
            Y = y;

            field = f;
            player = pl;

            MoveTimer = new Timer(Move, null, 0, 300);
        }

        public void Move(object? state)
        {
            int x = X + direction.Item1;
            int y = Y + direction.Item2;
            
            
            if (x >= 0 && y >= 0)
            {
                if (player.X == y && player.Y == x)
                {
                    player.Health -= 20;
                    X = -1;
                    Y = -1;
                }
                else if (field.field[x, y] == 5)
                {
                    X = x;
                    Y = y;
                }
                else
                {
                    X = -1;
                    Y = -1;
                }
            }
        }
    }
}
