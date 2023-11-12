using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Arrow
    {
        public Vector2 position;
        public Vector2 direction;
        public Player player;
        private readonly Field field;

        public Arrow(Vector2 pos, Vector2 dir, Field f, Player pl)
        {
            direction = dir;
            position = pos;

            field = f;
            player = pl;
        }

        public void Move()
        {
            Vector2 buf = position + direction;
    
            if (buf.X >= 0 && buf.Y >= 0)
            {
                if (player.position == buf)
                {
                    player.Health -= 20;
                    position = new(-1,-1);
                }
                else if (field.field[(int)buf.X, (int)buf.Y] == 5)
                {
                    position = buf;
                }
                else
                {
                    position = new(-1, -1);
                }
            }
        }
    }
}
