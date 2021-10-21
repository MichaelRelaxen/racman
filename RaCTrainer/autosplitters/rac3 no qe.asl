state("racman") {}

startup {
    print("Starting");
}

init {
	vars.splitCount = 0;
	vars.splitRoute = 0;
	vars.neffyToggle = 0;
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
	current.neffy = vars.reader.ReadSingle();
	current.neffydead = vars.reader.ReadUInt32();

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
	current.neffy = vars.reader.ReadSingle();
    current.neffydead = vars.reader.ReadUInt32();
    
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
        vars.splitCount = 0;
		vars.splitRoute = 0;
		vars.neffyToggle = 0;
        return true;
    }
}

split {
	if(vars.splitCount == 0 && current.destinationPlanet == 2  ||
	    vars.splitCount == 1 && current.destinationPlanet == 3  ||
	    vars.splitCount == 2 && current.destinationPlanet == 5  ||	    
	    vars.splitCount == 4 && (current.destinationPlanet == 12 && vars.splitRoute == 0 ||  current.destinationPlanet == 11 && vars.splitRoute == 1) ||
	    vars.splitCount == 5 && (current.destinationPlanet == 4 && vars.splitRoute == 0 ||   current.destinationPlanet == 12 && vars.splitRoute == 1) ||
		vars.splitCount == 6 && current.destinationPlanet == 3 ||
		vars.splitCount == 7 && current.destinationPlanet == 23 ||
		vars.splitCount == 8 && current.destinationPlanet == 21 ||
		vars.splitCount == 9 && current.destinationPlanet == 10 ||
		vars.splitCount == 10 && current.destinationPlanet == 3 ||
		vars.splitCount == 11 && current.destinationPlanet == 3 && current.planet != 3 && current.planet != 10||
		vars.splitCount == 12 && current.destinationPlanet == 3 && current.planet != 3 && current.planet != 16||
		vars.splitCount == 13 && current.destinationPlanet == 6 ||
		vars.splitCount == 14 && current.destinationPlanet == 14 ||
		vars.splitCount == 15 && current.destinationPlanet == 22 ||
		vars.splitCount == 16 && current.destinationPlanet == 20
		 ){
		vars.splitCount++;
		return true;
	}
	if(vars.splitCount == 3 && current.destinationPlanet == 11){		
		vars.splitCount++;
		return true;
	}
	else if(vars.splitCount == 3 && current.destinationPlanet == 4){
		vars.splitRoute = 1;
		vars.splitCount++;
		return true;
	}

	if (current.gameState == 0 && current.planet == 20 && current.neffydead == 1 && current.neffy == 1) {
        vars.neffyToggle = 1;
       
    }
     if(current.neffy == 0 && vars.neffyToggle == 1)
     {
     vars.neffyToggle = 0;
     return true;
     }
}

isLoading {
    return vars.ShouldStopTimer;
}
