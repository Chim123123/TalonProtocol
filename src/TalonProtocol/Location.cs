using TalonProtocol.Challenges;

namespace TalonProtocol;

public class Location
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Dictionary<string, Location> Exits { get; private set; }
    public Challenge? Challenge { get; private set; }

    public Location(string name, string description, Challenge? challenge = null)
    {
        Name = name;
        Description = description;
        Challenge = challenge;
        Exits = new Dictionary<string, Location>();
    }

    public void AddExit(string direction, Location location)
    {
        Exits[direction.ToLower()] = location;
    }

    public Location? GetExit(string direction)
    {
        direction = direction.ToLower();

        if (Exits.ContainsKey(direction))
        {
            return Exits[direction];
        }

        return null;
    }

    public void Display()
    {
        Console.WriteLine($"\n=== {Name} ===");
        Console.WriteLine(Description);

        if (Challenge != null && !Challenge.IsCompleted)
{
    Console.WriteLine($"\nChallenge: {Challenge.Description}");

    if (Challenge is PuzzleChallenge)
    {
        Console.WriteLine("Suggested command: solve");
    }
    else if (Challenge is CombatChallenge)
    {
        Console.WriteLine("Suggested command: attack");
    }
    else if (Challenge is HealingChallenge)
    {
        Console.WriteLine("Suggested command: heal");
    }
}
else if (Challenge != null && Challenge.IsCompleted)
{
    Console.WriteLine("\nChallenge completed.");
}

        Console.WriteLine("\nAvailable exits: " + string.Join(", ", Exits.Keys));
    }
}