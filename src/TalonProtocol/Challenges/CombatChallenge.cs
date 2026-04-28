namespace TalonProtocol.Challenges;

public class CombatChallenge : Challenge
{
    private string EnemyName { get; }
    private int EnemyDamage { get; }
    private string? RewardItem { get; }

    public CombatChallenge(string description, string enemyName, int enemyDamage, string? rewardItem = null)
        : base(description)
    {
        EnemyName = enemyName;
        EnemyDamage = enemyDamage;
        RewardItem = rewardItem;
    }

    public override void Start(Player player)
    {
        if (IsCompleted)
        {
            Console.WriteLine("This enemy has already been defeated.");
            return;
        }

        Console.WriteLine($"\nYou attack {EnemyName}.");
        Console.WriteLine($"{EnemyName} fights back and deals {EnemyDamage} damage.");

        player.TakeDamage(EnemyDamage);

        if (player.IsAlive())
        {
            Console.WriteLine($"You defeated {EnemyName}.");

            if (RewardItem != null)
            {
                player.AddItem(RewardItem);
                Console.WriteLine($"You received: {RewardItem}");
            }

            IsCompleted = true;
        }
    }
}