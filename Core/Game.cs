using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class Game
    {
        public Field field;
        public Controller controller;
        public View view;
        private ConsoleKey lastKey;
        private Timer? gameUpdateTimer;
        private readonly ManualResetEvent gameEnded = new(false);
        private int frameCount = 0;

        public Game()
        {
            field = new();
            view = new(field);
            controller = new(field.player);
        }

        public void Run()
        {
            gameUpdateTimer = new Timer(GameLoop, null, 0, 200);
            Task.Run(() => KeyInput());
            gameEnded.WaitOne();
        }

        private void NewGame()
        {
            field = new();
            view = new(field);
            controller = new(field.player);

            Run();
        }

        private void GameLoop(object? state)
        {
            if (field.player.Health<=0)
            {
                view.Update();
                
                gameUpdateTimer?.Dispose();
                gameEnded.Set();
                return;
            }

            if (field.player.isFinished)
            {
                gameUpdateTimer?.Dispose();
                NewGame();
                return;
            }

            //Каждые 2 кадра стрела сдвигается
            if (frameCount % 2 == 0)
            {
                for (int i = 0; i < field.skeletons.Count; i++)
                {
                    for (int j = 0; j < field.skeletons[i].arrows.Count; j++)
                    {
                        field.skeletons[i].arrows[j].Move();     
                    }
                }
            }

            //Каждые 5 кадров враги двигаются/атакуют
            if (frameCount % 5 == 0)
            {
                foreach(var zombie in field.zombies)
                {
                    zombie.Event();
                }
                foreach (var skeleton in field.skeletons)
                {
                    skeleton.Event();
                }
            }

            //Каждые 10 кадров скелеты стреляют
            if (frameCount % 10 == 0)
            {
                foreach (var skeleton in field.skeletons)
                {
                    skeleton.Shoot();
                }
            }


            frameCount++;
            view.Update();
            controller.HandleInput(lastKey);
            lastKey = ConsoleKey.NoName;
        }

        private async Task KeyInput()
        {
            while (!field.player.isFinished)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = await Task.Run(() => Console.ReadKey(intercept: true));
                    lastKey = keyInfo.Key;
                }
                await Task.Delay(10);
            }
        }

    }
}
