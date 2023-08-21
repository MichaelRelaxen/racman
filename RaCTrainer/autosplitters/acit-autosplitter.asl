state("racman") { }

init
{
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);

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

        vars.reader.BaseStream.Position = 0;

        // Read values from memory
        current.planet = vars.reader.ReadUInt32();
        current.gameState1 = vars.reader.ReadUInt32();
        current.gameState2 = vars.reader.ReadUInt32();
        current.boltCounter = vars.reader.ReadUInt32();
        current.ratchetX = vars.reader.ReadSingle();
        current.ratchetY = vars.reader.ReadSingle();
        current.ratchetZ = vars.reader.ReadSingle();
        current.azimuthHP = vars.reader.ReadSingle();
        current.timer = vars.reader.ReadSingle();
        
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
        vars.onVorselonBoltCount = 0;
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

    //print(current.timer.ToString());
    //print(current.gameState1.ToString() + " " + old.gameState1.ToString());

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
        vars.noSplitPlanets.Add(old.planet);
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
    if (current.planet == 20 && current.azimuthHP == 0.0f)
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
