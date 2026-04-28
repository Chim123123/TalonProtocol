namespace TalonProtocol.Challenges;

public class HealingChallenge : Challenge
{
    private int HealAmount { get; }

    public HealingChallenge(string description, int healAmount)
        : base(description)
    {
        HealAmount = healAmount;
    }

    public override void Start(Player player)
    {
        if (IsCompleted)
        {
            Console.WriteLine("You have already used the medical supplies.");
            return;
        }

        player.Heal(HealAmount);
        Console.WriteLine($"You recover {HealAmount} health.");
        IsCompleted = true;
    }
}