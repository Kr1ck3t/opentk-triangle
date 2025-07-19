using System;


namespace OpenTK_yttutorial;

class Program
{
    static void Main(string[] args)
    {
        try
    {
        using (Game game = new Game(800, 600))
        {
            game.Run();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unhandled exception: {ex.Message}");
    }
    }
}
