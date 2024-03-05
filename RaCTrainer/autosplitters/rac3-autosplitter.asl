state("racman") {}

startup {
    settings.Add("USE_SPLIT_ROUTE", true, "Use split route");
    settings.SetToolTip("USE_SPLIT_ROUTE", "Use a split route.");
    settings.Add("STRICT_ORDER", false, "Strict order mode", "USE_SPLIT_ROUTE");
    settings.SetToolTip("STRICT_ORDER", "Require the splits in the path to be completed in order.");
    settings.Add("BIO_SPLIT", true, "Split on biobliterator");
    settings.SetToolTip("BIO_SPLIT", "Splits on defeating the biobliterator, the final boss.");
    settings.Add("LDF_SPLIT", false, "Split when entering LDF");
    settings.SetToolTip("LDF_SPLIT", "Splits when entering the laser defence facility on Marcadia. Does not split on exit.");
    settings.Add("KOROS_BOLT", false, "Koros Bolt 2 Split");
    settings.SetToolTip("KOROS_BOLT", "Split when getting the second titanium bolt on Koros.\n(This may split incorrectly if you do things out of order, use at your own risk!)");
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
    current.neffyHealth = vars.reader.ReadSingle();
    current.neffyPhase = vars.reader.ReadUInt32();
    current.chunk = vars.reader.ReadInt32();

    vars.reader.BaseStream.Position = 128;
    vars.SplitRoute = vars.reader.ReadBytes(256);

    vars.SplitCount = 0;
    vars.biobliterator = false;

    vars.llIgnorePlanets = new bool[37];

    // Don't count out long loads when flying from or to these planets
    // i.e. the loading screen on these planets is different than the regular one with ratchet's ship
    // the "long loads" on these loading screens are not actually longer and shouldn't be timed out

    vars.llIgnorePlanets[20] = true; // Launch site (ranger ship)
    vars.llIgnorePlanets[26] = true; // Metro rangers (ranger ship)
    vars.llIgnorePlanets[27] = true; // Aquatos clank (submarine / falling down the pipe)
    vars.llIgnorePlanets[28] = true; // Aquatos sewers (falling down the pipe / submarine)
    vars.llIgnorePlanets[29] = true; // Tryhrranosis rangers (ranger ship)

    vars.korosTBs = 0;

    vars.originPlanet = 0;
    
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
    current.neffyHealth = vars.reader.ReadSingle();
    current.neffyPhase = vars.reader.ReadUInt32();
    current.chunk = vars.reader.ReadInt32();

    vars.reader.BaseStream.Position = 128;
    vars.SplitRoute = vars.reader.ReadBytes(256);


    if (current.planet != old.planet && current.destinationPlanet != 0) {
        vars.originPlanet = old.planet;
    }
    else if (current.planet == old.planet && current.destinationPlanet == 0) {
        vars.originPlanet = current.planet;
    }

    if (current.loadingScreen == 1 && old.loadingScreen != 1 && (!vars.llIgnorePlanets[current.destinationPlanet]) && (!vars.llIgnorePlanets[vars.originPlanet])) {
        timer.SetGameTime(timer.CurrentTime.GameTime.Value.Subtract(TimeSpan.FromSeconds(1)));
    }

    if (!vars.biobliterator && current.gameState == 0 && current.planet == 20 && current.neffyPhase % 2 == 1 && current.neffyHealth == 1) {
        vars.biobliterator = true;
    }

    if (current.playerState == 0x74 && old.playerState != 0x74 && current.planet == 14) {
        vars.korosTBs++;
    }
}

start {
    if (current.planet == 1 && // We are on veldin
    old.gameState == 6 &&      // The game was loading in the last frame
    current.gameState == 0){   // The game is in gameplay this frame
        vars.SplitCount = 0;
        vars.biobliterator = false;
        vars.korosTBs = 0;
        return true;
    } 
}

split {
    if (settings["LDF_SPLIT"] && current.planet == 4 && current.chunk == 1 && old.chunk != 1)
    {
        vars.SplitCount++;
        return true;
    }
    if (current.planet == 14 && vars.korosTBs >= 2 && settings["KOROS_BOLT"]) {
        vars.korosTBs = 0;
        return true;
    }
    if (current.neffyHealth == 0 && vars.biobliterator && current.planet == 20 && settings["BIO_SPLIT"]) {
        vars.biobliterator = false;
        vars.SplitCount++;
        return true;
    }
    else if (current.destinationPlanet != old.destinationPlanet && (current.planet != current.destinationPlanet) && current.destinationPlanet != 0 && current.planet != 0) {
        // we are changing levels (but not reloading, and not going to or from veldin)
        if (settings["STRICT_ORDER"]) {
            if (vars.SplitRoute[vars.SplitCount*2] == current.planet && vars.SplitRoute[vars.SplitCount*2+1] == current.destinationPlanet) {
                vars.SplitCount++;
                return true;
            }
        }
        else if (settings["USE_SPLIT_ROUTE"]) {
            for (int i = 0; i < vars.SplitRoute.Length; i += 2) {
                if (vars.SplitRoute[i] == current.planet && vars.SplitRoute[i+1] == current.destinationPlanet) {
                    vars.SplitCount++;
                    return true;
                }
            }
        }
        else {
            vars.SplitCount++;
            return true;
        }
    }
}

reset {
    if (current.planet == 1 && // We are on veldin
    old.gameState == 6 &&      // The game was loading in the last frame
    current.gameState == 0){   // The game is in gameplay this frame
        vars.SplitCount = 0;
        return true;
    } 
}

isLoading {
    return false;
}
