state("racman") {}

startup {
    print("Starting");

    settings.Add("GBsplit", false, "Split on Gold Bolts");
	settings.Add("SPsplit", false, "Split on Skillpoints");
	settings.Add("ItemSplit", false, "Split on items");
	settings.Add("InfobotSplit", false, "Split on infobots");
}

init {
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);
    
    vars.reader.BaseStream.Position = 0;

    current.x = vars.reader.ReadSingle();
    current.y = vars.reader.ReadSingle();
    current.destinationPlanet = vars.reader.ReadByte();
    current.planet = vars.reader.ReadByte();
    current.playerState = vars.reader.ReadUInt16();
    current.planetFramesCount = vars.reader.ReadUInt32();
    current.gameState = vars.reader.ReadUInt32();
    current.loadingScreen = vars.reader.ReadByte();
	current.gbCollect = vars.reader.ReadByte();
	current.spCollect = vars.reader.ReadByte();
	current.itemsCollect = vars.reader.ReadByte();
	current.kaleboBoltCollect = vars.reader.ReadByte();
	current.infobots = vars.reader.ReadByte();
    current.codebot = vars.reader.ReadByte();
    current.rari = vars.reader.ReadByte();

    vars.ShouldStopTimer = false;

    vars.timer = new System.Windows.Forms.Timer();
    vars.timer.Interval = 7560;
    vars.timer.Tick += new EventHandler((sender, e) => {
    // print("fuck this");
    vars.ShouldStopTimer = true;
    vars.timer.Enabled = false;
    });
	
	Tuple<float, float>[] buttons = {
		Tuple.Create(477.9081f, 601.4653f),
		Tuple.Create(453.7222f, 643.2076f),
		Tuple.Create(436.0573f, 577.0817f),
		Tuple.Create(411.8376f, 619.1204f)
    };
    vars.buttons = buttons;

    vars.veldinFix = false;
}

update {
    vars.reader.BaseStream.Position = 0;

    current.x = vars.reader.ReadSingle();
    current.y = vars.reader.ReadSingle();
    current.destinationPlanet = vars.reader.ReadByte();
    current.planet = vars.reader.ReadByte();
    current.playerState = vars.reader.ReadUInt16();
    current.planetFramesCount = vars.reader.ReadUInt32();
    current.gameState = vars.reader.ReadUInt32();
    current.loadingScreen = vars.reader.ReadByte();
    current.gbCollect = vars.reader.ReadByte();
	current.spCollect = vars.reader.ReadByte();
	current.itemsCollect = vars.reader.ReadByte();
	current.kaleboBoltCollect = vars.reader.ReadByte();
	current.infobots = vars.reader.ReadByte();
    current.codebot = vars.reader.ReadByte();
    current.rari = vars.reader.ReadByte();

    /*
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
    print("Current X: " + current.x + ". Current Y: " + current.y);
    */

    if (current.loadingScreen != 4 && !vars.ShouldStopTimer) {
		vars.timer.Enabled = true;
    }
    else if (current.loadingScreen == 4) {
		vars.ShouldStopTimer = false;
    }
}


reset {
    if (current.planet == 0 && (old.gameState == 6 && current.gameState == 0)) {
        return true;
    }
}

start {
    if (current.planet == 0 && (old.gameState == 6 && current.gameState == 0)) {
        vars.veldinFix = false;
        return true;
    }
}

split {
    // Split everything
    if (current.destinationPlanet != old.destinationPlanet && 
        (current.planet != current.destinationPlanet) && current.destinationPlanet != 0 && current.planet != 0) {
        return true;
    }
    
    
    // Veldin split
    if (!vars.veldinFix && current.gameState == 2 && old.gameState == 0 && current.planet == 0 && current.planetFramesCount > 5) {
        vars.veldinFix = true; // to avoid double veldin split that can happen sometimes.
        return true;
    }


    // Drek button split
    if (current.planet == 18 && current.playerState == 34 && old.playerState != 34) {
        foreach(var button in vars.buttons) {
            var x = current.x - button.Item1;
            var y = current.y - button.Item2;
    
            if(x * x + y * y < 1.7f) {
                return true;
            }
        }
    }

    // Gold bolt split
    if (settings["GBsplit"]) {

        if (current.gbCollect != old.gbCollect)
            return true;

        if (current.kaleboBoltCollect != old.kaleboBoltCollect && current.kaleboBoltCollect != 0)
            return true;
    }
	
	// Skillpoint split
    if (settings["SPsplit"] && current.spCollect != old.spCollect) {
        return true;
    }
	
	// Items split
    if (settings["ItemSplit"]) {

        if (current.itemsCollect != old.itemsCollect)
            return true;

        if (current.codebot != old.codebot && current.codebot != 0)
            return true;

        if (current.rari != old.rari && current.rari != 0)
            return true;
    }
	
	// Infobots split
	if (settings["InfobotSplit"] && (current.infobots != old.infobots)) {
		return true;
	}
}

isLoading {
    return vars.ShouldStopTimer;
}