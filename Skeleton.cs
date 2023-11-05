using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Skeleton : Enemy
    {
        public List<Arrow> arrows;
        private Random random;
        private List<(int, int)> possibleArrowDirection;
        private readonly Timer shootTimer;

        public Skeleton(Field field) : base(field)
        {
            X = field.SkeletonStartPos.Item1;
            Y = field.SkeletonStartPos.Item2;

            arrows = new List<Arrow>();

            random = new Random();
            possibleArrowDirection = new List<(int, int)>();
            shootTimer = new Timer(Shoot, null, 0, 2000);
        }

        public void Shoot(object? state)
        {
            if (!alive)
            {
                shootTimer.Dispose();
                return;
            }

            //Удаление уничтоженных стрел
            for (int i = 0; i < arrows.Count; i++)
            {
                if (arrows[i].X == -1 && arrows[i].Y == -1)
                {
                    arrows.RemoveAt(i);

                    if (i >= 0) { i--; }
                }
            }

            int[,] direction =
            {
                {-1,0},
                {0,1},
                {1,0},
                {0,-1}
            };

            for (int i = 0; i < 4; i++)
            {
                int x = X + direction[i, 0];
                int y = Y + direction[i, 1];

                if (x >= 0 && y >= 0)
                {
                    if (_field.field[x, y] == 5)
                    {
                        possibleArrowDirection.Add((direction[i, 0], direction[i, 1]));
                    }
                }
            }

            int ind = random.Next(0, possibleArrowDirection.Count);
            arrows.Add(new Arrow(X, Y, possibleArrowDirection[ind], _field, player));

            possibleArrowDirection.Clear();

        }
    }
}
