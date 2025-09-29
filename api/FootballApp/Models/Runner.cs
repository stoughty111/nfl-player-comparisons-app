namespace FootballApp.Models
{
    public class Runner : Player
    {
        public double RushingAttempts { get; set; }
        public double RushingYards { get; set; }
        public double RushingYardsPerAttempt { get; set; }
        public double RushingTouchdowns { get; set; }
        public double RushingLong { get; set; }
        public double Receptions { get; set; }
        public double ReceivingYards { get; set; }
        public double ReceivingTouchdowns { get; set; }
    }
}
