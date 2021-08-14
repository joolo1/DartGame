namespace DartGame
{
    class Turns
    {
        public int FirstDart   { get; set; }
        public int SecondDart  { get; set; }
        public int ThirdDart   { get; set; }

        public Turns(int dart1, int dart2, int dart3)
        {
            FirstDart  = dart1;
            SecondDart = dart2;
            ThirdDart  = dart3;
        }

        /// <summary>
        /// Returnerar den sammanlagda poängen för en pilomgång
        /// </summary>
        /// <returns>Sammanlagd summa</returns>
        public int GetScore()
        {
            return (FirstDart + SecondDart + ThirdDart);
        }
    }
}