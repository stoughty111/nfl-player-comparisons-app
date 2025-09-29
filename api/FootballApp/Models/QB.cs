namespace FootballApp.Models
{
    public class QB : Player
    {
        public double PassingAttempts { get; set; }
        public double PassingCompletions { get; set; }
        public double PassingYards { get; set; }
        public double PassingCompletionPercentage { get; set; }
        public double PassingYardsPerAttempt { get; set; }
        public double PassingYardsPerCompletion { get; set; }
        public double PassingTouchdowns { get; set; }
        public double PassingInterceptions { get; set; }
        public double PassingRating { get; set; }
        public double PassingLong { get; set; }
        public double RushingYards { get; set; }
        public double RushingTouchdowns { get; set; }
    }
}
