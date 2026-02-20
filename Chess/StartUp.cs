namespace Chess
{
    using System;

    using Models;
    using Models.Enums;
    using Models.EventArgs;
    using View;

    public class StartUp
    {
        public static void Main()
        {
            var is960 = false;
            Print printer = Factory.GetPrint();
            Draw drawer = Factory.GetDraw();
            printer.Ask960();
            var option2 = Console.ReadKey().Key;
                switch (option2)
                {
                    case ConsoleKey.Y:
                        is960 = true;
                        break;
                    case ConsoleKey.N:
                        is960 = false;
                        break;
                    default:
                        Console.Clear();
                        printer.ErrorWindow();
                        return;
            }
            try
            {
                Console.Clear();
                printer.Header();
                drawer.BoardEmpty(Color.Light);
                
                while (true)
                {
                    printer.Menu();

                    var option = Console.ReadKey().Key;
                    switch (option)
                    {
                        case ConsoleKey.N:
                            {
                                Game game = Factory.GetGame();
                                if (is960)
                                {
                                    game.Enable960();
                                }
                                game.GetPlayers();
                                game.New();
                                game.OnGameOver += Game_OnGameOver;
                                game.Start();
                                game.End();
                            }

                            break;
                        case ConsoleKey.L:
                            break;
                        case ConsoleKey.S:
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            return;
                    }
                }
            }
            catch (Exception)
            {
                Paint.DefaultBackground();
                Console.Clear();
                printer.ErrorWindow();
            }
            
            void Game_OnGameOver(object sender, EventArgs e)
            {
                var player = sender as Player;
                var gameOver = e as GameOverEventArgs;

                printer.FinalMessage(player, gameOver.GameOver);
            }
        }
    }
}
