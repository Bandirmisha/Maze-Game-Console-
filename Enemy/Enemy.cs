using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Enemy : IEnemy
    {
        public Vector2 position;
        public bool alive;
        public Player player;
        private Field field;
        private readonly List<Vector2> direction;
        private Random rand;
        
        public Enemy(Field f, Vector2 pos)
        {
            field = f;
            player = field.player;
            position = pos;
            rand = new Random();
            alive = true;

            direction = new List<Vector2>()
            {
                new Vector2(-1,0),
                new Vector2(0,-1),
                new Vector2(1,0),
                new Vector2(0,1)
            };

        }

        public void Event()
        {
            if (!alive)
            {
                position = new(-1,-1);
                return;
            }

            if (player != null)
            {
                if (player.position.X == position.X - 1 && player.position.Y == position.Y ||
                    player.position.X == position.X     && player.position.Y == position.Y - 1 ||
                    player.position.X == position.X + 1 && player.position.Y == position.Y ||
                    player.position.X == position.X     && player.position.Y == position.Y + 1)
                {
                    Attack();
                }
                else Move();
            }
        }

        public void Move()
        {
            Vector2 buf = position + direction[rand.Next(0, 4)];

            if (buf.X != field.width && buf.Y != field.height)
            {
                if (field.field[(int)buf.X, (int)buf.Y] == 5)
                {
                    position = buf;
                }
                //else { Move(); }
            }
        }

        public void Attack()
        {
            player.Health -= 10;
        }

        public Field _field
        { 
            get { return field; } 
        }
    }
}
