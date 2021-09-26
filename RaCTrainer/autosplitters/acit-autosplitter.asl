state("racman") {}

startup {
    print("Starting");
}

init {
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);
    
    vars.reader.BaseStream.Position = 0;

    current.planetFramesCount = vars.reader.ReadUInt32();
    current.isPaused = vars.reader.ReadByte();
    current.gameState = vars.reader.ReadByte();

    vars.shouldPauseTimer = false;
}

update {
    vars.reader.BaseStream.Position = 0;

    current.planetFramesCount = vars.reader.ReadUInt32();
    current.isPaused = vars.reader.ReadByte();
    current.gameState = vars.reader.ReadByte();

    if(current.gameState != 7 || current.isPaused == 1)
    {
        vars.shouldPauseTimer = true;
    }
    else
    {
        vars.shouldPauseTimer = false;
    }

}


reset {


}

start {

}

split {

}

isLoading {
	return vars.shouldPauseTimer;	
}