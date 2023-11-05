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
        Random rand;
        Cell currentCell;
        Cell neighbourCell;
        Stack<Cell> backTrack;
        public (int, int) ZombieStartPos;
        public (int, int) SkeletonStartPos;
        public (int, int) key;
        public (int, int) exit;

        public Field() 
        {
            rand = new Random();
            width = rand.Next(30, 80);
            height = rand.Next(10, 15);
            if (width % 2 == 0) { width++; }
            if (height % 2 == 0) { height++; }

            field = new int[width, height];

            currentCell = new(1, 1);
            backTrack = new();

            Generate();
        }

        public void Generate()
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
                        if (key == (0,0) && deadEndCount == 0)
                        {
                            deadEndCount++;
                            key = (currentCell.X, currentCell.Y); //ключик
                        }
                        else
                        {
                            if (exit == (0, 0) && deadEndCount == 6)
                            {
                                exit = (currentCell.X, currentCell.Y); //выход
                            }
                        }

                        if (deadEndCount == 2 || deadEndCount == 4)
                        {
                            deadEndCount++;
                        }
                       
                        currentCell = backTrack.Pop();
                    }
                }

                if (counter > count / 6 && ZombieStartPos == (0,0))
                    ZombieStartPos = (currentCell.X, currentCell.Y);
                
                if(counter > count / 3 && SkeletonStartPos == (0,0))
                    SkeletonStartPos = (currentCell.X, currentCell.Y);
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
       
        

    }
}
