namespace YuGiOh {
    public class Game {
        public Player[] players;
        public CardEntity fieldZone;

        public int currentPlayer;

        public Game (int numPlayers = 2) {
            players = new Player[numPlayers];

            for (int i = 0; i < numPlayers; i++) {
                players[i] = new Player();
            }
        } 
    }

}