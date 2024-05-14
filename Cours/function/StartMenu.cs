namespace Cours.function;

public class StartMenu
{
    public static int Menu()
    {
        Console.WriteLine("Bienvenue dans le démineur");
        Console.WriteLine("1 : jouer au démineur");
        Console.WriteLine("2 : charger la sauvegarde");
        Console.WriteLine("3 : Leaderboard");
        Console.WriteLine("");

        int choix = Convert.ToInt32(Console.ReadLine());

        if (choix == 1)
        {
            Console.WriteLine("Let's play !");
            PlayGame.Play();
        }
        else if (choix == 2)
        {
            Save.LoadSaveGame();
        }
        else if (choix == 3)
        {
            LeaderBoard.LeaderBoardShow();
        }
        else
        {
            Console.WriteLine("Choix invalide");
        }

        return choix;
    }
}