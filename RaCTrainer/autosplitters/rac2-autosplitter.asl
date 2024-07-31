state("racman") {}

startup
{	
    settings.Add("PROTO_SPLIT", false, "[BETA] Attempt to split on defeating protopet");
    settings.SetToolTip("PROTO_SPLIT", "Splits on defeating the protopet, the final boss.");

    settings.Add("CLANK_SPLIT", true, "A2 clank subsplit");
    settings.SetToolTip("CLANK_SPLIT", "Splits when transitioning from clank to ratchet on Aranos 2.");

    settings.Add("ARENA_SPLIT", false, "Maktar arena subsplit");
    settings.SetToolTip("ARENA_SPLIT", "Splits when entering the arena on Maktar.");
	
    settings.Add("MUSEUM_TO_BOLDAN_SPLIT", true, "Insomniac museum subsplit");
    settings.SetToolTip("MUSEUM_TO_BOLDAN_SPLIT", "Split when going from insomniac museum to Boldan.");
	
    settings.Add("JAMMING_ENTRY_SPLIT", true, "Jamming entry subsplit");
    settings.SetToolTip("JAMMING_ENTRY_SPLIT", "Split when going from Maktar to Jamming Array");
	
    settings.Add("TABORA_BARLOW_SPLIT", true, "Tabora shortcut subsplit");
    settings.SetToolTip("TABORA_BARLOW_SPLIT", "Split when shortcutting from Tabora to Barlow");
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
    if (current.destinationPlanet != 0 && current.planet == current.destinationPlanet && current.planet != old.planet && current.planet != 0)
    {
		if (current.planet == 21) 
		{
            return false;   
        }
		if (!settings["MUSEUM_TO_BOLDAN_SPLIT"] && old.planet == 21 && current.planet == 13)
		{
			return false;
		}
		if (!settings["TABORA_BARLOW_SPLIT"] && current.planet == 4 && old.planet == 8) 
        {
			return false;
		}
		if (!settings["JAMMING_ENTRY_SPLIT"] && current.planet == 26) {
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
