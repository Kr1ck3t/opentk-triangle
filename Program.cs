namespace OpenTK_yttutorial;

class Program
{
    static void Main(string[] args)
    {
        using (Game newGame = new Game(500, 500))
        {
            newGame.Run();
        }
    }
}
