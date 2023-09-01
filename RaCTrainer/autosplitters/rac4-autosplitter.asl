// Only works on PAL

state("racman") { }

init
{
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);

    // Update values from memory
    vars.UpdateValues = (Action) (() => {
        uint planet = 0;            // current planet
        uint loadPlanet = 0;        // load planet
        float voxHP = 0;            // Vox HP
        uint cutscene = 0;          // cutscene
        uint inGame = 0;            // in game

        vars.reader.BaseStream.Position = 0;

        // Read values from memory
        current.planet = vars.reader.ReadUInt32();
        current.loadPlanet = vars.reader.ReadUInt32();
        current.voxHP = vars.reader.ReadSingle();
        current.cutscene = vars.reader.ReadUInt32();
        current.inGame = vars.reader.ReadUInt32();
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
