namespace TalonProtocol.Challenges;

public class PuzzleChallenge : Challenge
{
    private string Question { get; }
    private string Answer { get; }
    private string? RewardItem { get; }

    public PuzzleChallenge(string description, string question, string answer, string? rewardItem = null)
        : base(description)
    {
        Question = question;
        Answer = answer.ToLower();
        RewardItem = rewardItem;
    }

    public override void Start(Player player)
    {
        if (IsCompleted)
        {
            Console.WriteLine("You have already completed this challenge.");
            return;
        }

        Console.WriteLine("\n" + Question);
        Console.Write("Answer: ");
        string? input = Console.ReadLine();

        if (input == null || input.Trim() == "")
        {
            Console.WriteLine("You must enter an answer.");
            return;
        }

        if (input.Trim().ToLower() == Answer)
        {
            Console.WriteLine("Correct. Challenge completed.");

            if (RewardItem != null)
            {
                player.AddItem(RewardItem);
                Console.WriteLine($"You received: {RewardItem}");
            }

            IsCompleted = true;
        }
        else
        {
            Console.WriteLine("Incorrect. The base security system damages you.");
            player.TakeDamage(10);
        }
    }
}