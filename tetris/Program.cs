using tetris;

Console.CursorVisible = false;

var game = new Game();

game.Start();

while ( ! game.GameOver)
{
    // listen to key presses
    if (Console.KeyAvailable)
    {
        var input = Console.ReadKey(true);

        switch (input.Key)
        {
            // send key presses to the game if it's not paused
            case ConsoleKey.UpArrow:
            case ConsoleKey.DownArrow:
            case ConsoleKey.LeftArrow:
            case ConsoleKey.RightArrow:
            case ConsoleKey.Spacebar:
                if (!game.Paused)
                    game.Input(input.Key);
                break;

            case ConsoleKey.P:
                if (game.Paused)
                    game.Resume();
                else
                    game.Pause();
                break;

            case ConsoleKey.Escape:
                game.Stop();
                return;
        }
    }
}
