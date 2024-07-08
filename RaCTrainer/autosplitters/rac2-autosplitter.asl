state("racman") {}

startup
{
    settings.Add("MUSEUM_SPLIT", true, "Don't split entering Insomniac Museum");
    settings.SetToolTip("MUSEUM_SPLIT", "Prevents splitting when doing IMG in NG+.");

    settings.Add("PROTO_SPLIT", false, "[BETA] Attempt to split on defeating protopet");
    settings.SetToolTip("PROTO_SPLIT", "Splits on defeating the protopet, the final boss.");

    settings.Add("CLANK_SPLIT", true, "A2 clank subsplit");
    settings.SetToolTip("CLANK_SPLIT", "Splits when transitioning from clank to ratchet on Aranos 2.");

    settings.Add("ARENA_SPLIT", false, "Maktar arena subsplit");
    settings.SetToolTip("ARENA_SPLIT", "Splits when entering the arena on Maktar.");
}

init
{
	System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);
    
    vars.reader.BaseStream.Position = 0;

    current.gameState = vars.reader.ReadUInt32();
    current.playerState = vars.reader.ReadUInt32();
    current.protopetHealth = vars.reader.ReadSingle();
    current.planet = vars.reader.ReadUInt32();
    current.destinationPlanet = vars.reader.ReadUInt32();
    current.chunk = vars.reader.ReadByte();
    current.clank = vars.reader.ReadByte();
}

update
{
    vars.reader.BaseStream.Position = 0;

    current.gameState = vars.reader.ReadUInt32();
    current.playerState = vars.reader.ReadUInt32();
    current.protopetHealth = vars.reader.ReadSingle();
    current.planet = vars.reader.ReadUInt32();
    current.destinationPlanet = vars.reader.ReadUInt32();
    current.chunk = vars.reader.ReadByte();
    current.clank = vars.reader.ReadByte();
}

start
{
    return current.planet == 0 && current.playerState == 98 && old.playerState == 0;
    print("Starting the run");
}

reset
{
    return current.planet == 0 && current.playerState == 98 && old.playerState == 0;
}

split
{
    if (current.planet != old.planet && current.planet != 0)
    {
        if (settings["MUSEUM_SPLIT"] && current.planet == 21) 
        {
            return false;   
        }
        return true;
    }

    if (settings["ARENA_SPLIT"] && current.planet == 2 && current.chunk == 1 && old.chunk == 0) 
    {
        return true;
    }

    if (settings["CLANK_SPLIT"] && current.planet == 14 && current.clank == 128 && old.clank == 0) 
    {
        return true;
    }

    if (settings["PROTO_SPLIT"] && current.gameState == 0 && current.planet == 20 && current.protopetHealth < 0.04 && old.protopetHealth > 0.04)
    {
        print("Protopet is dead!!!!!");
        return true;
    }
}