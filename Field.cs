using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Field
    {
        public int[,] field;
        public int width;
        public int height;
        public Field() 
        {
            Generate();
            Update();
        }

        public void Generate()
        {
            var rand = new Random();

            width= rand.Next(80,101);
            height = rand.Next(20, 40);

            //width = 9;
            //height = 9;

            //Нужно, чтобы значения были нечетными
            if (width % 2 == 0) { width++; }
            if (height % 2 == 0) { height++; }

            //Инициализация поля лабиринта
            field = new int[width,height];
            
          
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


            //Генерация
            List<Cell> neigbours;
            List<Cell> visitedNeigbours = new();
            Cell currentCell = new(1, 1);
            Cell neighbourCell;
            Stack<Cell> backTrack = new();

            visitedNeigbours.Add(currentCell);
            backTrack.Push(currentCell);

            // Нахождение кол-ва всех возможных соседей
            int a = (width - 1) / 2;
            int b = (height - 1) / 2;
            int count = a * b;

            while (true)
            {
                //Нахождение соседей
                neigbours = getNeigbours(width, height, currentCell);

                //Удаление посещенных соседей
                for (int i = 0; i < neigbours.Count; i++)
                {
                    if (visitedNeigbours.Count > 0)
                    {
                        for (int j = 0; j < visitedNeigbours.Count; j++)
                        {
                            if (neigbours.Count == 0)
                                break;
                            if (neigbours[i].X == visitedNeigbours[j].X &&
                                    neigbours[i].Y == visitedNeigbours[j].Y)
                            {
                                neigbours.RemoveAt(i);

                                if (i != 0)
                                    i--;

                            }
                        }
                    }
                    if (neigbours.Count == 0)
                        break;
                }

                if (neigbours.Count > 0)
                {
                    //Соединение случайного соседа с текущей клеткой
                    neighbourCell = neigbours[rand.Next(0, neigbours.Count)];

                    if (neighbourCell.X > currentCell.X)
                        field[neighbourCell.X - 1, currentCell.Y] = 1;
                    else if (neighbourCell.X < currentCell.X)
                        field[currentCell.X - 1, currentCell.Y] = 1;
                    else if (neighbourCell.Y > currentCell.Y)
                        field[currentCell.X, neighbourCell.Y - 1] = 1;
                    else if (neighbourCell.Y < currentCell.Y)
                        field[currentCell.X, currentCell.Y - 1] = 1;

                    visitedNeigbours.Add(neighbourCell);

                    //Отслеживание пути следования
                    backTrack.Push(neighbourCell);

                    //Смещение "указателя"
                    currentCell = neighbourCell;

                    
                }
                else
                {
                    if(backTrack.Count > 0)
                    {
                        currentCell = backTrack.Pop();
                    }
                }

                //отслеживание currentCell в консоли
                field[currentCell.X, currentCell.Y] = 2;

                //Update();

                //отслеживание currentCell в консоли
                field[currentCell.X, currentCell.Y] = 1;


                if (visitedNeigbours.Count == count)
                    break;
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
            for (int i = 0; i<4 ;i++)
            {
                Cell bufCell = currCell;

                bufCell.X += direction[i, 0];
                bufCell.Y += direction[i, 1];

                if(bufCell.X < 0 || bufCell.X >= width ||bufCell.Y <0 || bufCell.Y >= height)
                { }
                else neigbours.Add(bufCell);
            }


            return neigbours;
        }
            
        struct Cell
        {
            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
        }
       

        public void Update()
        {
            Console.Clear();
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (field[i, j] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(' ');
                    }
                    else if (field[i, j] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                    }
                    else if (field[i, j] == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(' ');
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\n");
            }
        }



    }
}
