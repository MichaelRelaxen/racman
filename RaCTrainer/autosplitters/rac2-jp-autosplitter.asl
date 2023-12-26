state("racman") {}

startup
{

}

init
{
	System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);
    
    vars.reader.BaseStream.Position = 0;

    current.playerState = vars.reader.ReadUInt32();
    current.planet = vars.reader.ReadUInt32();
}

update
{
    vars.reader.BaseStream.Position = 0;

    current.playerState = vars.reader.ReadUInt32();
    current.planet = vars.reader.ReadUInt32();
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
    if (current.planet != old.planet) 
    {
        return true;
    }
}