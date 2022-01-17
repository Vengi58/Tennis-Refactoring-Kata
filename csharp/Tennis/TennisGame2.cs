using System;

namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        class TennisPlayer
        {
            public TennisPlayer(string name)
            {
                Name = name;
            }
            public string Name { get; }
            public int Points { get; set; }
        }

        enum Scores
        {
            Love = 0,
            Fifteen = 1,
            Thirty = 2,
            Forty = 3
        }

        TennisPlayer this[string playerName]
        {
            get => player1.Name.Equals(playerName) ? player1 :
                (player2.Name.Equals(playerName) ? player2 :
                throw new IndexOutOfRangeException($"Unknown player {playerName}!")
                );
        }

        private readonly TennisPlayer player1, player2;

        public TennisGame2(string player1Name, string player2Name)
        {
            player1 = new TennisPlayer(player1Name);
            player2 = new TennisPlayer(player2Name);
        }

        public string GetScore()
        {
            return (player1, player2) switch
            {
                var (p1, p2) when IsAll(p1.Points, p2.Points) => $"{(Scores)p1.Points}-All",
                var (p1, p2) when IsDeuce(p1.Points, p2.Points) => "Deuce",
                var (p1, p2) when IsAdvantage(p1.Points, p2.Points) => $"Advantage {p1.Name}",
                var (p1, p2) when IsAdvantage(p2.Points, p1.Points) => $"Advantage {p2.Name}",
                var (p1, p2) when IsWin(p1.Points, p2.Points) => $"Win for {p1.Name}",
                var (p1, p2) when IsWin(p2.Points, p1.Points) => $"Win for {p2.Name}",
                _ => $"{(Scores)player1.Points}-{(Scores)player2.Points}"
            };
        }

        public void WonPoint(string playerName)
        {
            this[playerName].Points++;
        }

        private bool IsAll(int score1, int score2) => score1 == score2 && score1 < (int)Scores.Forty;
        private bool IsDeuce(int score1, int score2) => score1 == score2 && score1 > (int)Scores.Thirty;
        private bool IsAdvantage(int score1, int score2) => score1 - score2 == 1 && score2 >= (int)Scores.Forty;
        private bool IsWin(int score1, int score2) => score1 - score2 > 1 && score1 > (int)Scores.Forty && score2 >= (int)Scores.Love;
    }
}

