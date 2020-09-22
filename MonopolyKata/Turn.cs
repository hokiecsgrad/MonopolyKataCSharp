namespace MonopolyKata
{
    public class Turn
    {
        private Board Board { get; }
        private Dice Dice { get; }

        public Turn(Board board, Dice dice)
        {
            Board = board;
            Dice = dice;
        }

        public void Take(Player player)
        {
            (int, int) roll;
            int numberOfTurns = 0;

            do
            {
                numberOfTurns += 1;
                roll = Dice.Roll();

                if (Dice.LastRollWasDoubles && numberOfTurns == 3)
                {
                    SendPlayerToJail(player);
                    break;
                }

                Board.Move(player, roll.Item1 + roll.Item2);

            } while (Dice.LastRollWasDoubles && numberOfTurns < 3);

            player.Rounds++;
        }

        private void SendPlayerToJail(Player player)
        {
            player.IsInJail = true;
            player.Position = Board.GetBoardPositionOf("Jail");
        }
    }
}