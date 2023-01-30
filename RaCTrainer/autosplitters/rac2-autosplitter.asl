state("racman") {}

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
        return true;

    }

    else if (current.gameState == 0 && current.planet == 20 && current.protopetHealth == 0)
    {
        print("Protopets health is 0!!!!!");
        //return true;
        // Commenting this out until I fix it
    }
}