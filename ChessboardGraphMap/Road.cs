namespace ChessboardGraphMap
{
    internal class Road
    {
        public Road(Location to, decimal timeToCross)
        {
            To = to;
            TimeToCross = (int)(timeToCross * 1000);
        }

        public Location To { get; }

        /// <summary>
        /// In miliseconds.
        /// </summary>
        public int TimeToCross { get; }

        public override string ToString()
        {
            return $" => {To} = {TimeToCross}";
        }
    }
}
