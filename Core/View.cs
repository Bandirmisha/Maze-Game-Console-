using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class View
    {
        public Field field;

        public View(Field field)
        {
            this.field = field;
        }

        public void Update()
        {
            Console.SetCursorPosition(0, 0);

            //Поле
            for (int j = 0; j < field.height; j++)
            {
                for (int i = 0; i < field.width; i++)
                {
                    //Стены
                    if (field.field[i, j] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(' ');
                    }
                    //Проход
                    else if (field.field[i, j] == 5)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                    }

                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\n");
            }


            //Зомби
            for (int i = 0; i < field.zombies.Count; i++)
            {
                if (field.zombies[i].alive)
                    DrawObject(field.zombies[i].position, 'Z', ConsoleColor.DarkRed);
            }

            //Скелеты
            for (int i = 0; i < field.skeletons.Count; i++)
            {
                if (field.skeletons[i].alive)
                    DrawObject(field.skeletons[i].position, 'S', ConsoleColor.Red);
            }

            //Стрелы
            for (int i = 0; i < field.skeletons.Count; i++)
            {
                for (int j = 0; j < field.skeletons[i].arrows.Count; j++)
                {
                    if (field.skeletons[i].arrows[j].position != new Vector2(-1, -1))
                        DrawObject(field.skeletons[i].arrows[j].position, '+', ConsoleColor.Black);
                } 
            }

            //Ключ
            if (!field.player.isKeyPicked)
            {
                DrawObject(field.key, 'K', ConsoleColor.Yellow);
            }

            //Выход
            DrawObject(field.exit, ' ', ConsoleColor.Cyan);

            //Игрок
            DrawObject(field.player.position, ' ', ConsoleColor.Green);



            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n");

            DrawStats();

        }

        public void DrawObject(Vector2 position, char symbol, ConsoleColor color)
        {
            Console.SetCursorPosition(Convert.ToInt32(position.X), Convert.ToInt32(position.Y));
            Console.BackgroundColor = color;
            Console.Write(symbol);
        }

        public void DrawStats()
        {
            Console.SetCursorPosition(0, field.height + 2);

            Console.WriteLine("Здоровье: {0}                    \n" +
                               "Атака: нажать E                 \n", field.player.Health.ToString());

            if (!field.player.isKeyPicked && field.player.Health != 0)
            {
                Console.WriteLine("Задание: найдите ключ             ");
            }
            else
            {
                if (!field.player.isFinished && field.player.Health != 0)
                {
                    Console.WriteLine("Задание: доберитесь до выхода          ");
                }
            }

            if (field.player.Health == 0)
                Console.WriteLine("Вы погибли!                    ");
        }

    }
}
