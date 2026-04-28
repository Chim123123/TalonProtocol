namespace TalonProtocol;

public class GameController
{
    private Game Game { get; }
    private Player Player { get; }

    public GameController(Game game, Player player)
    {
        Game = game;
        Player = player;
    }

    public void DisplayPrompt()
    {
        Console.WriteLine("\n---------------------------------");
        Console.WriteLine($"Current location: {Player.CurrentLocation.Name}");
        Console.Write("Command: ");

        string? input = Console.ReadLine();

        if (input == null || input.Trim() == "")
        {
            Console.WriteLine("Invalid input. Type 'help' to see available commands.");
            return;
        }

        ProcessCommand(input.Trim().ToLower());
    }

// Interprets player commands and runs the correct game action.

    private void ProcessCommand(string command)
    {
        if (command == "help")
        {
            ShowHelp();
        }
        else if (command == "look")
        {
            Player.CurrentLocation.Display();
        }
        else if (command == "status")
        {
            Player.ShowStatus();
        }
        else if (command == "inventory")
        {
            ShowInventory();
        }
        else if (command.StartsWith("go "))
        {
            string direction = command.Replace("go ", "");
            MovePlayer(direction);
        }
        else if (command == "solve" || command == "attack" || command == "heal")
        {
            StartChallenge(command);
        }
        else if (command == "quit")
        {
            Console.WriteLine("You abandon the mission.");
            Game.IsRunning = false;
        }
        else
        {
            Console.WriteLine("Invalid command. Type 'help' to see what you can do.");
        }
    }

    private void MovePlayer(string direction)
    {
        Location? nextLocation = Player.CurrentLocation.GetExit(direction);

        if (nextLocation == null)
        {
            Console.WriteLine($"You cannot go {direction} from here.");
            return;
        }

        Player.CurrentLocation = nextLocation;
        Console.WriteLine($"You move {direction} to {Player.CurrentLocation.Name}.");
        Player.CurrentLocation.Display();
    }

private void StartChallenge(string command)
{
    if (Player.CurrentLocation.Challenge == null)
    {
        Console.WriteLine("There is no challenge to complete here.");
        return;
    }

    if (Player.CurrentLocation.Challenge.IsCompleted)
    {
        Console.WriteLine("You have already completed this challenge.");
        return;
    }

    string challengeType = Player.CurrentLocation.Challenge.GetType().Name;

    if (command == "solve" && challengeType != "PuzzleChallenge")
    {
        Console.WriteLine("There is no puzzle to solve here.");
        return;
    }

    if (command == "attack" && challengeType != "CombatChallenge")
    {
        Console.WriteLine("There is no enemy to attack here.");
        return;
    }

    if (command == "heal" && challengeType != "HealingChallenge")
    {
        Console.WriteLine("There are no medical supplies to use here.");
        return;
    }

    Player.CurrentLocation.Challenge.Start(Player);
}

    private void ShowHelp()
{
    Console.WriteLine("\nAvailable commands:");
    Console.WriteLine("look        - View your current location, exits, and challenge");
    Console.WriteLine("status      - View your health, location, and inventory");
    Console.WriteLine("inventory   - View collected items");
    Console.WriteLine("go forward  - Move forward if that exit exists");
    Console.WriteLine("go back     - Move back if that exit exists");
    Console.WriteLine("go left     - Move left if that exit exists");
    Console.WriteLine("go right    - Move right if that exit exists");
    Console.WriteLine("solve       - Attempt a puzzle challenge");
    Console.WriteLine("attack      - Attack an enemy challenge");
    Console.WriteLine("heal        - Use healing supplies if available");
    Console.WriteLine("quit        - Exit the game");

    Console.WriteLine("\nTip: Type 'look' whenever you enter a new location.");
}

    private void ShowInventory()
    {
        if (Player.Inventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty.");
        }
        else
        {
            Console.WriteLine("Inventory: " + string.Join(", ", Player.Inventory));
        }
    }
}