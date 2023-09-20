state("racman") {}

startup
{
    settings.Add("MUSEUM_SPLIT", true, "Don't split entering Insomniac Museum");
    settings.SetToolTip("MUSEUM_SPLIT", "Prevents splitting when doing IMG in NG+.");

    settings.Add("SHIP_SPLIT", false, "Only split on ship levels");
    settings.SetToolTip("SHIP_SPLIT", "Only split leaving Feltzin, Hrugis and Gorn; because Emeralve asked for it.");

    settings.Add("PROTO_SPLIT", false, "[BETA] Attempt to split on defeating protopet");
    settings.SetToolTip("PROTO_SPLIT", "Splits on defeating the protopet, the final boss.");
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
}

update
{
    vars.reader.BaseStream.Position = 0;

    current.gameState = vars.reader.ReadUInt32();
    current.playerState = vars.reader.ReadUInt32();
    current.protopetHealth = vars.reader.ReadSingle();
    current.planet = vars.reader.ReadUInt32();
    current.destinationPlanet = vars.reader.ReadUInt32();
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
    if (current.destinationPlanet != old.destinationPlanet && current.destinationPlanet != 0 && current.destinationPlanet != current.planet)
    {
        print("You're going to "+current.destinationPlanet.ToString());

        if (settings["MUSEUM_SPLIT"] && current.destinationPlanet == 21) 
        {
            return false;   
        }
        if (settings["SHIP_SPLIT"]) 
        {
            if (old.planet != 5 && old.planet != 10 && old.planet != 15 && old.planet != 25)
            {
                return false;
            }
        }
        return true;
    }

    else if (settings["PROTO_SPLIT"] && current.gameState == 0 && current.planet == 20 && current.protopetHealth < 0.04 && old.protopetHealth > 0.04)
    {
        print("Protopet is dead!!!!!");
        return true;
    }
}