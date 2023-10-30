using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            width = 11;
            height = 11;

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
            Cell currentCell = new(1, 1);
            Cell neighbourCell;
            
            while(true)
            {
                List<Cell> neigbours = getNeigbours(width, height, currentCell);
                    
            }

        }

        private List<Cell> getNeigbours(int width, int height, Cell currCell)
        {
            List<Cell> neigbours = new List<Cell>();

            int[,] direction =
            {
                {-1,0},
                {0,1},
                {1,0},
                {0,-1}
            };

            //Проход по всем направлениям
            for (int i = 0; i<4 ;i++)
            {
                Cell bufCell = currCell;

                bufCell.X += direction[i, 0];
                bufCell.Y += direction[i, 1];

                if(bufCell.X < 0 || 
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
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (field[i, j] == 0)
                        Console.Write('#');
                    else if (field[i, j] == 1)
                        Console.Write(' ');
                    else if (field[i, j] == 2)
                        Console.Write('A');
                }
                Console.Write("\n");
            }
        }



    }
}
