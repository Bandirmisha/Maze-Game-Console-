using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Field
    {
        public int[,] field;
        public int width;
        public int height;
        Random rand;
        Cell currentCell;
        Cell neighbourCell;
        Stack<Cell> backTrack;

        public Player player;
        public List<Zombie> zombies;
        public List<Skeleton> skeletons;
        public Vector2 key;
        public Vector2 exit;

        public Field() 
        {
            rand = new Random();
            width = rand.Next(25, 40);
            height = rand.Next(10, 15);
            if (width % 2 == 0) { width++; }
            if (height % 2 == 0) { height++; }

            field = new int[width, height];
            key = new Vector2(0, 0);
            exit = new Vector2(0, 0);

            currentCell = new(1, 1);
            backTrack = new();

            player = new(this);
            zombies = new List<Zombie>();
            skeletons = new List<Skeleton>();

            Generate();
            CreateEnemies();
        }

        private void Generate()
        {
            // Заполнение
            for (int j = 0; j < height; j++)
            {
                for(int i = 0; i < width; i++)
                {
                    //Создание границ
                    if (i == 0 || j == 0 || i == width - 1 || j == height - 1 || i % 2 == 0 || j % 2 == 0)
                    {
                        field[i, j] = 0;
                    }
                    else field[i, j] = 1;
                }
            }

            //Начальная точка посещена
            field[1, 1] = 5;
            backTrack.Push(currentCell);

            // Нахождение кол-ва всех непосещенных соседей
            int a = (width - 1) / 2;
            int b = (height - 1) / 2;
            int count = a * b - 1;

            //Генерация
            int deadEndCount = 0;
            int counter = 0;
            while (counter != count)
            {
                //Нахождение соседей
                List<Cell> neigbours = getNeigbours(width, height, currentCell);

                //Удаление посещенных соседей
                for (int i = 0; i < neigbours.Count; i++)
                {
                    int x = neigbours[i].X;
                    int y = neigbours[i].Y;

                    if (field[x, y] == 5)
                    {
                        neigbours.RemoveAt(i);

                        if(i>=0) { i--; }
                    }
                }

                if (neigbours.Count > 0)
                {
                    //Выбор случайного соседа
                    neighbourCell = neigbours[rand.Next(0, neigbours.Count)];

                    //Сосед отмечен посещенным
                    field[neighbourCell.X, neighbourCell.Y] = 5;

                    //Соединение
                    if (neighbourCell.X > currentCell.X)
                        field[neighbourCell.X - 1, currentCell.Y] = 5;
                    else if (neighbourCell.X < currentCell.X)
                        field[currentCell.X - 1, currentCell.Y] = 5;
                    else if (neighbourCell.Y > currentCell.Y)
                        field[currentCell.X, neighbourCell.Y - 1] = 5;
                    else if (neighbourCell.Y < currentCell.Y)
                        field[currentCell.X, currentCell.Y - 1] = 5;

                    //Отслеживание пути следования
                    backTrack.Push(neighbourCell);

                    //Смещение "указателя"
                    currentCell = neighbourCell;

                    counter++;


                    if (deadEndCount == 1 || deadEndCount == 3 || deadEndCount == 5)
                    {
                        deadEndCount++;
                    }
                   

                }
                else
                {
                    if (backTrack.Count > 0)
                    {
                        //Создание ключа и выхода
                        if (key.X == 0 && key.Y == 0 && deadEndCount == 0)
                        {
                            deadEndCount++;
                            key = new(currentCell.X, currentCell.Y);
                        }
                        else
                        {
                            if (exit.X == 0 && exit.Y == 0 && deadEndCount == 6)
                            {
                                exit = new(currentCell.X, currentCell.Y);
                            }
                        }

                        if (deadEndCount == 2 || deadEndCount == 4)
                        {
                            deadEndCount++;
                        }
                       
                        currentCell = backTrack.Pop();
                    }
                }
            }

            if (exit.X == 0 && exit.Y == 0)
            {
                exit = new(currentCell.X, currentCell.Y);
            }
        }

        private List<Cell> getNeigbours(int width, int height, Cell currCell)
        {
            List<Cell> neigbours = new List<Cell>();

            int[,] direction =
            {
                {-2,0},
                {0,2},
                {2,0},
                {0,-2}
            };

            //Проход по всем направлениям
            for (int i = 0; i < 4; i++)
            {
                Cell bufCell = currCell;

                bufCell.X += direction[i, 0];
                bufCell.Y += direction[i, 1];

                if (bufCell.X < 0 || bufCell.X >= width || bufCell.Y < 0 || bufCell.Y >= height)
                { }
                else neigbours.Add(bufCell);
            }

            return neigbours;
        }

        private struct Cell
        {
            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
        }

        private void CreateEnemies()
        {
            int zombieCount = rand.Next(2, 5); 
            int skeletonCount = rand.Next(1, 3);

            for (int i = 0; i < zombieCount; i++)
            {
                Vector2 startPos = GetEnemyStartPos();
                zombies.Add(new Zombie(this, startPos));
            }

            for (int i = 0; i < skeletonCount; i++)
            {
                Vector2 startPos = GetEnemyStartPos();
                skeletons.Add(new Skeleton(this, startPos));
            }
        }

        private Vector2 GetEnemyStartPos()
        {
            Vector2 vec = new Vector2(rand.Next(1, width), rand.Next(1, height));
            if (field[(int)vec.X, (int)vec.Y] == 0)
                GetEnemyStartPos();
            
            return vec;
        }

    }
}
