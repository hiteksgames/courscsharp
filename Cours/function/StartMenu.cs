using System.IO;

namespace Cours.function;

public class StartMenu
{
    public static int Menu()
    {
        string instructions = File.ReadAllText("assets/instructions.txt");
        Console.WriteLine(instructions);
        int choix = Convert.ToInt32(Console.ReadLine());

        switch (choix)
        {
            case 1:
                Console.WriteLine("Let's play !");
                PlayGame.Play();
                break;
            case 2:
                Save.LoadSaveGame();
                break;
            case 3:
                LeaderBoard.LeaderBoardShow();
                break;
            default:
                Console.WriteLine("Choix invalide");
                break;
        }
        
        return choix;
    }
}