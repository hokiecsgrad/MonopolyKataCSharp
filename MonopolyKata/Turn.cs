namespace MonopolyKata
{
    public class Turn
    {
        private Board Board { get; }
        private Dice Dice { get; set; }

        public Turn(Board board, Dice dice)
        {
            Board = board;
            Dice = dice;
        }

        public void SetDice(Dice dice)
        {
            Dice = dice;
        }

        public void Take(Player player)
        {
            (int, int) roll;
            int numberOfTurns = 0;

            if (player.IsInJail) player.NumTurnsInJail++;

            do
            {
                numberOfTurns += 1;
                roll = Dice.Roll();
                player.LastRoll = roll;

                if (!player.IsInJail && Dice.LastRollWasDoubles && numberOfTurns == 3)
                {
                    SendPlayerToJail(player);
                    break;
                }

                Board.Move(player, roll.Item1 + roll.Item2);

            } while (player.NumTurnsInJail == 0 && Dice.LastRollWasDoubles && numberOfTurns < 3);

            if (!player.IsInJail) { player.NumTurnsInJail = 0; player.WantsToPayToGetOutOfJail = false; }

            player.Rounds++;
        }

        private void SendPlayerToJail(Player player)
        {
            player.IsInJail = true;
            player.Position = Board.GetBoardPositionOf("Jail");
        }
    }
}