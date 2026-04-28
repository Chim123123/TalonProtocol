using TalonProtocol.Challenges;

namespace TalonProtocol;

public class Game
{
    private Player Player { get; set; }
    private GameController Controller { get; set; }
    public bool IsRunning { get; set; }

    public Game()
    {
        Player = new Player("Recruit");
        Controller = new GameController(this, Player);
        IsRunning = true;

        SetupGame();
    }

// Creates all locations, connects exits, and assigns challenges.

    private void SetupGame()
    {
        Location entranceHall = new Location(
            "Entrance Hall",
            "You stand inside the entrance of a hidden Talon-style base. Red lights flash across the walls."
        );

        Location trainingRoom = new Location(
            "Training Room",
            "Broken drones and weapon targets fill the room.",
            new CombatChallenge(
                "A Talon guard blocks your path.",
                "Talon Guard",
                15,
                "Training Pass"
            )
        );

        Location laboratory = new Location(
            "Laboratory",
            "Strange chemicals bubble inside glass tanks. A terminal waits for input.",
            new PuzzleChallenge(
                "A science puzzle is displayed on the terminal.",
                "What gas do humans need to breathe?",
                "oxygen",
                "Lab Notes"
            )
        );

        Location armoury = new Location(
            "Armoury",
            "Weapon crates line the walls. One crate is locked by a keypad.",
            new PuzzleChallenge(
                "The weapon crate requires a simple access code.",
                "What is 12 + 8?",
                "20",
                "Access Key"
            )
        );

        Location medBay = new Location(
            "Med Bay",
            "Medical supplies are scattered across the room.",
            new HealingChallenge(
                "You can use the medical supplies to recover health.",
                30
            )
        );

        Location securityRoom = new Location(
            "Security Room",
            "Screens show every corridor in the base.",
            new PuzzleChallenge(
                "A security console asks for a three-digit override code found elsewhere in the base.",
                "Enter the security code:",
                "314"
            )
        );

        Location serverRoom = new Location(
            "Server Room",
            "The base servers hum loudly. A hacker defence system activates.",
            new CombatChallenge(
                "Cipher's digital trap attacks you.",
                "Cipher System",
                20,
                "System Override"
            )
        );

        Location commandCentre = new Location(
            "Command Centre",
            "You have reached the heart of the base. The final override terminal stands before you.",
            new PuzzleChallenge(
                "The final terminal asks for the override phrase.",
                "Type the final phrase: talon protocol",
                "talon protocol"
            )
        );

        entranceHall.AddExit("forward", trainingRoom);

        trainingRoom.AddExit("back", entranceHall);
        trainingRoom.AddExit("left", laboratory);
        trainingRoom.AddExit("right", armoury);

        laboratory.AddExit("back", trainingRoom);
        laboratory.AddExit("forward", medBay);

        medBay.AddExit("back", laboratory);

        armoury.AddExit("back", trainingRoom);
        armoury.AddExit("forward", securityRoom);

        securityRoom.AddExit("back", armoury);
        securityRoom.AddExit("left", serverRoom);

        serverRoom.AddExit("back", securityRoom);
        serverRoom.AddExit("forward", commandCentre);

        commandCentre.AddExit("back", serverRoom);

        Player.CurrentLocation = entranceHall;
    }

    public void Start()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("       TALON PROTOCOL");
        Console.WriteLine("=================================");
        Console.WriteLine("You are a new recruit inside a secret Talon-style base.");
        Console.WriteLine("Your mission is to survive, collect key items, and override the Command Centre.");
        Console.WriteLine("\nWin condition:");
        Console.WriteLine("- Collect the Access Key");
        Console.WriteLine("- Collect the System Override");
        Console.WriteLine("- Complete the final Command Centre puzzle");
        Console.WriteLine("\nLose condition:");
        Console.WriteLine("- Your health reaches 0");
        Console.WriteLine("\nType 'help' to see available commands.");
        Player.CurrentLocation.Display();

        while (IsRunning)
        {
            if (!Player.IsAlive())
            {
                Console.WriteLine("\nYour health has reached 0.");
                Console.WriteLine("MISSION FAILED.");
                IsRunning = false;
                break;
            }

            if (CheckWinCondition())
            {
                Console.WriteLine("\nYou activate the final terminal and take control of the base.");
                Console.WriteLine("MISSION COMPLETE. YOU WIN.");
                IsRunning = false;
                break;
            }

            Controller.DisplayPrompt();
        }
    }

// Checks whether the player has met all conditions required to win.

    private bool CheckWinCondition()
    {
        return Player.CurrentLocation.Name == "Command Centre"
               && Player.HasItem("Access Key")
               && Player.HasItem("System Override")
               && Player.CurrentLocation.Challenge != null
               && Player.CurrentLocation.Challenge.IsCompleted;
    }
}