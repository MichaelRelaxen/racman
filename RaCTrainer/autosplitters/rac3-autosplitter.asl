// Skip to line 110 to edit the split route

state("racman") {

}

startup {
    settings.Add("USE_SPLIT_ROUTE", true, "Use split route");
    settings.SetToolTip("USE_SPLIT_ROUTE", "Use a split route.");
    settings.Add("STRICT_ORDER", false, "Strict order mode", "USE_SPLIT_ROUTE");
    settings.SetToolTip("STRICT_ORDER", "Require the splits in the path to be completed in order.");
    settings.Add("BIO_SPLIT", true, "Split on biobliterator");
    settings.SetToolTip("BIO_SPLIT", "Splits on defeating the biobliterator, the final boss.");
    settings.Add("COUNT_LONG_LOADS", false, "Use long load counter");
    settings.SetToolTip("COUNT_LONG_LOADS", "Count the long loads in a text component. Requires a text component with the left text set to \"Long Loads\".");
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

    vars.reader.BaseStream.Position = 128;
    vars.SplitRoute = vars.reader.ReadBytes(128);

    vars.SplitCount = 0;
    vars.biobliterator = false;

    vars.longloads = 0;

    vars.shipLevels = new byte[]{
        1,2,3,4,5,6,7,8,9,10,11,12,14,16,17,18,19,21,22,23,24
    }.ToList();

    vars.LLCountText = null;

    vars.GetTextComponentPointer = (Func<string, dynamic>)((name) => {
        foreach (dynamic component in timer.Layout.Components)
        {
            if (component.GetType().Name == "TextComponent" && component.Settings.Text1 == name) {
                return component;
            }
        }
        return null;
    });
    
}

update {

    if (settings["COUNT_LONG_LOADS"] && vars.LLCountText == null) {
        vars.LLCountText = vars.GetTextComponentPointer("Long Loads");
    }

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

    vars.reader.BaseStream.Position = 128;
    vars.SplitRoute = vars.reader.ReadBytes(128);

    if (current.loadingScreen == 1 && old.loadingScreen != 1 && vars.shipLevels.Contains(current.destinationPlanet)) {
        // Count a long load
        vars.longloads++;
        timer.SetGameTime(timer.CurrentTime.GameTime.Value.Subtract(TimeSpan.FromSeconds(1)));
    }

    if (settings["COUNT_LONG_LOADS"]) {
        if (vars.LLCountText != null) {
            vars.LLCountText.Settings.Text2 = vars.longloads.ToString();
        }
    }

    if (!vars.biobliterator && current.gameState == 0 && current.planet == 20 && current.neffyPhase % 2 == 1 && current.neffyHealth == 1) {
        vars.biobliterator = true;
    }
}

start {
    if (current.planet == 1 && // We are on veldin
    old.gameState == 6 &&      // The game was loading in the last frame
    current.gameState == 0){   // The game is in gameplay this frame
        vars.SplitCount = 0;
        vars.longloads = 0;
        vars.biobliterator = false;
        return true;
    } 
}

split {
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
