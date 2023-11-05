using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Controller
    {
        public Player player;

        public Controller(Player p)
        {
            player = p;
        }

        public void HandleInput(ConsoleKey key)
        {
            if (Direction(key) == (-1, 1))
                player.Attack();
            else
                player.Move(Direction(key));
        }

        private (int,int) Direction(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.W => (-1,0),
                ConsoleKey.A => (0,-1),
                ConsoleKey.S => (1, 0),
                ConsoleKey.D => (0, 1),
                ConsoleKey.E => (-1, 1),
                _ => (0,0)
            };
        }
    }
}
