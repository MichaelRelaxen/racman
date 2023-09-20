// Only works on NPEA00423 us version

state("rpcs3") { }

init
{
    IntPtr basePointer = new IntPtr(0x300000000);

    // Pointers
    int planetPtr = 0x119353C;          // current planet   (it's 0 in main menu)
    int loadPlanetPtr = 0x9C307C;       // load planet
    int voxHPPtr = 0x449BEAD0;          // Vox HP
    int cutscenePtr = 0xB36DE8;         // cutscene
    int inGamePtr = 0xB1F460;           // in Game (0 in main menu | 1 in game)

    // Convert big endian int to little endian int
    vars.IntToLittleEndian = (Func<uint, uint>)((bigEndianInt) => {
        byte[] bytes = BitConverter.GetBytes(bigEndianInt);
        Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    });

    // Convert big endian float to little endian float
    vars.FloatToLittleEndian = (Func<float, float>)((bigEndianFloat) => {
        byte[] bytes = BitConverter.GetBytes(bigEndianFloat);
        Array.Reverse(bytes);
        return BitConverter.ToSingle(bytes, 0);
    });

    // Update values from memory
    vars.UpdateValues = (Action) (() => {
        uint planet = 0;            // current planet
        uint loadPlanet = 0;        // load planet
        float voxHP = 0;            // Vox HP
        uint cutscene = 0;          // cutscene
        uint inGame = 0;            // in game

        // Read values from memory
        memory.ReadValue<uint>(IntPtr.Add(basePointer, planetPtr), out planet);
        memory.ReadValue<uint>(IntPtr.Add(basePointer, loadPlanetPtr), out loadPlanet);
        memory.ReadValue<float>(IntPtr.Add(basePointer, voxHPPtr), out voxHP);
        memory.ReadValue<uint>(IntPtr.Add(basePointer, cutscenePtr), out cutscene);
        memory.ReadValue<uint>(IntPtr.Add(basePointer, inGamePtr), out inGame);

        // Update values
        current.planet = vars.IntToLittleEndian(planet);
        current.loadPlanetPtr = vars.IntToLittleEndian(loadPlanet);
        current.voxHP = vars.FloatToLittleEndian(voxHP);
        current.cutscene = vars.IntToLittleEndian(cutscene);
        current.inGame = vars.IntToLittleEndian(inGame);
    });
    vars.UpdateValues();
}

update
{
    vars.UpdateValues();
}

split
{
    if (!settings.SplitEnabled)
    {
        return false;
    }

    // planet split
    if (old.loadPlanet != current.loadPlanet && current.loadPlanet != 15)
    {
        print("Split on planet " + current.loadPlanet + "->" + old.loadPlanet);
        return true;
    }

    // Vox split
    if (current.planet == 15 && current.voxHP <= 0.0f && old.cutscene == 0 && current.cutscene == 1)    
    {
        print("Split on Vox");
        return true;
    }
}

reset
{
    if (!settings.ResetEnabled)
    {
        return false;
    }

    if (current.planet == 0 && old.inGame == 1 && current.inGame == 0)
    {
        return true;
    }
}

start
{
    if (!settings.StartEnabled)
    {
        return false;
    }

    if (old.inGame == 0 && current.inGame == 1)
    {
        return true;
    }
}
