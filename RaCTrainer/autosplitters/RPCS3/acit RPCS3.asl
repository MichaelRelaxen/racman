state("rpcs3") { }

init
{
    IntPtr basePointer = new IntPtr(0x300000000);

    // Pointers
    int planetPtr = 0xE897B4;           // current planet
    int gameState1Ptr = 0xFBAE48;       // (0 = in game, 1 = in main menu, 2 = in pause) (NOTE: first pause will result in a 1 for a second)
    int gameState2Ptr = 0x4027CF70;     // (6 = in main menu, 7 = in game)  
    int boltCounterPtr = 0xE25068;      // bolt counter
    int ratchetCoordsPtr = 0xE24170;    // ratchet coords
    int azimuthHPPtr = 0x40E89A2C;      // Azimuth HP
    int timerPtr = 0x40EBADE0;          // timer

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
        uint gameState1 = 0;        // game state1
        uint gameState2 = 0;        // game state2
        uint boltCounter = 0;       // bolt counter
        float ratchetX = 0;         // ratchet x coord
        float ratchetY = 0;         // ratchet y coord
        float ratchetZ = 0;         // ratchet z coord
        float azimuthHP = 0;        // Azimuth HP
        float acitTimer = 0;        // timer

        // Read values from memory
        memory.ReadValue<uint>(IntPtr.Add(basePointer, planetPtr), out planet);
        memory.ReadValue<uint>(IntPtr.Add(basePointer, gameState1Ptr), out gameState1);
        memory.ReadValue<uint>(IntPtr.Add(basePointer, gameState2Ptr), out gameState2);
        memory.ReadValue<uint>(IntPtr.Add(basePointer, boltCounterPtr), out boltCounter);
        memory.ReadValue<float>(IntPtr.Add(basePointer, ratchetCoordsPtr), out ratchetX);
        memory.ReadValue<float>(IntPtr.Add(basePointer, ratchetCoordsPtr + 8), out ratchetY);
        memory.ReadValue<float>(IntPtr.Add(basePointer, ratchetCoordsPtr + 4), out ratchetZ);
        memory.ReadValue<float>(IntPtr.Add(basePointer, azimuthHPPtr), out azimuthHP);
        memory.ReadValue<float>(IntPtr.Add(basePointer, timerPtr), out acitTimer);

        // Update values
        current.planet = vars.IntToLittleEndian(planet);
        current.gameState1 = vars.IntToLittleEndian(gameState1);
        current.gameState2 = vars.IntToLittleEndian(gameState2);
        current.boltCounter = vars.IntToLittleEndian(boltCounter);
        current.ratchetX = vars.FloatToLittleEndian(ratchetX);
        current.ratchetY = vars.FloatToLittleEndian(ratchetY);
        current.ratchetZ = vars.FloatToLittleEndian(ratchetZ);
        current.azimuthHP = vars.FloatToLittleEndian(azimuthHP);
        current.timer1 = vars.FloatToLittleEndian(acitTimer);
    });
    vars.UpdateValues();

    // Initialize constants
    vars.neffy1MaxZ = 320.0f;   // this value is a bit lover than the max height of the elevator on Neffy1

    // Initialize run values
    vars.ResetRunValues = (Action)(() => {
        vars.noSplitPlanets = new HashSet<int> { 3, 5, 6, 9, 11, 15, 18 };
        vars.gameTime = 0.0f;
        vars.tempTimer = 0.0f;

        vars.reachedNeffy1FinalRoom = false;

        vars.isFirstVorselVisit = true;
        vars.onVorselonBoltCount = -1;
    });
    vars.ResetRunValues();
}

onReset
{
    vars.ResetRunValues();
}

update
{
    vars.UpdateValues();

    print(current.timer1.ToString());
    //print((!vars.noSplitPlanets.Contains((int)old.planet)).ToString());
    //print(current.planet.ToString() + " " + old.planet.ToString());

    // Check if Ratchet reached the final room in Neffy1
    if (current.planet == 17 && current.ratchetZ > vars.neffy1MaxZ)
    {
        vars.reachedNeffy1FinalRoom = true;
    }

    // If you visit Vorselon from another save it won't count the game timer
    if (old.planet == 3 && current.planet == 4 && vars.isFirstVorselVisit)
    {
        vars.isFirstVorselVisit = false;
        vars.onVorselonBoltCount = current.boltCounter;
    }

    if (current.planet == 3 && current.boltCounter == vars.onVorselonBoltCount && !vars.isFirstVorselVisit)
    {
        return;
    }

    if (old.timer > current.timer)
    {
        vars.tempTimer += old.timer;
    }
    vars.gameTime = vars.tempTimer + current.timer;
}

split
{
    if (!settings.SplitEnabled)
    {
        return false;
    }

    // planet split
    if (old.planet != current.planet && !vars.noSplitPlanets.Contains((int)old.planet))
    {
        vars.noSplitPlanets.Add((int)old.planet);
        return true;
    }
    
    // split on leaving Vorselon 2
    if (old.planet == 4 && current.planet == 10)
    {
        return true;
    }

    // Libra split (No address for Libra HP, so splitting on leaving planet 10)
    /*if (current.planet == 10 && current.libraHP == 0.0f)
    {
        return true;
    }*/

    // Neffy1 split (it splits if Ratchet reaches the final room and his z coord is less than the final room z coord)
    if (current.planet == 17 && vars.reachedNeffy1FinalRoom && current.ratchetZ < vars.neffy1MaxZ)
    {
        return true;
    }

    // Azimuth split
    if (current.planet == 20 && current.azimuthHP <= 0.0f)
    {
        return true;
    }
}

reset
{
    if (!settings.ResetEnabled)
    {
        return false;
    }

    if (current.gameState1 == 1 && old.gameState1 == 0 && current.gameState2 == 6 && current.timer == 0)
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

    if (current.planet == 1 && old.gameState1==1 && current.gameState1==0 &&
        current.gameState2 == 7 && Equals(timer.CurrentPhase.ToString(), "NotRunning"))
    {
        return true;
    }
}

gameTime
{
    return TimeSpan.FromSeconds(vars.gameTime);
}

isLoading
{
    return true;
}
