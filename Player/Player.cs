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
        public Vector2 position;
        public int Health;
        public Field field;
        public bool isKeyPicked;
        public bool isFinished;

        public Player(Field field)
        {
            position = new Vector2(1,1);
            Health = 100;
            this.field = field;
            isKeyPicked = false;
            isFinished = false;
        }

        public void Move(Vector2 direction)
        {
            Vector2 buf = position + direction;

            for(int i = 0; i<field.zombies.Count; i++)
            {
                if (buf.X == field.zombies[i].position.Y && buf.Y == field.zombies[i].position.X)
                {
                    return;
                }
            }

            for (int i = 0; i < field.skeletons.Count; i++)
            {
                if (buf.X == field.skeletons[i].position.Y && buf.Y == field.skeletons[i].position.X)
                {
                    return;
                }
            }

            if (field.field[(int)buf.X, (int)buf.Y] == 5)
            {
                position += direction;
            }

            if (field.key == position)
            {
                isKeyPicked = true;
            }

            if (field.exit  == position)
            {
                isFinished = true;
            }

        }

        public void Attack()
        {
            foreach(var zombie in field.zombies)
            {
                if (zombie.position.X == position.X - 1 && zombie.position.Y == position.Y ||
                    zombie.position.X == position.X     && zombie.position.Y == position.Y - 1 ||
                    zombie.position.X == position.X + 1 && zombie.position.Y == position.Y ||
                    zombie.position.X == position.X     && zombie.position.Y == position.Y + 1)
                {
                    zombie.alive = false;
                }
            }

            foreach (var skeleton in field.skeletons)
            {
                if (skeleton.position.X == position.X - 1 && skeleton.position.Y == position.Y ||
                    skeleton.position.X == position.X     && skeleton.position.Y == position.Y - 1 ||
                    skeleton.position.X == position.X + 1 && skeleton.position.Y == position.Y ||
                    skeleton.position.X == position.X     && skeleton.position.Y == position.Y + 1)
                {
                    skeleton.alive = false;
                }
            }

        }
    }
}
