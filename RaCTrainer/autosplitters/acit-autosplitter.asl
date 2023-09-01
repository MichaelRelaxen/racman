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
        uint cutsceneState1 = 0;    // cutscene state
        uint cutsceneState2 = 0;    // cutscene state
        uint cutsceneState3 = 0;    // cutscene state
        uint saveFileID = 0;        // save file ID
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
        current.cutsceneState1 = vars.reader.ReadUInt32();
        current.cutsceneState2 = vars.reader.ReadUInt32();
        current.cutsceneState3 = vars.reader.ReadUInt32();
        current.saveFileID = vars.reader.ReadUInt32();
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
    vars.ResetRunValues = (Action) (() => {
        vars.noSplitPlanets = new HashSet<int> { 3, 5, 6, 9, 10, 11, 14, 15, 18 };
        vars.gameTime = 0.0f;
        vars.tempTimer = 0.0f;
        vars.runSaveFileID = -1;

        vars.korthosCutsceneCount = 0;
        vars.isLibraAlive = true;
        vars.firstVorselonVisit = true;

        vars.reachedNeffy1FinalRoom = false;
    });
    vars.ResetRunValues();
}

onStart
{
    vars.ResetRunValues();
    vars.runSaveFileID = current.saveFileID;
}

update
{
    vars.UpdateValues();

    //print(current.timer.ToString());
    //print(current.planet.ToString() + " " + old.planet.ToString());
    //print(vars.gameTime.ToString() + " " + vars.tempTimer.ToString() + " " + current.timer.ToString());

    // setting up libra fight
    if (current.planet == 10 && old.cutsceneState2 == 0 && current.cutsceneState2 == 1 && vars.isLibraAlive)
    {
        vars.korthosCutsceneCount++;
    }

    // check if the save file is changed
    if (vars.firstVorselonVisit && current.saveFileID != old.saveFileID)
    {
        vars.runSaveFileID = current.saveFileID;
    }
    
    if (vars.firstVorselonVisit && old.planet == 3 && current.planet == 4)
    {
        vars.firstVorselonVisit = false;
    }

    // check if Ratchet reached the final room in Neffy1
    if (current.planet == 17 && current.ratchetZ > vars.neffy1MaxZ)
    {
        vars.reachedNeffy1FinalRoom = true;
    }

    // not counting time spent during cutscenes
    if ((old.cutsceneState2 == 0 && current.cutsceneState2 == 1) ||
        (old.cutsceneState3 == 0 && current.cutsceneState3 == 1))
    {
        vars.tempTimer += current.timer;
    }

    if ((old.cutsceneState2 == 1 && current.cutsceneState2 == 0) ||
        (old.cutsceneState3 == 1 && current.cutsceneState3 == 0))
    {
        vars.tempTimer -= current.timer;
    }

    if (current.cutsceneState2 == 1 || current.cutsceneState3 == 1)
    {
        return;
    }

    // not counting time spent on other files
    if (current.saveFileID != vars.runSaveFileID && current.saveFileID != old.saveFileID && !vars.firstVorselonVisit)
    {
        vars.tempTimer += current.timer;
    }

    if (current.saveFileID == vars.runSaveFileID && current.saveFileID != old.saveFileID && !vars.firstVorselonVisit)
    {
        vars.tempTimer -= current.timer;
    }

    if (current.saveFileID != vars.runSaveFileID)
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
        print("Split on planet change " + old.planet.ToString() + " -> " + current.planet.ToString());
        vars.noSplitPlanets.Add((int)old.planet);
        return true;
    }
    
    // split on leaving Vorselon 2
    if (old.planet == 4 && current.planet == 10)
    {
        print("Split on leaving Vorselon 2");
        return true;
    }

    // Libra split
    if (current.planet == 10 && vars.isLibraAlive && vars.korthosCutsceneCount == 3)
    {
        print("Split on Libra");
        vars.isLibraAlive = false;
        return true;
    }

    // Neffy1 split (it splits if Ratchet reaches the final room and his z coord is less than the final room z coord)
    if (current.planet == 17 && vars.reachedNeffy1FinalRoom && current.ratchetZ < vars.neffy1MaxZ)
    {
        vars.reachedNeffy1FinalRoom = false;
        return true;
    }

    // Azimuth split
    if (current.planet == 20 && current.azimuthHP <= 0.0f && current.cutsceneState1 == 1 && old.cutsceneState1 == 0)
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

    // any%
    if (current.gameState1 == 1 && old.gameState1 == 0 && current.timer == 0)
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

    // any%
    if (current.planet == 1 && old.gameState1==1 && current.gameState1==0 && Equals(timer.CurrentPhase.ToString(), "NotRunning"))
    {
        return true;
    }

    // NG+
    if (current.planet == 1 && old.gameState1==2 && current.gameState1==0 && Equals(timer.CurrentPhase.ToString(), "NotRunning"))
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
