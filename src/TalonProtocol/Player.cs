namespace TalonProtocol;

public class Player
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public List<string> Inventory { get; private set; }
    public Location CurrentLocation { get; set; }

    public Player(string name)
    {
        Name = name;
        Health = 100;
        Inventory = new List<string>();
        CurrentLocation = null!;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if (Health < 0)
        {
            Health = 0;
        }
    }

    public void Heal(int amount)
    {
        Health += amount;

        if (Health > 100)
        {
            Health = 100;
        }
    }

    public void AddItem(string item)
    {
        if (!Inventory.Contains(item))
        {
            Inventory.Add(item);
        }
    }

    public bool HasItem(string item)
    {
        return Inventory.Contains(item);
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public void ShowStatus()
    {
        Console.WriteLine("\n--- Player Status ---");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Health}");

        if (Inventory.Count == 0)
        {
            Console.WriteLine("Inventory: Empty");
        }
        else
        {
            Console.WriteLine("Inventory: " + string.Join(", ", Inventory));
        }
    }
}