public class PlayerStats
{
    public string playerName;
    public int healthRemaining;
    public float distanceTravelled;
    public WeaponStats primaryWeaponStats;
    public WeaponStats secondaryWeaponStats;

    public PlayerStats() {
        primaryWeaponStats = new WeaponStats();
        secondaryWeaponStats = new WeaponStats();
    }
}
