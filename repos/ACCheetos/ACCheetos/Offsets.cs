namespace ACCheetos
{
    public enum Offsets
    {
        //Pointers
        ViewMatrix = 0x17DFFC - 0x6C + 0x4 * 16,
        LocalPlayer = 0x17E0A8,
        EntityList = 0x191FCC,

        //Offsets from entity class
        vHead = 0x4,
        vFeet = 0x28,
        vAngles = 0x34,
        iHealth = 0xEC,
        iDead = 0xB4,
        sName = 0x205,
        iTeam = 0x30C,
        iAmmo = 0x140,

        //Max players room
        //iPlayers = 0x58AC0C in memory view
        iPlayers = 0x18AC0C
    }
}
