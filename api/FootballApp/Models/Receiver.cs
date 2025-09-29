namespace FootballApp.Models
{
    public class Receiver : Player
    {
        public double ReceivingTargets { get; set; }
        public double Receptions { get; set; }
        public double ReceivingYards { get; set; }
        public double ReceivingYardsPerReception { get; set; }
        public double ReceivingTouchdowns { get; set; }
        public double ReceivingLong { get; set; }
    }
}
