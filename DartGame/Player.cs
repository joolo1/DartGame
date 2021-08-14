using System;
using System.Collections.Generic;

namespace DartGame
{
    class Player
    {
        private string name;
        public string Name
        {
            get {
                return name;
            }
            set
            {
                if (value.Length <= 0)
                    throw new FormatException("Spelaren måste ha ett namn.");

                name = value;
            }
        }

        public bool IsAI { get; set; }

        private List<Turns> turns = new List<Turns>();

        /// <summary>
        /// Property som ger andra klasser åtkomst till turns listan
        /// </summary>
        public List<Turns> Turns { get { return turns; } set { turns = value; } }

        /// <summary>
        /// Fördefinierade namn för datorspelare.
        /// </summary>
        private string[] computerNames = new string[]
        {
            "Kalle",
            "Olle",
            "Kristine",
            "Pär",
            "Martin",
            "Olga",
            "Berit",
            "Marcus"
        };

        public Player(string name)
        {
            Name = name;
        }

        public Player(bool isAI)
        {
            // Sätt denna spelare till att vara datorstyrd
            IsAI = isAI;

            // Instansiera random klassen
            Random rand = new Random();

            // Randomiserat heltal mellan 0 och längden av sträng vektorn availableNames
            int selected = rand.Next(computerNames.Length);

            // Tilldela datorspelaren ett slumpat namn från vektorn computerNames
            Name = $"Datorspelare: {computerNames[selected]}";
        }

        /// <summary>
        /// Returnerar den totala summan poäng som en spelare hittils fått.
        /// </summary>
        public int CalculatePoints()
        {
            int points = 0;

            foreach (var player in turns)
                points += player.GetScore();

            return points;
        }

        /// <summary>
        /// Lägger till en pilomgång i turns listan
        /// </summary>
        /// <param name="dart1">Första pilen</param>
        /// <param name="dart2">Andra pilen</param>
        /// <param name="dart3">Tredje pilen</param>
        public void AddTurn(int dart1, int dart2, int dart3)
        {
            Turns turn = new Turns(dart1, dart2, dart3);
            turns.Add(turn);
        }
    }
}