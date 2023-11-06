state("racman") { }

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
        current.boltCounter = vars.reader.ReadUInt32();
        current.azimuthHP = vars.reader.ReadSingle();
        current.LibraHP = vars.reader.ReadSingle();
        current.vorselon1SpaceCombat = vars.reader.ReadUInt32();
        current.neffy1SpaceCombat = vars.reader.ReadUInt32();
        current.wasGC2Visited = vars.reader.ReadUInt32();
        current.timer = vars.reader.ReadSingle();
        current.isLoading = vars.reader.ReadUInt32();
    });
    vars.UpdateValues();

    // Initialize run values
    vars.ResetRunValues = (Action) (() => {
        vars.gameTime = 0.0f;
        vars.tempTimer = 0.0f;
        vars.runSaveFileID = -1;

        vars.startAfterLoad = false;
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
}

split
{
    if (!settings.SplitEnabled)
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
    // TODO: check if ratchet is on the rigth save file
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
    if (current.planet == 10 && current.LibraHP <= 0.1f && old.cutsceneState2 == 0 && current.cutsceneState2 == 1)
    {
        print("Split on beating Libra");
        return true;
    }

    // Battleplex
    if (old.planet == 12 && current.planet == 3)
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
    if (current.planet == 17 && old.neffy1SpaceCombat == 1 && current.neffy1SpaceCombat == 2)
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
    if (current.planet == 1 && old.gameState==2 && current.gameState==0 && Equals(timer.CurrentPhase.ToString(), "Running"))
    {
        print("Reset on NG+");
        return true;
    }
}

start
{
    if (!settings.StartEnabled)
    {
        return false;
    }

    // if in main menu
    if (current.planet == 1 && old.gameState==1 && current.gameState==0 && Equals(timer.CurrentPhase.ToString(), "NotRunning"))
    {
        print("Starting on main menu");
        vars.startAfterLoad = true;
    }

    // if NG+
    if (current.planet == 1 && old.gameState==2 && current.gameState==0 && Equals(timer.CurrentPhase.ToString(), "NotRunning"))
    {
        print("Starting on NG+");
        vars.startAfterLoad = true;
    }

    if (vars.startAfterLoad && old.isLoading == 1 && current.isLoading == 0)
    {
        print("Start");
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
