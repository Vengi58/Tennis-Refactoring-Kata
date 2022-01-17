using System;

namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private int p1point;
        private int p2point;

        private string p1res = "";
        private string p2res = "";
        private string player1Name;
        private string player2Name;
        private readonly TennisPlayer player1, player2;

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

        public TennisGame2(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            p1point = 0;
            this.player2Name = player2Name;

            player1 = new TennisPlayer(player1Name);
            player2 = new TennisPlayer(player2Name);
        }

        private bool IsAll(int score1, int score2) => score1 == score2 && score1 < (int)Scores.Forty;
        private bool IsDeuce(int score1, int score2) => score1 == score2 && score1 > (int)Scores.Thirty;
        private bool IsAdvantage(int score1, int score2) => score1 - score2 == 1 && score2 >= (int)Scores.Forty;
        private bool IsWin(int score1, int score2) => score1 - score2 > 1 && score1 > (int)Scores.Forty && score2 >= (int)Scores.Love;
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

        public void SetP1Score(int number)
        {
            for (int i = 0; i < number; i++)
            {
                P1Score();
            }
        }

        public void SetP2Score(int number)
        {
            for (var i = 0; i < number; i++)
            {
                P2Score();
            }
        }

        private void P1Score()
        {
            p1point++;
            player1.Points++;
        }

        private void P2Score()
        {
            p2point++;
            player2.Points++;
        }

        public void WonPoint(string playerName)
        {
            this[playerName].Points++;
        }
    }
}

