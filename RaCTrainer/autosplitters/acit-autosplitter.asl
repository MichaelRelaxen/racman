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
}

update {
    vars.reader.BaseStream.Position = 0;

    current.planetFramesCount = vars.reader.ReadUInt32();
}


reset {

}

start {

}

split {

}

isLoading {
    return current.planetFramesCount == old.planetFramesCount;
}