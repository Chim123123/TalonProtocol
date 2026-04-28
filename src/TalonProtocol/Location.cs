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
        }

        Console.WriteLine("\nAvailable exits: " + string.Join(", ", Exits.Keys));
    }
}