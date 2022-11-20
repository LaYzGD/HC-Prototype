
public struct PlayerStats
{
    public float MovementSpeed { get; private set; }
    public int BiteStrength { get; private set; }
    public int BiteSpeed { get; private set; }

    public PlayerStats(float movementSpeed = 5f, int biteStrength = 1, int biteSpeed = 1)
    {
        MovementSpeed = movementSpeed;
        BiteStrength = biteStrength;
        BiteSpeed = biteSpeed;
    }

    public static PlayerStats operator +(PlayerStats startStats, PlayerStats upgradedStats)
    {
        return new PlayerStats
        {
            MovementSpeed = startStats.MovementSpeed + upgradedStats.MovementSpeed,
            BiteStrength = startStats.BiteStrength + upgradedStats.BiteStrength,
            BiteSpeed = startStats.BiteSpeed + upgradedStats.BiteSpeed
        };
    }
}
