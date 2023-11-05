using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Enemy : IEnemy
    {
        public int X;
        public int Y;
        public bool alive;
        public Player player;
        private Field field;
        private readonly Timer EventTimer;
        //private readonly Timer attackEventTimer;
        private readonly List<(int, int)> direction;
        private Random rand;
        
        public Enemy(Field f)
        {
            field = f;
            rand = new Random();
            alive = true;

            direction = new List<(int, int)>()
            {
                (-1,0),
                (0,1),
                (1,0),
                (0,-1)
            };

            EventTimer = new Timer(Event, null, 0, 1000);
            //attackEventTimer = new Timer(AttackEvent, null, 0, 2000);
        }

        public void Event(object? state)
        {
            if (!alive)
            {
                X = 0;
                Y = 0;
                EventTimer.Dispose();
                return;
            }

            if (player != null)
            {
                if (player.X == Y - 1 && player.Y == X ||
                    player.X == Y && player.Y == X - 1 ||
                    player.X == Y + 1 && player.Y == X ||
                    player.X == Y && player.Y == X + 1)
                {
                    Attack();
                }
                else Move();
            }
        }

        public void Move()
        {
            int index = rand.Next(0, 4);

            int x = X + direction[index].Item1;
            int y = Y + direction[index].Item2;

            if (field.field[x, y] == 5)
            {
                X = x;
                Y = y;
            }
            else { Move(); }
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
