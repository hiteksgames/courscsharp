namespace Cours.function;

public static class PlayGame
{
    public static void Play()
    {
        Console.Clear();
        string tailleGrille = File.ReadAllText("assets/tailleGrille.txt");
        Console.WriteLine(tailleGrille);
        int taille = Convert.ToInt32(Console.ReadLine());
        if (taille != 1 && taille != 2 && taille != 3)
        {
            Console.WriteLine("Choix invalide");
            Play();
        }
        switch (taille)
        {
            case 1:
                taille = 10;
                break;
            case 2:
                taille = 20;
                break;
            case 3:
                taille = 26;
                break;
        }
        Console.Clear();
        string dificulty = File.ReadAllText("assets/dificulty.txt");
        Console.WriteLine(dificulty);
        int difficulte = Convert.ToInt32(Console.ReadLine());
        if (difficulte != 1 && difficulte != 2 && difficulte != 3)
        {
            Console.WriteLine("Choix invalide");
            Play();
        }

        DateTime startTime = DateTime.Now;
        
        int nbMines = Mines.CalculateNumberOfMines(taille, difficulte);

        int casesRestantes = taille * taille - nbMines;

        List<List<char>> grid = new List<List<char>>();
        Grid.InitializeGrid(grid, taille);

        Mines.PlaceMinesRandomly(grid, taille, nbMines);
        Console.Clear();
        Grid.DisplayGrid(grid, taille);
        Input.WaitInput(grid, taille, startTime, ref casesRestantes);
    }
}