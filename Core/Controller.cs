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
            if (Direction(key) == new Vector2(-1, 1))
                player.Attack();
            else
                player.Move(Direction(key));
        }

        private Vector2 Direction(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.W => new Vector2(0,-1),
                ConsoleKey.A => new Vector2(-1,0),
                ConsoleKey.S => new Vector2(0, 1),
                ConsoleKey.D => new Vector2(1, 0),
                ConsoleKey.E => new Vector2(-1, 1),
                _ => new Vector2(0,0)
            };
        }
    }
}
