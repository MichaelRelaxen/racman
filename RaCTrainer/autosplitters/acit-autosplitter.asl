state("racman") {}

startup {
    print("Starting");
}

init {
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);
    
    vars.reader.BaseStream.Position = 0;

    current.mapTimer = vars.reader.ReadSingle();
    current.isPaused = vars.reader.ReadByte();
    current.gameState = vars.reader.ReadByte();
    current.planetString = vars.reader.ReadString();

    vars.gameTimer = 0.0f;
}

update {
    vars.reader.BaseStream.Position = 0;

    current.mapTimer = vars.reader.ReadSingle();
    current.isPaused = vars.reader.ReadByte();
    current.gameState = vars.reader.ReadByte();
    current.planetString = vars.reader.ReadString();

    if(current.gameState != old.gameState)
    {
        print("fucking shit gamestate is " + current.gameState);
    }
    if(current.planetString != old.planetString)
    {
        print("shit planet is " + current.planetString);
    }

    float delta = current.mapTimer - old.mapTimer;

    if (delta > 0.0f)
    {
        vars.gameTimer += delta;
    }
}


reset {


}

start {
    vars.gameTimer = 0.0f;
    if (current.planetString.Contains("great_clock_a") && old.gameState == 6 && current.gameState == 7)
    {
        return true;
    }
}

split {

}

gameTime {
    return TimeSpan.FromSeconds(vars.gameTimer);
}

isLoading
{
    return true;
} 