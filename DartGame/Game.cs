using System;
using System.Collections.Generic;
using System.Threading;

namespace DartGame
{
    class Game
    {
        private int numberOfPlayers;
        private int numberOfAIPlayers;

        /// <summary>
        /// Används vid flera tillfällen för att kontrollera loopar i programmet
        /// </summary>
        private int counter;

        private List<Player> players = new List<Player>();

        public void StartGame()
        {
            // Rensa spelarlistan vid ett nytt spel
            players.Clear();

            Console.Clear();

            Console.WriteLine(".:##### Välkommen till Dart 301! #####:.\n");
            Console.WriteLine("- Spelare kastar sina pilar i tur och ordning. Man får tre kast per omgång.");
            Console.WriteLine("- Varje pil kan ge från 0 till 20 poäng.");
            Console.WriteLine("- Den spelare vars totala summa når över 301 poäng vinner spelet.");
            Console.WriteLine("- Den vinnande spelaren kommer att visas på skärmen samt statistik över hur spelaren kastat sina pilar för att vinna.");
            Console.WriteLine("\nInnan spelet börjar så måste du ange hur många spelare som ska vara med.\n");

            while (true)
            {
                Console.Write("Hur många spelare ska vara med? ");

                try
                {
                    // Kontrollera så att inmatningen är av datatypen int
                    numberOfPlayers = Convert.ToInt32(Console.ReadLine());

                    // Kontrollera även så att det faktiskt finns åtminstonde en spelare
                    if (numberOfPlayers <= 0)
                        throw new OverflowException("\nDet måste finnas åtminstonde en spelare med.\n");

                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("\nInmatningen måste vara ett heltal.\n");
                }
                catch(OverflowException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.Clear();

            Console.WriteLine($"## Ok, så {numberOfPlayers} spelare ska vara med.\n");

            counter = 1;

            do
            {
                while (true)
                {
                    try
                    {
                        Console.Write($"Ange namnet på spelare nummer {counter}: ");

                        string nameInput = Console.ReadLine();

                        players.Add(new Player(nameInput));
                        counter++;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            } while (counter < numberOfPlayers + 1);

            Console.Clear();

            Console.Write("Hur många datorstyrda spelare ska vara med i spelet? ");

            while (true)
            {
                try
                {
                    // Kontrollera så att inmatningen är av datatypen int
                    numberOfAIPlayers = Convert.ToInt32(Console.ReadLine());

                    // Lägg till varje datorspelare i players listan
                    for (int i = 0; i < numberOfAIPlayers; i++)
                        players.Add(new Player(true));

                    break;
                }
                catch
                {
                    Console.WriteLine("\nInmatningen måste vara ett heltal.\n");
                }
            }

            Console.Clear();

            Console.WriteLine("##### SPELARLISTA #####");

            counter = 0;

            // Skriv ut alla spelare som är med samt deras spelarnummer
            foreach (Player player in players)
            {
                counter++;
                Console.WriteLine($"Spelare {counter}: {player.Name}");
            }

            Console.WriteLine($"\nSpelaren som börjar är: {players[0].Name}, tryck på valfri tangent för att starta spelet");
            Console.Read();

            Random random = new Random();

            // Vektor som tilldelas de 3 pilar som spelaren kastar
            int[] dart = new int[4];

            do
            {
                for (int i = 0; i < players.Count; i++)
                {
                    Console.Clear();
                    Console.WriteLine($"----- Nuvarande spelare: {players[i].Name} -----");

                    for (int x = 1; x < 4; x++)
                    {
                        // Om spelaren är datorstyrd så ska pilkasten ske automatiskt
                        if (players[i].IsAI)
                        {
                            Console.WriteLine($"Kastar nu {x}:a pilen...");

                            // Sov i 1000ms innan datorns kastar nästa kast
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Console.WriteLine($"Tryck på en tangent för att kasta {x}:a pilen\n");
                            Console.ReadKey();
                        }

                        dart[x] = random.Next(0, 21);

                        Console.WriteLine($"Pil {x} gav: >-- {dart[x]} --> poäng.");
                    }

                    // Spara spelarens omgång
                    players[i].AddTurn(dart[1], dart[2], dart[3]);

                    Console.WriteLine($"\n{players[i].Name} har nu totalt {players[i].CalculatePoints()} poäng.");
                    Console.WriteLine("Tryck på valfri tangent för att fortsätta.");

                    // Om spelaren når över 301 poäng så har den vunnit spelet
                    if (players[i].CalculatePoints() > 301)
                    {
                        Console.Clear();
                        Console.WriteLine($"\nSpelare {players[i].Name} vann spelet med totalt {players[i].CalculatePoints()} poäng!");
                        Console.WriteLine("Alla rundor denna spelare har kastat såg ut på följande sätt:\n");
                        Console.WriteLine("#######################################");

                        counter = 0;

                        foreach (Turns turn in players[i].Turns)
                        {
                            counter++;
                            Console.WriteLine($"Runda {counter}: {turn.FirstDart}, {turn.SecondDart}, {turn.ThirdDart}");
                        }

                        Console.WriteLine("#######################################\n");

                        Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn.");
                        Console.ReadKey();
                        StartGame();
                    }

                    Console.ReadKey();
                }
            } while (true);
        }
    }
}