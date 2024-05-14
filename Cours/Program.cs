using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        Program program = new Program();
        program.Menu();
    }

    int Menu()
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
            Play();
        }
        else if (choix == 2)
        {
            LoadSaveGame();
        }
        else if (choix == 3)
        {
            LeaderBoardShow();
        }
        else
        {
            Console.WriteLine("Choix invalide");
        }

        return choix;
    }

    void LoadSaveGame()
    {
        if (File.Exists("saved_game.json"))
        {
            string jsonString = File.ReadAllText("saved_game.json");
            GameSaveData saveData = JsonSerializer.Deserialize<GameSaveData>(jsonString);
            int taille = saveData.Taille;
            List<List<char>> grid = saveData.Grid;
            int casesRestantes = saveData.CasesRestantes;
            DateTime startTime = saveData.StartTime;
            DisplayGrid(grid, taille);
            WaitInput(grid, taille, startTime, ref casesRestantes);
        }
        else
        {
            Console.WriteLine("Aucune partie sauvegardée trouvée !");
            Menu();
        }
    }

    void SaveGame(List<List<char>> grid, int taille, int casesRestantes, DateTime startTime)
    {
        GameSaveData saveData = new GameSaveData();
        saveData.Taille = taille;
        saveData.Grid = grid;
        saveData.CasesRestantes = casesRestantes;
        saveData.StartTime = startTime;
        string jsonString = JsonSerializer.Serialize(saveData);
        File.WriteAllText("saved_game.json", jsonString);
        Console.WriteLine("Le jeu a été sauvegardé avec succès !");
    }

    class GameSaveData
    {
        public int Taille { get; set; }
        public int Difficulte { get; set; }
        public List<List<char>> Grid { get; set; }
        public int CasesRestantes { get; set; }
        public DateTime StartTime { get; set; }
    }

    void Play()
    {
        Console.Clear();
        Console.WriteLine("Choisissez la taille de la grille");
        Console.WriteLine("1 : 10x10");
        Console.WriteLine("2 : 20x20");
        Console.WriteLine("3 : 26x26");
        Console.WriteLine("");
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
        Console.WriteLine("Choisissez la difficulté");
        Console.WriteLine("1 : facile");
        Console.WriteLine("2 : moyen");
        Console.WriteLine("3 : difficile");
        Console.WriteLine("");
        int difficulte = Convert.ToInt32(Console.ReadLine());
        if (difficulte != 1 && difficulte != 2 && difficulte != 3)
        {
            Console.WriteLine("Choix invalide");
            Play();
        }

        DateTime startTime = DateTime.Now;
        
        int nbMines = CalculateNumberOfMines(taille, difficulte);

        int casesRestantes = taille * taille - nbMines;

        List<List<char>> grid = new List<List<char>>();
        InitializeGrid(grid, taille);

        PlaceMinesRandomly(grid, taille, nbMines);
        Console.Clear();
        DisplayGrid(grid, taille);
        WaitInput(grid, taille, startTime, ref casesRestantes);
    }

    int CalculateNumberOfMines(int taille, int difficulte)
    {
        int nbMines = 0;
        if (difficulte == 1)
        {
            nbMines = taille * taille / 10;
        }
        else if (difficulte == 2)
        {
            nbMines = taille * taille / 5;
        }
        else if (difficulte == 3)
        {
            nbMines = taille * taille / 3;
        }
        return nbMines;
    }

    void InitializeGrid(List<List<char>> grid, int taille)
    {
        for (int i = 0; i < taille; i++)
        {
            grid.Add(new List<char>());
            for (int j = 0; j < taille; j++)
            {
                grid[i].Add(' ');
            }
        }
    }

    void PlaceMinesRandomly(List<List<char>> grid, int taille, int nbMines)
    {
        Random rand = new Random();

        while (nbMines > 0)
        {
            int x = rand.Next(0, taille);
            int y = rand.Next(0, taille);

            if (grid[x][y] != 'X')
            {
                grid[x][y] = 'X';
                nbMines--;
            }
        }
    }

    void DisplayGrid(List<List<char>> grid, int taille)
    {
        string nbCase = "   ";
        string[] Letters = new[]
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
            "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };
        for (int i = 0; i < taille; i++)
        {
            nbCase += i + " ";
            if (i < 10)
            {
                nbCase += " ";
            }
        }

        Console.WriteLine(nbCase);
        for (int i = 0; i < taille; i++)
        {
            Console.Write(Letters[i] + "  ");
            for (int j = 0; j < taille; j++)
            {
                if (grid[i][j] == 'X')
                {
                    Console.Write("X  ");
                }
                else if (grid[i][j] == '0' || grid[i][j] == '1' || grid[i][j] == '2' || grid[i][j] == '3' || grid[i][j] == '4' || grid[i][j] == '5' || grid[i][j] == '6' || grid[i][j] == '7' || grid[i][j] == '8')
                {
                    Console.Write(grid[i][j] + "  ");
                }
                else
                {
                    Console.Write("X  ");
                }
            }

            Console.WriteLine("");
        }
    }

    void WaitInput(List<List<char>> grid, int taille, DateTime startTime, ref int casesRestantes)
    {
        Console.WriteLine("Choisissez une case (sous format A1) : ");
        string choixCase = Console.ReadLine().ToUpper();
        if (choixCase == "SAVE")
        {
            SaveGame(grid, taille, casesRestantes, startTime);
            Console.WriteLine("Continuer ? (O/N) : ");
            string continuer = Console.ReadLine().ToUpper();
            if (continuer == "O")
            {
                WaitInput(grid, taille, startTime, ref casesRestantes);
            }
            else
            {
                Menu();
            }
        }else if (choixCase == "WIN")
        {
            Console.WriteLine("Félicitations ! Vous avez gagné !");
            LeaderBoardAdd(taille, startTime);
        }
        int ligne = choixCase[0] - 'A';
        int colonne = Convert.ToInt32(choixCase.Substring(1));

        if (colonne < 0 || colonne >= taille || ligne < 0 || ligne >= taille)
        {
            Console.Clear();
            Console.WriteLine("Coordonnées invalides !");
            DisplayGrid(grid, taille);
            WaitInput(grid, taille, startTime, ref casesRestantes);
        }
        else
        {
            int adjMines = CountAdjacentMines(grid, taille, ligne, colonne);
            Console.WriteLine($"Vous avez choisi la case {choixCase}");
            if (grid[ligne][colonne] == 'X')
            {
                Console.WriteLine("BOUM ! Vous avez perdu !");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Vous avez survécu !");
                grid[ligne][colonne] = adjMines.ToString()[0];
                casesRestantes--;
                DisplayGrid(grid, taille);

                // Vérification de la victoire
                if (casesRestantes == 0)
                {
                    Console.WriteLine("Félicitations ! Vous avez gagné !");
                    LeaderBoardAdd(taille, startTime);
                }
                else
                {
                    WaitInput(grid, taille, startTime, ref casesRestantes);
                }
            }
        }
    }

    int CountAdjacentMines(List<List<char>> grid, int taille, int ligne, int colonne)
    {
        int count = 0;

        for (int i = ligne - 1; i <= ligne + 1; i++)
        {
            for (int j = colonne - 1; j <= colonne + 1; j++)
            {
                if (i >= 0 && i < taille && j >= 0 && j < taille)
                {
                    if (grid[i][j] == 'X')
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }
    
    void LeaderBoardShow()
    {
        int loop = 0;
        string jsonString = File.ReadAllText("leaderboard.json");
        List<Leaderboard> leaderboardList = JsonSerializer.Deserialize<List<Leaderboard>>(jsonString);
        leaderboardList.Sort((x, y) => x.duree.CompareTo(y.duree));
        foreach (Leaderboard leader in leaderboardList)
        {
            loop++;
            Console.WriteLine($"Nom : {leader.Nom} | Durée : {leader.duree} | Difficulté : {leader.dificulty}");
            if (loop == 10)
            {
                break;
            }
        }
        Thread.Sleep(10000);
        Menu();
    }
    
    void LeaderBoardAdd(int taille, DateTime startTime)
    {
        DateTime endTime = DateTime.Now;
        Console.WriteLine("Entrez votre nom pour le leaderboard : ");
        string nom = Console.ReadLine();
        Leaderboard leaderboard = new Leaderboard();
        leaderboard.Nom = nom;
        leaderboard.duree = (int)(endTime - startTime).TotalSeconds;
        switch (taille)
        {
            case 10:
                leaderboard.dificulty = 1;
                break;
            case 20:
                leaderboard.dificulty = 2;
                break;
            case 26:
                leaderboard.dificulty = 3;
                break;
        }
        string jsonString = File.ReadAllText("leaderboard.json");
        List<Leaderboard> leaderboardList = new List<Leaderboard>();
        if (!string.IsNullOrEmpty(jsonString) && jsonString.StartsWith("[") && jsonString.EndsWith("]"))
        {
            leaderboardList = JsonSerializer.Deserialize<List<Leaderboard>>(jsonString);
        }
        leaderboardList.Add(leaderboard);
        jsonString = JsonSerializer.Serialize(leaderboardList);
        File.WriteAllText("leaderboard.json", jsonString);
        Menu();
    }
    class Leaderboard
    {
        public string Nom { get; set; }
        public int duree { get; set; }
        public int dificulty { get; set; }
    }
}
