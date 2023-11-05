using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Game
    {
        public Field field;
        public Player player;
        public Zombie zombie;
        public Skeleton skeleton;
        public Controller controller;
        public View view;
        private ConsoleKey lastKey;
        private readonly Timer gameUpdateTimer;
        private readonly ManualResetEvent gameEnded = new(false);

        public Game() 
        {
            field = new();
            player = new(field);
            zombie = new(field);
            skeleton = new(field);

            view = new(field,player,zombie,skeleton);
            controller = new(player);

            player.zombie = zombie;
            player.skeleton = skeleton;
            zombie.player = player;
            skeleton.player = player;
            
            gameUpdateTimer = new Timer(GameLoop, null, 0, 200);
            
            KeyInput();
            gameEnded.WaitOne();
        }

        private void GameLoop(object? state)
        {
            if (player.Health==0 || player.isFinished)
            {
                view.Update();
                
                gameUpdateTimer.Dispose();
                gameEnded.Set();
                return;
            }

            view.Update();
            controller.HandleInput(lastKey);
            lastKey = ConsoleKey.NoName;
        }

        private void KeyInput()
        {
            Task.Run(() =>
            {
                var keyInfo = Console.ReadKey(intercept: true);
                lastKey = keyInfo.Key;
                KeyInput();
            });
        }

    }
}
