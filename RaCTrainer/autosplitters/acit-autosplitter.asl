state("racman") { }

startup
{
    settings.Add("Round", true, "Round final time to nearest second");
    settings.SetToolTip("Round", "This makes it match the time you see on the file.");
}

init
{
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);

    // Update values from memory
    vars.UpdateValues = (Action) (() => {
        vars.reader.BaseStream.Position = 0;
        
        // Read values from memory
        current.planet = vars.reader.ReadUInt32();
        current.gameState = vars.reader.ReadUInt32();
        current.cutsceneState1 = vars.reader.ReadUInt32();
        current.cutsceneState2 = vars.reader.ReadUInt32();
        current.cutsceneState3 = vars.reader.ReadUInt32();
        current.saveFileID = vars.reader.ReadUInt32();
        current.azimuthHP = vars.reader.ReadSingle();
        current.LibraHP = vars.reader.ReadSingle();
        current.vorselon1SpaceCombat = vars.reader.ReadUInt32();
        current.neffy1finalRoom = vars.reader.ReadUInt32();
        current.wasGC2Visited = vars.reader.ReadUInt32();
        current.timer = vars.reader.ReadSingle();
        current.firstCutscene = vars.reader.ReadUInt32();
        current.loadSaveState = vars.reader.ReadUInt32();

        current.checkpointTimer = vars.reader.ReadSingle();
        current.IGT = vars.reader.ReadUInt32();
    });
    vars.UpdateValues();

    // Initialize run values
    vars.ResetRunValues = (Action) (() => {
        vars.gameTime = 0.0f;
        vars.initTimer = 0.0f;
        vars.possibleSaveDetection = false;
        vars.loopCounter = 0;
        vars.waitTillTimerChanges = false;

        vars.gameIsAboutToStart = false;
        vars.runSaveFileID = -1;
        vars.isLibraSpawned = false;
        vars.isPlayerOnRunSaveFile = true;
        vars.waitTillFileChanges = false;

        vars.runSaves = new HashSet<int>();
    });
    vars.ResetRunValues();
}

onStart
{
    vars.ResetRunValues();
    vars.runSaveFileID = current.saveFileID;
    vars.runSaves.Add((int)current.saveFileID);
    vars.initTimer = -current.IGT -current.timer;
    vars.gameIsAboutToStart = false;
}

onReset
{
    vars.gameIsAboutToStart = false;
}

update
{
    vars.UpdateValues();
    vars.isPlayerOnRunSaveFile = vars.runSaveFileID == current.saveFileID;
    vars.loopCounter++;

    if (!vars.gameIsAboutToStart)
    {
        vars.gameIsAboutToStart = (current.planet == 1 || current.planet == 0) && old.firstCutscene == 0 && current.firstCutscene == 1;
    }
    
    // update libra spawned state
    if (current.planet == 10 && current.LibraHP > 0.6f)
    {
        vars.isLibraSpawned = true;
    }

    // update save file ID if the change is caused by a save and only if the player was on the run save file
    if (!vars.isPlayerOnRunSaveFile && current.loadSaveState == 1 && old.saveFileID == vars.runSaveFileID)
    {
        vars.runSaveFileID = current.saveFileID;
        vars.runSaves.Add((int)current.saveFileID);
    }

    // there is a bug where in the first run the save file ID is always 0
    if (vars.runSaveFileID == 0 && old.saveFileID != 0 && current.saveFileID != old.saveFileID)
    {
        vars.runSaveFileID = current.saveFileID;
    }

    // don't update the timer if the save file ID is different from the run save file ID
    if (!vars.isPlayerOnRunSaveFile)
    {
        vars.waitTillFileChanges = true;
    }

    // if the player changes back to the run save file, then we must wait until the timer goes back to 0
    if (vars.isPlayerOnRunSaveFile && current.timer <= 0.1f && vars.waitTillFileChanges)
    {
        vars.waitTillFileChanges = false;
    }
    
    if (vars.waitTillFileChanges)
    {
        return;
    }

    // Timer related

    // this fixes the timer when the game saves and the IGT is out of sync by a fraction of a second
    if (!vars.possibleSaveDetection && current.timer < old.timer)
    {
        vars.possibleSaveDetection = true;
        vars.loopCounter = 0;
    }
    if (vars.possibleSaveDetection && vars.loopCounter > 10)
    {
        vars.possibleSaveDetection = false;
    }
    if (vars.possibleSaveDetection)
    {
        return;
    }

    // do not update the timer in case the timer drops. This is (can) due to the fact that the
    // IGT and the checkpoint timers are updated at different times. Sometimes if the game saves the timer
    // will drop by the checkpoint timer for a split second.
    if (current.timer < old.timer)
    {
        vars.waitTillTimerChanges = true;
    }
    if (current.timer > old.timer)
    {
        vars.waitTillTimerChanges = false;
    }
    if (vars.waitTillTimerChanges)
    {
        return;
    }

    vars.gameTime = vars.initTimer + current.IGT + current.timer;
    
    //print(vars.gameTime.ToString());
    //print(current.planet.ToString() + " " + old.planet.ToString());
}

split
{
    if (!settings.SplitEnabled)
    {
        return false;
    }

    // don't split if the save file ID is different from the run save file ID
    if (vars.runSaveFileID != current.saveFileID)
    {
        return false;
    }

    // GC1
    if (old.planet == 1 && current.planet == 2)
    {
        print("Split on leaving GC1");
        return true;
    }

    // Zolar
    if (old.planet == 2 && current.planet == 3 && current.vorselon1SpaceCombat == 0)
    {
        print("Split on leaving Zolar");
        return true;
    }

    // Vorselon 1
    if (old.planet == 4 && current.planet == 3 && current.wasGC2Visited == 0)
    {
        print("Split on leaving Vorselon 1");
        return true;
    }

    // Molonoth
    if (old.planet == 7 && current.planet == 6)
    {
        print("Split on leaving Molonoth");
        return true;
    }

    // Axiom
    if (old.planet == 8 && current.planet == 6)
    {
        print("Split on leaving Axiom");
        return true;
    }

    // Libra
    if (current.planet == 10 && current.LibraHP <= 0.1f && old.cutsceneState2 == 0 && current.cutsceneState2 == 1 && vars.isLibraSpawned)
    {
        print("Split on beating Libra");
        return true;
    }

    // Battleplex
    if (old.planet == 12 && current.planet == 4)
    {
        print("Split on leaving Battleplex");
        return true;
    }
    
    // Vorselon 2
    if (old.planet == 4 && current.planet == 10)
    {
        print("Split on leaving Vorselon 2");
        return true;
    }

    // Gemlik
    if (old.planet == 19 && current.planet == 17)
    {
        print("Split on leaving Gemlik");
        return true;
    }

    // Neffy 1
    if (current.planet == 17 && old.neffy1finalRoom == 1 && current.neffy1finalRoom == 2)
    {
        print("Split on leaving Neffy 1");
        return true;
    }

    // Neffy 2
    if (old.planet == 17 && current.planet == 20)
    {
        print("Split on leaving Neffy 2");
        return true;
    }

    // Azimuth split
    if (current.planet == 20 && current.azimuthHP <= 0.0f && current.cutsceneState1 == 1 && old.cutsceneState1 == 0)
    {
        if (settings["Round"]) 
        {
            var t = timer.CurrentTime.GameTime.Value;
            if (t.Milliseconds > 0) {

                timer.SetGameTime(TimeSpan.FromSeconds(Math.Ceiling(t.TotalSeconds)));
            }
        }
        return true;
    }
}

reset
{
    if (!settings.ResetEnabled)
    {
        return false;
    }

    // if in main menu
    if (current.gameState == 1 && old.gameState == 0 && current.timer == 0)
    {
        print("Reset on main menu");
        return true;
    }

    // if NG+
    if (!vars.runSaves.Contains((int)current.saveFileID))
    {
        print("Reset on leaving run save files");
        return true;
    }
}

start
{
    if (!settings.StartEnabled)
    {
        return false;
    }

    // if the first cutscene starts
    if (vars.gameIsAboutToStart && current.timer < 0.1f)
    {
        print("Start on first cutscene");
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
