using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Skeleton : Enemy
    {
        public List<Arrow> arrows;
        private Random random;
        private List<Vector2> possibleArrowDirection;
        private List<Vector2> direction;

        public Skeleton(Field field, Vector2 startPos) : base(field, startPos)
        {
            arrows = new List<Arrow>();

            random = new Random();
            possibleArrowDirection = new List<Vector2>();
            direction = new List<Vector2>()
            {
                new Vector2(-1,0),
                new Vector2(0,-1),
                new Vector2(1,0),
                new Vector2(0,1)
            };

        }

        public void Shoot()
        {
            if (!alive)
            {
                return;
            }

            //Удаление уничтоженных стрел
            for (int i = 0; i < arrows.Count; i++)
            {
                if (arrows[i].position.X == -1 && arrows[i].position.Y == -1)
                {
                    arrows.RemoveAt(i);

                    if (i >= 0) { i--; }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Vector2 buf = position + direction[i];

                if (buf.X >= 0 && buf.Y >= 0 && buf.X < _field.width && buf.Y < _field.height)
                {
                    if (_field.field[(int)buf.X, (int)buf.Y] == 5)
                    {
                        possibleArrowDirection.Add(direction[i]);
                    }
                }
            }

            int ind = random.Next(0, possibleArrowDirection.Count);
            arrows.Add(new Arrow(position, possibleArrowDirection[ind], _field, player));

            possibleArrowDirection.Clear();

        }
    }
}
