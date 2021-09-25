state("racman") {}

startup {
    print("Starting");

    settings.Add("GBsplit",false, "Split on Gold Bolts");
}

init {
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);
    
    vars.reader.BaseStream.Position = 0;

    current.destinationPlanet = vars.reader.ReadByte();
    current.planet = vars.reader.ReadByte();
    current.playerState = vars.reader.ReadUInt16();
    current.planetFramesCount = vars.reader.ReadUInt32();
    current.gameState = vars.reader.ReadUInt32();
    current.loadingScreen = vars.reader.ReadByte();
    current.mission = vars.reader.ReadByte();

    vars.ShouldStopTimer = false;

    vars.timer = new System.Windows.Forms.Timer();
    vars.timer.Interval = 12320;
    vars.timer.Tick += new EventHandler((sender, e) => {
    print("fuck this");
    vars.ShouldStopTimer = true;
    vars.timer.Enabled = false;
    });
}

update {
    vars.reader.BaseStream.Position = 0;

    current.destinationPlanet = vars.reader.ReadByte();
    current.planet = vars.reader.ReadByte();
    current.playerState = vars.reader.ReadUInt16();
    current.planetFramesCount = vars.reader.ReadUInt32();
    current.gameState = vars.reader.ReadUInt32();
    current.loadingScreen = vars.reader.ReadByte();
    current.mission = vars.reader.ReadByte();
    
    
    if (current.planet != old.planet) {
        print("Shit planet changed to " + current.planet);
    }
    
    if (current.destinationPlanet != old.destinationPlanet) {
        print("Holy fuck you're going to " + current.destinationPlanet);
    }
    
    if (current.playerState != old.playerState) {
        print("Player state " + current.playerState);
    }
    
    if (current.gameState != old.gameState) {
        print("Game state is " + current.gameState);
    }

        if (current.loadingScreen != old.loadingScreen) {
        print("wtf load screen is " + current.loadingScreen);
    }

    if (current.loadingScreen != 4 && !vars.ShouldStopTimer) {
    vars.timer.Enabled = true;
    }
    else if (current.loadingScreen == 4) {
    vars.ShouldStopTimer = false;
    }

}


reset {
    if (current.planet == 1 && (old.gameState == 6 && current.gameState == 0)) {
        return true;
    }
}

start {
    if (current.planet == 1 && (old.gameState == 6 && current.gameState == 0)) {
        return true;
    }
}

split {
    if (current.destinationPlanet != old.destinationPlanet &&
        (current.planet != current.destinationPlanet) && current.destinationPlanet != 0 && current.planet != 0)
    {
        return true;
    }

    if (settings["GBsplit"] && current.playerState == 116 && old.playerState != 116) {
        return true;
    }
}

isLoading {
    return vars.ShouldStopTimer;
}