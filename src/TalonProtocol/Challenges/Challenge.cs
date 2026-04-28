namespace TalonProtocol.Challenges;

public abstract class Challenge
{
    public string Description { get; protected set; }
    public bool IsCompleted { get; protected set; }

    protected Challenge(string description)
    {
        Description = description;
        IsCompleted = false;
    }

    public abstract void Start(Player player);
}