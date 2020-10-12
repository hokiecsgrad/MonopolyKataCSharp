namespace MonopolyKata
{
    public interface IDice
    {
        public int NumSides { get; }
        public bool LastRollWasDoubles { get; }

        public (int, int) Roll();
    }
}