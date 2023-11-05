using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class View
    {
        public Field map;
        public Player player;
        public Zombie zombie;
        public Skeleton skeleton;

        public View(Field field, Player p, Zombie z, Skeleton s)
        {
            map = field;
            player = p;
            zombie = z;
            skeleton = s;
        }

        public void Update()
        {
            Console.SetCursorPosition(0, 0);

            for (int j = 0; j < map.height; j++)
            {
                for (int i = 0; i < map.width; i++)
                {
                    //Игрок
                    if (i == player.Y && j == player.X)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(' ');
                        continue;
                    }

                    //Зомби
                    if (i == zombie.X && j == zombie.Y && zombie.alive)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write('Z');
                        continue;
                    }

                    //Скелет
                    if (i == skeleton.X && j == skeleton.Y && skeleton.alive)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write('S');
                        continue;
                    }

                    //Стрелы
                    bool arrowDrawed = false;
                    for(int k = 0; k < skeleton.arrows.Count; k++)
                    {
                        if (i == skeleton.arrows[k].X && j == skeleton.arrows[k].Y)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write('+');
                            arrowDrawed = true;
                            break;
                        }
                    }
                    if (arrowDrawed) continue;

                    //Стены
                    if (map.field[i, j] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(' ');
                    }
                    //Ключ
                    else if (map.key.Item1 == i && map.key.Item2 == j && !player.isKeyPicked)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write('K');
                    }
                    //Выход
                    else if (map.exit.Item1 == i && map.exit.Item2 == j)
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write(' ');
                    }
                    //Проход
                    else if (map.field[i, j] == 5)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                    }

                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("\n");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n");



            Console.WriteLine("Здоровье: {0}                    \n" +
                               "Атака: нажать E                 \n", player.Health.ToString());

            if (!player.isKeyPicked && player.Health!=0) 
            { 
                Console.WriteLine("Задание: найдите ключ             "); 
            }
            else
            { 
                if(!player.isFinished && player.Health != 0) 
                { 
                    Console.WriteLine("Задание: доберитесь до выхода          ");
                }
                else if(player.isFinished && player.Health != 0)
                {
                    Console.WriteLine("Вы выбрались!                       ");
                }
            }

            if(player.Health == 0)
                Console.WriteLine("Вы погибли!                    ");

        }

    }
}
